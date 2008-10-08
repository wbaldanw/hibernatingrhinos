using System.Collections.Generic;
using NUnit.Framework;
using RepositoryPattern.Model;
using RepositoryPattern.Repositories;
using RepositoryPattern.RepositoryImpl;

namespace UnitTests
{
    [TestFixture]
    public class FakeRepositoryTester
    {
        private IProductRepository repository;
        private int countProducts;

        [SetUp]
        public void SetupContext()
        {
            repository = new ProductRepositoryFake();
            countProducts = ((ProductRepositoryFake)repository).Dictionary.Count;
        }

        [Test]
        public void can_load_a_product_by_its_id_from_the_repository()
        {
            var product = repository.GetById(2);

            product.ShouldNotBeNull();
            product.Id.ShouldEqual(2);
        }

        [Test]
        public void can_add_a_new_product_to_the_repository()
        {
            repository.Add(new Product {Id = 99, Name = "Product 99", ReorderLevel = 13, Discontinued = false});
            
            // let's try to load this new product
            var product = repository.GetById(99);
            product.Id.ShouldEqual(99);
            product.Name.ShouldEqual("Product 99");
        }

        [Test]
        public void can_remove_an_existing_product_from_the_repository()
        {
            var product = repository.GetById(1);
            repository.Remove(product);

            typeof (KeyNotFoundException).ShouldBeThrownBy(() => repository.GetById(1));
        }

        [Test]
        public void can_load_all_products_from_the_repository()
        {
            var products = repository.FindAll();

            products.Count.ShouldEqual(countProducts);
        }
    }
}
