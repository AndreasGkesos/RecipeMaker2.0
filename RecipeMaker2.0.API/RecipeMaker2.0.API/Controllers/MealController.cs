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
    public class MealController : ControllerBase
    {
        private readonly IMealService _mealService;
        private readonly IMapper _mapper;
        private readonly ILogger<MealController> _logger;

        public MealController(IMealService mealService, IMapper mapper, ILogger<MealController> logger)
        {
            _mealService = mealService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IReadOnlyList<MealDTO>> GetAllAsync()
        {
            var result = await _mealService.GetAllAsync();
            return _mapper.Map<IReadOnlyList<MealDTO>>(result);
        }

        [HttpPost]
        [Authorize(Roles = "Operator,Admin")]
        public async Task<MealDTO> AddAsync(MealAddDTO entity)
        {
            var model = _mapper.Map<Meal>(entity);
            var result = await _mealService.AddAsync(model);
            return _mapper.Map<MealDTO>(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<MealDTO> GetByIdAsync(Guid id)
        {
            var result = await _mealService.GetByIdAsync(id);
            return _mapper.Map<MealDTO>(result);
        }

        [HttpPut]
        [Authorize(Roles = "Operator,Admin")]
        public async Task<int> UpdateAsync(MealUpdateDTO entity)
        {
            var model = _mapper.Map<Meal>(entity);
            var result = await _mealService.UpdateAsync(model);
            return result;
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Operator,Admin")]
        public async Task<int> DeleteAsync(Guid id)
        {
            var result = await _mealService.DeleteAsync(id);
            return result;
        }
    }
}
