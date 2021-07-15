using RecipeMaker2._0.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeMaker2._0.Interfaces
{
    public interface ISupplierRepo { 
        Task<Supplier> GetByIdAsync(Guid id);
        Task<IReadOnlyList<Supplier>> GetAllAsync();
        Task<Supplier> AddAsync(Supplier entity);
        Task<int> UpdateAsync(Supplier entity);
        Task<int> DeleteAsync(Guid id);
    }
}
