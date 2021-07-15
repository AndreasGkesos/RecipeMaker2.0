using RecipeMaker2._0.DTO;
using RecipeMaker2._0.Interfaces;
using RecipeMaker2._0.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeMaker2._0.Services
{
    public class MealService : IMealService
    {
        private readonly IMealRepo _mealRepo;

        public MealService(IMealRepo mealRepo)
        {
            _mealRepo = mealRepo;
        }

        public Task<Meal> AddAsync(Meal entity)
        {
            var result = _mealRepo.AddAsync(entity);
            return result;
        }

        public Task<int> DeleteAsync(Guid id)
        {
            var result = _mealRepo.DeleteAsync(id);
            return result;
        }

        public Task<IReadOnlyList<Meal>> GetAllAsync()
        {
            return _mealRepo.GetAllAsync();
        }

        public Task<Meal> GetByIdAsync(Guid id)
        {
            var result = _mealRepo.GetByIdAsync(id);
            return result;
        }

        public Task<int> UpdateAsync(Meal entity)
        {
            var result = _mealRepo.UpdateAsync(entity);
            return result;
        }
    }
}
