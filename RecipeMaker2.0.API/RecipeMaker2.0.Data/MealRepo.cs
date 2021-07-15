using RecipeMaker2._0.Interfaces;
using RecipeMaker2._0.Model;
using Dapper;
using System.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Text;
using System.Transactions;
using RecipeMaker2._0.DTO;

namespace RecipeMaker2._0.Data
{
    public class MealRepo : IMealRepo
    {
        private readonly IDbConnection _dbConnection;

        public MealRepo(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<Meal> AddAsync(Meal entity)
        {
            using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                var sql = @"declare @Id uniqueidentifier
                        set @Id = NEWID()
                        insert into Meals (Id,Name,Deleted,DateDeleted,DateCreated) VALUES (@Id,@Name,@Deleted,@DateDeleted,@DateCreated)
                        select @id";
                var result = await _dbConnection.QueryAsync<Guid>(sql, new
                {
                    @Name = entity.Name,
                    @Deleted = false,
                    @DateDeleted = DateTime.UtcNow,
                    @DateCreated = DateTime.UtcNow
                });
                entity.Id = result.Single();

                var sb = new StringBuilder();
                var now = DateTime.UtcNow;
                var insertSql = "insert into ProductQuantities (Id,Quantity,ProductId,MealId,Deleted,DateDeleted,DateCreated) VALUES ";
                var valuesSql = "(NEWID(),{0},'{1}','{2}',{3},'{4}','{5}')";
                foreach (var pq in entity.ProductQuantities)
                {
                    var values = string.Format(valuesSql, pq.Quantity, pq.ProductId, entity.Id, 0, now, now);
                    sb.Append(insertSql + string.Join(',', values));
                }

                sb.Append($"select * from ProductQuantities where MealId = '{entity.Id}'");
                var result2 = await _dbConnection.QueryAsync<ProductQuantity>(sb.ToString());
                entity.ProductQuantities = result2.ToList();

                transactionScope.Complete();
                return entity;
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                transactionScope.Dispose();
            }
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            var sql = @"update Meals
                        set Deleted = 1,
                        DateDeleted = @DateDeleted
                        where Id = @Id";
            var result = await _dbConnection.ExecuteAsync(sql, new
            {
                @DateDeleted = DateTime.UtcNow,
                @Id = id,
            });

            sql = @"update ProductQuantities
                    set Deleted = 1,
                    DateDeleted = @DateDeleted
                    where MealId = @Id";
            var result2 = await _dbConnection.ExecuteAsync(sql, new
            {
                @DateDeleted = DateTime.UtcNow,
                @Id = id,
            });

            return result;
        }

        public async Task<IReadOnlyList<Meal>> GetAllAsync()
        {
            var sql = @"select *
                        from Meals as m
                        inner join ProductQuantities as pq
                        on pq.MealId = m.Id";
            var dict = new Dictionary<Guid, Meal>();
            var result = await _dbConnection.QueryAsync<Meal, ProductQuantity, Meal>(sql, (m, pq) => {
                Meal entry;
                if (!dict.TryGetValue(m.Id, out entry))
                {
                    entry = m;
                    dict.Add(entry.Id, entry);
                }
                entry.ProductQuantities.Add(pq);
                return entry;
            });
            return result.Distinct().ToList();
        }

        public async Task<Meal> GetByIdAsync(Guid id)
        {
            var sql = @"select *
                        from Meals as m
                        inner join ProductQuantities as pq
                        on pq.MealId = m.Id
                        where m.Id = @Id";
            var dict = new Dictionary<Guid, Meal>();
            var result = await _dbConnection.QueryAsync<Meal, ProductQuantity, Meal>(sql, (m, pq) => {
                Meal entry;
                if (!dict.TryGetValue(m.Id, out entry))
                {
                    entry = m;
                    dict.Add(entry.Id, entry);
                }
                entry.ProductQuantities.Add(pq);
                return entry;
            }, new { @Id = id });

            return result.FirstOrDefault();
        }

        public async Task<IReadOnlyList<MealDataDTO>> GetMealsDataAsync(IEnumerable<Guid> guids)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("GUID", typeof(Guid));

            foreach (var id in guids)
            {
                dt.Rows.Add(id);
            }

            var param = new DynamicParameters();
            param.Add("@GUIDs", dt, dbType: DbType.Object);

            var result = await _dbConnection.QueryAsync<MealDataDTO>("GetMealsData", param, commandType: CommandType.StoredProcedure);

            return result.ToList();
        }

        public async Task<int> UpdateAsync(Meal entity)
        {
            var sql = @"update Meals
                        set Name = @Name
                        where Id = @Id";
            var result = await _dbConnection.ExecuteAsync(sql, new
            {
                @Name = entity.Name,
                @Id = entity.Id
            });

            var sb = new StringBuilder();
            var now = DateTime.UtcNow;
            var updateSql = " update ProductQuantities set ";
            var valuesSql = "Quantity = {0} ";
            foreach (var pq in entity.ProductQuantities)
            {
                var values = string.Format(valuesSql, pq.Quantity);
                sb.Append(updateSql + string.Join(',', values));
                sb.Append($"where Id = '{pq.Id}'");
            }

            var result2 = await _dbConnection.QueryAsync<ProductQuantity>(sb.ToString());
            return result;
        }
    }
}
