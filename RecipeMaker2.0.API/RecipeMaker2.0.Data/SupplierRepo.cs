using RecipeMaker2._0.Interfaces;
using RecipeMaker2._0.Model;
using Dapper;
using System.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace RecipeMaker2._0.Data
{
    public class SupplierRepo : ISupplierRepo
    {
        private readonly IDbConnection _dbConnection;

        public SupplierRepo(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<Supplier> AddAsync(Supplier entity)
        {
            var sql = @"declare @Id uniqueidentifier
                        set @Id = NEWID()
                        insert into Suppliers (Id,Name,DaysBefore,Preference,Deleted,DateDeleted,DateCreated) VALUES (@Id,@Name,@DaysBefore,@Preference,@Deleted,@DateDeleted,@DateCreated)
                        select @id";
            var result = await _dbConnection.QueryAsync<Guid>(sql, new {
                @Name = entity.Name,
                @DaysBefore = entity.DaysBefore,
                @Preference = (int)entity.Preference,
                @Deleted = false,
                @DateDeleted = DateTime.UtcNow,
                @DateCreated = DateTime.UtcNow
            });
            entity.Id = result.Single();
            return entity;
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            var sql = @"update Suppliers
                        set Deleted = 1,
                        DateDeleted = @DateDeleted
                        where Id = @Id";
            var result = await _dbConnection.ExecuteAsync(sql, new
            {
                @DateDeleted = DateTime.UtcNow,
                @Id = id,
            });
            return result;
        }

        public async Task<IReadOnlyList<Supplier>> GetAllAsync()
        {
            var result = await _dbConnection.QueryAsync<Supplier>("SELECT * FROM Suppliers");
            return result.ToList();
        }

        public async Task<Supplier> GetByIdAsync(Guid id)
        {
            var result = await _dbConnection.QueryAsync<Supplier>("SELECT * FROM Suppliers where Id = @Id", new { @Id = id });
            return result.FirstOrDefault();
        }

        public async Task<int> UpdateAsync(Supplier entity)
        {
            var sql = @"update Suppliers
                        set Name = @Name,
                        DaysBefore = @DaysBefore,
                        Preference = @Preference
                        where Id = @Id";
            var result = await _dbConnection.ExecuteAsync(sql, new
            {
                @Name = entity.Name,
                @DaysBefore = entity.DaysBefore,
                @Preference = (int)entity.Preference,
                @Id = entity.Id
            });
            return result;
        }
    }
}
