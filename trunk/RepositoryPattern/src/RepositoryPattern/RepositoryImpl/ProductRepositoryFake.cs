using System.Collections.Generic;
using RepositoryPattern.Model;
using RepositoryPattern.Repositories;

namespace RepositoryPattern.RepositoryImpl
{
    public class ProductRepositoryFake : IProductRepository
    {
        private readonly Dictionary<int, Product> dictionary;

        public Dictionary<int, Product> Dictionary
        {
            get { return dictionary; }
        }

        public ProductRepositoryFake()
        {
            dictionary = new Dictionary<int, Product>();
            dictionary.Add(1, new Product {Id = 1, Name = "Product 1", ReorderLevel = 10, Discontinued = false});
            dictionary.Add(2, new Product {Id = 2, Name = "Product 2", ReorderLevel = 15, Discontinued = false});
            dictionary.Add(3, new Product {Id = 3, Name = "Product 3", ReorderLevel = 10, Discontinued = false});
            dictionary.Add(4, new Product {Id = 4, Name = "Product 4", ReorderLevel = 12, Discontinued = false});
            dictionary.Add(5, new Product {Id = 5, Name = "Product 5", ReorderLevel = 20, Discontinued = true});
        }

        public Product GetById(int id)
        {
            return dictionary[id];
        }

        public ICollection<Product> FindAll()
        {
            return dictionary.Values;
        }

        public void Add(Product product)
        {
            dictionary.Add(product.Id, product);
        }

        public void Remove(Product product)
        {
            dictionary.Remove(product.Id);
        }
    }
}