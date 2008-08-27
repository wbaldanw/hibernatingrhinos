using FluentMapping.Domain.Scenario1;
using FluentNHibernate.Framework;
using NUnit.Framework;

namespace FluentMapping.UnitTests.Scenario1
{
    [TestFixture]
    public class Account_Fixture : FixtureBase
    {
        private Customer customer;

        protected override void CreateInitialData(NHibernate.ISession session)
        {
            customer = new Customer {Name = "Microsoft"};
            Session.Save(customer);
        }

        [Test]
        public void Can_add_a_new_account()
        {
            var account = new Account
                              {
                                  Name = "12345.678 8H1",
                                  Customer = customer,
                                  Balance = new Money(2000, "USD")
                              };
            Session.Save(account);

            Session.Flush();
            Session.Clear();

            var fromDb = Session.Get<Account>(account.Id);
            Assert.AreNotSame(account, fromDb);
            Assert.AreEqual(account.Name, fromDb.Name);
            Assert.AreEqual(account.Customer.Name, fromDb.Customer.Name);
            Assert.AreEqual(account.Balance.Amount, fromDb.Balance.Amount);
            Assert.AreEqual(account.Balance.Currency, fromDb.Balance.Currency);
        }

        [Test]
        public void Can_add_a_new_account_revisited()
        {
            new PersistenceSpecification<Account>(Session)
                .CheckProperty(x => x.Name, "12345.678 8H1")
                .CheckProperty(x => x.Customer, customer)
                .CheckProperty(x => x.Balance, new Money(2000, "USD"))
                .VerifyTheMappings();
        }
    }
}