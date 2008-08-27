using FluentMapping.Domain;
using FluentNHibernate.Framework;
using NUnit.Framework;

namespace FluentMapping.UnitTests
{
    [TestFixture]
    public class Product_Fixture : FixtureBase
    {
        [Test]
        public void Can_add_product_to_database()
        {
            var product = new Product
                              {
                                  Name = "Apple",
                                  UnitPrice = 0.25m,
                                  UnitsOnStock = 1255,
                                  Discontinued = false
                              };
            Session.Save(product);

            // Assertion
            Session.Flush();
            Session.Clear();
            var fromDb = Session.Get<Product>(product.Id);
            Assert.AreNotSame(product, fromDb);
            Assert.AreEqual(product.Name, fromDb.Name);
            Assert.AreEqual(product.UnitPrice, fromDb.UnitPrice);
            Assert.AreEqual(product.UnitsOnStock, fromDb.UnitsOnStock);
            Assert.AreEqual(product.Discontinued, fromDb.Discontinued);
        }

        [Test]
        public void Can_add_product_to_database_revisited()
        {
            new PersistenceSpecification<Product>(Session)
                .CheckProperty(x=>x.Name, "Apple")
                .CheckProperty(x=>x.UnitPrice, 0.25m)
                .CheckProperty(x=>x.UnitsOnStock, 2345)
                .CheckProperty(x=>x.Discontinued, true)
                .VerifyTheMappings();
        }
    }
}
