using System;

namespace NHibernateRepository.Tests
{
    public class ProductDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ProductDTO(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}