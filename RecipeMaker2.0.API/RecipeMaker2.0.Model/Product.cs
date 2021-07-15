using System;

namespace RecipeMaker2._0.Model
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int? Quantity { get; set; }
        public Guid SupplierId { get; set; }
        public bool? Deleted { get; set; }
        public DateTime? DateDeleted { get; set; }
        public DateTime? DateCreated { get; set; }
        public Supplier Supplier { get; set; }
    }
}