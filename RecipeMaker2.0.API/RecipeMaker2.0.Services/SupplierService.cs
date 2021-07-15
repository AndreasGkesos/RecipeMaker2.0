using RecipeMaker2._0.DTO;
using RecipeMaker2._0.Interfaces;
using RecipeMaker2._0.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeMaker2._0.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepo _supplierRepo;

        public SupplierService(ISupplierRepo supplierRepo)
        {
            _supplierRepo = supplierRepo;
        }

        public Task<Supplier> AddAsync(Supplier supplier)
        {
            var result = _supplierRepo.AddAsync(supplier);
            return result;
        }

        public Task<int> DeleteAsync(Guid id)
        {
            var result = _supplierRepo.DeleteAsync(id);
            return result;
        }

        public Task<IReadOnlyList<Supplier>> GetAllAsync()
        {
            return _supplierRepo.GetAllAsync();
        }

        public Task<Supplier> GetByIdAsync(Guid id)
        {
            var result = _supplierRepo.GetByIdAsync(id);
            return result;
        }

        public Task<int> UpdateAsync(Supplier entity)
        {
            var result = _supplierRepo.UpdateAsync(entity);
            return result;
        }
    }
}
