using System;

namespace RecipeMaker2._0.DTO
{
    public class ProductDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int? Quantity { get; set; }
        public Guid SupplierId { get; set; }
        public bool? Deleted { get; set; }
        public DateTime? DateDeleted { get; set; }
        public DateTime DateCreated { get; set; }
        public SupplierDTO Supplier { get; set; }
    }

    public class ProductAddDTO
    {
        public string Name { get; set; }
        public int? Quantity { get; set; }
        public Guid SupplierId { get; set; }
    }

    public class ProductUpdateDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int? Quantity { get; set; }
        public Guid SupplierId { get; set; }
    }
}
