using System;

namespace Domain
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int UnitsOnStock { get; set; }
        public Category Category { get; set; }
    }
}
