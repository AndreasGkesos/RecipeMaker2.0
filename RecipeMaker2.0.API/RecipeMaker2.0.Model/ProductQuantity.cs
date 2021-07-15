using System;

namespace RecipeMaker2._0.Model
{
    public class ProductQuantity
    {
        public Guid Id { get; set; }
        public int? Quantity { get; set; }
        public Guid ProductId { get; set; }
        public Guid MealId { get; set; }
        public bool? Deleted { get; set; }
        public DateTime? DateDeleted { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}