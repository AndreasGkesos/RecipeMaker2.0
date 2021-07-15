using System;

namespace RecipeMaker2._0.DTO
{
    public class SupplierDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int? DaysBefore { get; set; }
        public int Preference { get; set; }
        public bool? Deleted { get; set; }
        public DateTime? DateDeleted { get; set; }
        public DateTime DateCreated { get; set; }
    }

    public class SupplierAddDTO
    {
        public string Name { get; set; }
        public int? DaysBefore { get; set; }
        public int Preference { get; set; }
    }

    public class SupplierUpdateDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int? DaysBefore { get; set; }
        public int Preference { get; set; }
    }
}
