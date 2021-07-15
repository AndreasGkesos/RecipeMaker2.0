using System;
using System.Collections.Generic;

namespace RecipeMaker2._0.DTO
{
    public class CalculateMealsDTO
    {
        public int Quantity { get; set; }
        public string Date { get; set; }
        public IEnumerable<Guid> Meals { get; set; }
    }

    public class MealDataDTO
    {
        public Guid MealId { get; set; }
        public Guid ProductQuantityId { get; set; }
        public Guid ProductId { get; set; }
        public Guid SupplierId { get; set; }
        public int PQQuantity { get; set; }
        public string ProductName { get; set; }
        public int ProductQuanyity { get; set; }
        public string SupplierName { get; set; }
        public int Preference { get; set; }
        public int DaysBefore { get; set; }
    }

    public class MealDataToReturnDTO
    {
        public Guid MealId { get; set; }
        public Guid ProductQuantityId { get; set; }
        public Guid ProductId { get; set; }
        public Guid SupplierId { get; set; }
        public int PQQuantity { get; set; }
        public string ProductName { get; set; }
        public int ProductQuanyity { get; set; }
        public string SupplierName { get; set; }
        public string Preference { get; set; }
        public int DaysBefore { get; set; }
        public bool NeedsExtra { get; set; }
        public int ExtraQuantity { get; set; }
        public DateTime InformDate { get; set; }
    }
}