using System;

namespace RecipeMaker2._0.Model
{
    public class Supplier
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int? DaysBefore { get; set; }
        public Preference? Preference { get; set; }
        public bool? Deleted { get; set; }
        public DateTime? DateDeleted { get; set; }
        public DateTime DateCreated { get; set; }
    }

    public enum Preference {
        Phone = 1,
        Email = 2
    }
}