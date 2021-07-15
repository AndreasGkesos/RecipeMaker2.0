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
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;
        private readonly IMapper _mapper;
        private readonly ILogger<SupplierController> _logger;

        public SupplierController(ISupplierService supplierService, IMapper mapper, ILogger<SupplierController> logger)
        {
            _supplierService = supplierService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IReadOnlyList<SupplierDTO>> GetAllAsync()
        {
            var result = await _supplierService.GetAllAsync();
            return _mapper.Map<IReadOnlyList<SupplierDTO>>(result);
        }

        [Authorize(Roles = "Operator,Admin")]
        [HttpPost]
        public async Task<SupplierDTO> AddAsync(SupplierAddDTO supplier)
        {
            var model = _mapper.Map<Supplier>(supplier);
            var result = await _supplierService.AddAsync(model);
            return _mapper.Map<SupplierDTO>(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<SupplierDTO> GetByIdAsync(Guid id)
        {
            var result = await _supplierService.GetByIdAsync(id);
            return _mapper.Map<SupplierDTO>(result);
        }

        [Authorize(Roles = "Operator,Admin")]
        [HttpPut]
        public async Task<int> UpdateAsync(SupplierUpdateDTO supplier)
        {
            var model = _mapper.Map<Supplier>(supplier);
            var result = await _supplierService.UpdateAsync(model);
            return result;
        }

        [Authorize(Roles = "Operator,Admin")]
        [HttpDelete]
        [Route("{id}")]
        public async Task<int> DeleteAsync(Guid id)
        {
            var result = await _supplierService.DeleteAsync(id);
            return result;
        }
    }
}
