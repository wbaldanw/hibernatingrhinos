using FluentMapping.Domain.Scenario2;
using FluentNHibernate.Framework;
using NUnit.Framework;

namespace FluentMapping.UnitTests.Scenario2
{
    [TestFixture]
    public class Employee_Fixture : FixtureBase
    {
        [Test]
        public void Verify_that_employee_saves()
        {
            var emp = new Employee
                          {
                              FirstName = "Gabriel",
                              LastName = "Schenker",
                              HomeAddress = new Address("Castle home", null, "8888", "Paradise", "Switzerland"),
                              WorkAddress = new Address("My work place", null, "7777", "Atlantis", "Pegasus")
                          };
            Session.Save(emp);

            Session.Flush();
            Session.Clear();

            var fromDb = Session.Get<Employee>(emp.Id);
            Assert.AreNotSame(emp, fromDb);
            Assert.AreEqual(emp.FirstName, fromDb.FirstName);
            Assert.AreEqual(emp.LastName, fromDb.LastName);
            Assert.AreEqual(emp.HomeAddress.AddressLine1, fromDb.HomeAddress.AddressLine1);
            Assert.AreEqual(emp.HomeAddress.AddressLine2, fromDb.HomeAddress.AddressLine2);
            Assert.AreEqual(emp.HomeAddress.PostalCode, fromDb.HomeAddress.PostalCode);
            Assert.AreEqual(emp.HomeAddress.City, fromDb.HomeAddress.City);
            Assert.AreEqual(emp.HomeAddress.Country, fromDb.HomeAddress.Country);
            Assert.AreEqual(emp.WorkAddress.AddressLine1, fromDb.WorkAddress.AddressLine1);
            Assert.AreEqual(emp.WorkAddress.AddressLine2, fromDb.WorkAddress.AddressLine2);
            Assert.AreEqual(emp.WorkAddress.PostalCode, fromDb.WorkAddress.PostalCode);
            Assert.AreEqual(emp.WorkAddress.City, fromDb.WorkAddress.City);
            Assert.AreEqual(emp.WorkAddress.Country, fromDb.WorkAddress.Country);
        }

        [Test]
        public void Verify_that_employee_saves_revisited()
        {
            new PersistenceSpecification<Employee>(Session)
                .CheckProperty(x=>x.FirstName, "Gabriel")
                .CheckProperty(x=>x.LastName, "Schenker")
                .CheckProperty(x => x.HomeAddress, new Address("Castle home", null, "8888", "Paradise", "Switzerland"))
                .CheckProperty(x => x.WorkAddress, new Address("My work place", null, "7777", "Atlantis", "Pegasus"))
                .VerifyTheMappings();
        }
    }
}