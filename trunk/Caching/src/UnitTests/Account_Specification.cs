using System;
using Caching;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class when_an_account_exists : FixtureBase
    {
        private Account account;

        protected override void Context()
        {
            Console.WriteLine("--------------Start setup context");
            base.Context();
            account = new Account("US $ Account", 1000m);
            Session.Save(account);
            Session.Flush();
            Session.Clear();
            Console.WriteLine("--------------End setup context");
        }

        [Test]
        public void trying_to_get_a_nonexisting_entity_should_return_null()
        {
            var acc1 = Session.Get<Account>(0);
            acc1.ShouldBeNull();
        }

        [Test]
        public void trying_to_get_an_existing_entity_should_retrieve_it_from_database()
        {
            var acc1 = Session.Get<Account>(account.Id);
            acc1.ShouldNotBeNull();
            acc1.ShouldNotBeTheSameAs(account);
            acc1.Name.ShouldEqual(account.Name);
            acc1.Balance.ShouldEqual(account.Balance);
        }

        [Test]
        public void trying_to_get_the_same_account_a_second_time_should_get_the_account_from_1st_level_cache()
        {
            var acc1 = Session.Get<Account>(account.Id);
            var acc2 = Session.Get<Account>(account.Id);

            acc1.ShouldBeTheSameAs(acc2);
        }

        [Test]
        public void trying_to_load_a_non_existing_entity()
        {
            var acc1 = Session.Load<Account>(account.Id);
            acc1.ShouldNotBeNull();
            Console.WriteLine("------ now accessing the id of the entity");
            acc1.Id.ShouldEqual(account.Id);
            Console.WriteLine("------ now accessing a property (other than the entity) of the entity");
            acc1.Name.ShouldEqual(account.Name);
        }
    }
}
