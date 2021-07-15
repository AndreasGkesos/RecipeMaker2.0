using System;
using System.Collections.Generic;

namespace RecipeMaker2._0.Model
{
    public class Meal
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool? Deleted { get; set; }
        public DateTime? DateDeleted { get; set; }
        public DateTime? DateCreated { get; set; }

        public ICollection<ProductQuantity> ProductQuantities { get; set; } = new List<ProductQuantity>();
    }
}