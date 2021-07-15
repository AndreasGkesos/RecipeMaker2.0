using RecipeMaker2._0.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeMaker2._0.Interfaces
{
    public interface IProductRepo { 
        Task<Product> GetByIdAsync(Guid id);
        Task<IReadOnlyList<Product>> GetAllAsync();
        Task<Product> AddAsync(Product entity);
        Task<int> UpdateAsync(Product entity);
        Task<int> DeleteAsync(Guid id);
    }
}
