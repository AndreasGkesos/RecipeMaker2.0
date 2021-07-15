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
    public class ProductRepo : IProductRepo
    {
        private readonly IDbConnection _dbConnection;

        public ProductRepo(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<Product> AddAsync(Product entity)
        {
            var sql = @"declare @Id uniqueidentifier
                        set @Id = NEWID()
                        insert into Products (Id,Name,Quantity,SupplierId,Deleted,DateDeleted,DateCreated) VALUES (@Id,@Name,@Quantity,@SupplierId,@Deleted,@DateDeleted,@DateCreated)
                        select @id";
            var result = await _dbConnection.QueryAsync<Guid>(sql, new {
                @Name = entity.Name,
                @Quantity = entity.Quantity,
                @SupplierId = entity.SupplierId,
                @Deleted = false,
                @DateDeleted = DateTime.UtcNow,
                @DateCreated = DateTime.UtcNow
            });
            entity.Id = result.Single();
            return entity;
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            var sql = @"update Products
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

        public async Task<IReadOnlyList<Product>> GetAllAsync()
        {
            var sql = @"select *
                        from Products as p
                        inner join Suppliers as s
                        on p.SupplierId = s.Id";
            var result = await _dbConnection.QueryAsync<Product, Supplier, Product>(sql, (p, s) => {
                p.Supplier = s;
                return p;
            });
            return result.ToList();
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            var sql = @"select *
                        from Products as p
                        inner join Suppliers as s
                        on p.SupplierId = s.Id
                        where p.Id = @Id";
            var result = await _dbConnection.QueryAsync<Product, Supplier, Product>(sql, (p, s) => {
                p.Supplier = s;
                return p;
            }, new { @Id = id });
            return result.FirstOrDefault();
        }

        public async Task<int> UpdateAsync(Product entity)
        {
            var sql = @"update Products
                        set Name = @Name,
                        Quantity = @Quantity,
                        SupplierId = @SupplierId
                        where Id = @Id";
            var result = await _dbConnection.ExecuteAsync(sql, new
            {
                @Name = entity.Name,
                @Quantity = entity.Quantity,
                @SupplierId = entity.SupplierId,
                @Id = entity.Id
            });
            return result;
        }
    }
}
