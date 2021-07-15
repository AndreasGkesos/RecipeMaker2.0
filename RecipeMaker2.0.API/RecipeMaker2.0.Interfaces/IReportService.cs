using RecipeMaker2._0.DTO;
using RecipeMaker2._0.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeMaker2._0.Interfaces
{
    public interface IReportService
    {
        Task<IEnumerable<MealDataToReturnDTO>> CalculateMeals(int quantity, IEnumerable<Guid> mealIds, string date);
    }
}
