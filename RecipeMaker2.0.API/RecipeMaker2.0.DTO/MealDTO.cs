using System;
using System.Collections.Generic;

namespace RecipeMaker2._0.DTO
{
    public class MealDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool? Deleted { get; set; }
        public DateTime? DateDeleted { get; set; }
        public DateTime DateCreated { get; set; }
        public IEnumerable<ProductQuantityDTO> ProductQuantities { get; set; }
    }

    public class MealAddDTO
    {
        public string Name { get; set; }
        public IEnumerable<ProductQuantityAddDTO> ProductQuantities { get; set; }
    }

    public class MealUpdateDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<ProductQuantityUpdateDTO> ProductQuantities { get; set; }
    }
}
