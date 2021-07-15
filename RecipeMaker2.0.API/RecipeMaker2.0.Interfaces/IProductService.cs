using RecipeMaker2._0.DTO;
using RecipeMaker2._0.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeMaker2._0.Interfaces
{
    public interface IProductService
    {
        Task<IReadOnlyList<Product>> GetAllAsync();
        Task<Product> AddAsync(Product supplier);
        Task<Product> GetByIdAsync(Guid id);
        Task<int> UpdateAsync(Product entity);
        Task<int> DeleteAsync(Guid id);
    }
}
