using RecipeMaker2._0.DTO;
using RecipeMaker2._0.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeMaker2._0.Interfaces
{
    public interface ISupplierService
    {
        Task<IReadOnlyList<Supplier>> GetAllAsync();
        Task<Supplier> AddAsync(Supplier entity);
        Task<Supplier> GetByIdAsync(Guid id);
        Task<int> UpdateAsync(Supplier entity);
        Task<int> DeleteAsync(Guid id);
    }
}
