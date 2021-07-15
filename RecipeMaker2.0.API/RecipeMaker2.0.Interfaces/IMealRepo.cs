using RecipeMaker2._0.DTO;
using RecipeMaker2._0.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeMaker2._0.Interfaces
{
    public interface IMealRepo { 
        Task<Meal> GetByIdAsync(Guid id);
        Task<IReadOnlyList<Meal>> GetAllAsync();
        Task<Meal> AddAsync(Meal entity);
        Task<int> UpdateAsync(Meal entity);
        Task<int> DeleteAsync(Guid id);
        Task<IReadOnlyList<MealDataDTO>> GetMealsDataAsync(IEnumerable<Guid> guids);
    }
}
