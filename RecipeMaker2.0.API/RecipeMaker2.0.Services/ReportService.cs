using RecipeMaker2._0.DTO;
using RecipeMaker2._0.Interfaces;
using RecipeMaker2._0.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Globalization;

namespace RecipeMaker2._0.Services
{
    public class ReportService : IReportService
    {
        private readonly IMealRepo _mealRepo;

        public ReportService(IMealRepo mealRepo)
        {
            _mealRepo = mealRepo;
        }

        public async Task<IEnumerable<MealDataToReturnDTO>> CalculateMeals(int quantity, IEnumerable<Guid> mealIds, string date)
        {
            var result = await _mealRepo.GetMealsDataAsync(mealIds);
            var mealsGroups = result.GroupBy(x => x.MealId).ToList();

            var productQuantityDict = new Dictionary<Guid, int>();

            foreach (var group in mealsGroups)
            {
                foreach (var meal in group)
                {
                    int q;
                    if (!productQuantityDict.TryGetValue(meal.ProductId, out q))
                    {
                        q = meal.PQQuantity;
                        productQuantityDict.Add(meal.ProductId, q);
                    } else
                    {
                        productQuantityDict[meal.ProductId] = q + productQuantityDict[meal.ProductId];
                    }
                }
            }

            var resultsToRerurn = new List<MealDataToReturnDTO>();
            var eventDateTime = DateTime.Parse(date).ToUniversalTime();
            
            foreach (var item in productQuantityDict)
            {
                var meal = result.Where(x => x.ProductId == item.Key).FirstOrDefault();
                var m = new MealDataToReturnDTO {
                    MealId = meal.MealId,
                    ProductQuantityId = meal.ProductQuantityId,
                    ProductId = meal.ProductId,
                    SupplierId = meal.SupplierId,
                    PQQuantity = item.Value * quantity,
                    ProductName = meal.ProductName,
                    ProductQuanyity = meal.ProductQuanyity,
                    SupplierName = meal.SupplierName,
                    Preference = Enum.GetName(typeof(Preference), meal.Preference),
                    DaysBefore = meal.DaysBefore,
                    NeedsExtra = meal.ProductQuanyity < item.Value * quantity,
                    ExtraQuantity = item.Value * quantity - meal.ProductQuanyity,
                    InformDate = eventDateTime.Subtract( new TimeSpan(days: meal.DaysBefore, hours: 0, minutes: 0, seconds: 0)).Date
                };

                resultsToRerurn.Add(m);
            }

            return resultsToRerurn;
        }
    }
}
