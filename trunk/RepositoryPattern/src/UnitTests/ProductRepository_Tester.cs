using System.Linq;
using NHibernateUnitOfWork;
using NUnit.Framework;
using RepositoryPattern.Model;
using RepositoryPattern.Repositories;
using RepositoryPattern.RepositoryImpl;

namespace UnitTests
{
    [TestFixture]
    public class ProductRepository_Tester : RepositoryFixtureBase<Product>
    {
        private IProductRepository repository;
        private Product[] products;

        protected override void Context()
        {
            products = new[]
                           {
                               new Product {Id = 1, Name = "Product 1", ReorderLevel = 10, Discontinued = false},
                               new Product {Id = 2, Name = "Product 2", ReorderLevel = 15, Discontinued = false},
                               new Product {Id = 3, Name = "Product 3", ReorderLevel = 10, Discontinued = false},
                               new Product {Id = 4, Name = "Product 4", ReorderLevel = 12, Discontinued = false},
                               new Product {Id = 5, Name = "Product 5", ReorderLevel = 20, Discontinued = true},
                           };
            
            foreach (var product in products)
                UnitOfWork.CurrentSession.Save(product);

            UnitOfWork.CurrentSession.Flush();
            UnitOfWork.CurrentSession.Clear();

            repository = new ProductRepository();
        }

        [Test]
        public void can_load_product()
        {
            var p = repository.GetById(2);
            p.ShouldNotBeNull();
            p.Id.ShouldEqual(products[1].Id);
        }

        [Test]
        public void can_load_all_products_from_the_repository()
        {
            var list = repository.FindAll();

            list.Count.ShouldEqual(products.Length);
        }

        [Test]
        public void can_add_a_product_to_the_repository()
        {
            repository.Add(new Product { Id = 6, Name = "Product 6", ReorderLevel = 6, Discontinued = false });

            UnitOfWork.CurrentSession.Flush();
            UnitOfWork.CurrentSession.Clear();
            var prod = UnitOfWork.CurrentSession.Get<Product>(6);
            prod.ShouldNotBeNull();
        }

        [Test]
        public void can_remove_product_from_repository()
        {
            repository.Remove(products[2]);

            UnitOfWork.CurrentSession.Flush();
            UnitOfWork.CurrentSession.Clear();
            var prod = UnitOfWork.CurrentSession.Get<Product>(products[2].Id);
            prod.ShouldBeNull();
        }

[Test]
public void can_load_all_discontinued_products()
{
    var discontinuedProducts = repository.FindAllDiscontinuedProducts();

    discontinuedProducts.Count.ShouldBeGreaterThan(0);
    discontinuedProducts.ToList().ForEach(p=>p.Discontinued.ShouldBeTrue());
}
    }
}