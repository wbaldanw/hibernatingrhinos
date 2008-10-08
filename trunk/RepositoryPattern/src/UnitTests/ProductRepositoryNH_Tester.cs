using NHibernate;
using NHibernate.Tool.hbm2ddl;
using NHibernateUnitOfWork;
using NUnit.Framework;
using RepositoryPattern;
using RepositoryPattern.Model;
using RepositoryPattern.Repositories;
using RepositoryPattern.RepositoryImpl;

namespace UnitTests
{
    [TestFixture]
    public class ProductRepositoryNH_Tester
    {
        private IProductRepository repository;
        private Product[] products;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            UnitOfWork.Configuration.AddAssembly(typeof(Product).Assembly);
        }

        [SetUp]
        public void SetupContext()
        {
            UnitOfWork.Start();

            using (var session = SessionProvider.GetSession())
            {
                new SchemaExport(SessionProvider.Configuration)
                    .Execute(false, true, false, false, session.Connection, null);

                CreateInitialData(session);
            }

            repository = new ProductRepository();
        }

        private void CreateInitialData(ISession session)
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
                    session.Save(product);
        }

        [Test]
        public void can_load_product()
        {
            var product = repository.GetById(2);
            product.ShouldNotBeNull();
            product.Id.ShouldEqual(products[1].Id);
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
        }

        [Test]
        public void can_remove_product_from_repository()
        {
            repository.Remove(products[2]);
        }
    }
}