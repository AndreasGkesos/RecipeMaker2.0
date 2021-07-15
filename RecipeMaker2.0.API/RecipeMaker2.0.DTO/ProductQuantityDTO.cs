using System;

namespace RecipeMaker2._0.DTO
{
    public class ProductQuantityDTO
    {
        public Guid Id { get; set; }
        public int? Quantity { get; set; }
        public Guid ProductId { get; set; }
        public Guid MealId { get; set; }
        public bool? Deleted { get; set; }
        public DateTime? DateDeleted { get; set; }
        public DateTime? DateCreated { get; set; }
    }

    public class ProductQuantityAddDTO
    {
        public int? Quantity { get; set; }
        public Guid ProductId { get; set; }
    }

    public class ProductQuantityUpdateDTO
    {
        public Guid Id { get; set; }
        public int? Quantity { get; set; }
        public Guid ProductId { get; set; }
        public Guid MealId { get; set; }
    }
}
