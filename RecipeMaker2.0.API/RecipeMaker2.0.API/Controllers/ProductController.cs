using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecipeMaker2._0.DTO;
using RecipeMaker2._0.Interfaces;
using RecipeMaker2._0.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeMaker2._0.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Operator,Admin,User")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService productService, IMapper mapper, ILogger<ProductController> logger)
        {
            _productService = productService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IReadOnlyList<ProductDTO>> GetAllAsync()
        {
            var result = await _productService.GetAllAsync();
            return _mapper.Map<IReadOnlyList<ProductDTO>>(result);
        }

        [HttpPost]
        [Authorize(Roles = "Operator,Admin")]
        public async Task<ProductDTO> AddAsync(ProductAddDTO entity)
        {
            var model = _mapper.Map<Product>(entity);
            var result = await _productService.AddAsync(model);
            return _mapper.Map<ProductDTO>(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ProductDTO> GetByIdAsync(Guid id)
        {
            var result = await _productService.GetByIdAsync(id);
            return _mapper.Map<ProductDTO>(result);
        }

        [HttpPut]
        [Authorize(Roles = "Operator,Admin")]
        public async Task<int> UpdateAsync(ProductUpdateDTO entity)
        {
            var model = _mapper.Map<Product>(entity);
            var result = await _productService.UpdateAsync(model);
            return result;
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Operator,Admin")]
        public async Task<int> DeleteAsync(Guid id)
        {
            var result = await _productService.DeleteAsync(id);
            return result;
        }
    }
}
