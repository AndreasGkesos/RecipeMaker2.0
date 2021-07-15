using RecipeMaker2._0.DTO;
using RecipeMaker2._0.Interfaces;
using RecipeMaker2._0.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeMaker2._0.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepo _productRepo;

        public ProductService(IProductRepo productRepo)
        {
            _productRepo = productRepo;
        }

        public Task<Product> AddAsync(Product entity)
        {
            var result = _productRepo.AddAsync(entity);
            return result;
        }

        public Task<int> DeleteAsync(Guid id)
        {
            var result = _productRepo.DeleteAsync(id);
            return result;
        }

        public Task<IReadOnlyList<Product>> GetAllAsync()
        {
            return _productRepo.GetAllAsync();
        }

        public Task<Product> GetByIdAsync(Guid id)
        {
            var result = _productRepo.GetByIdAsync(id);
            return result;
        }

        public Task<int> UpdateAsync(Product entity)
        {
            var result = _productRepo.UpdateAsync(entity);
            return result;
        }
    }
}
