using System;
using Caching;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class Account_with_2nd_level_cache_spec : FixtureBase
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

        protected override string ConfigurationName
        { get { return "hibernate.2nd.cfg.xml"; }}

        [Test]
        public void trying_to_load_an_existing_item_twice_in_different_sessions_should_use_2nd_level_cache()
        {
            using(var session = SessionFactory.OpenSession())
            {
                var acc = session.Get<Account>(account.Id);
                acc.ShouldNotBeNull();
            }

            using(var session = SessionFactory.OpenSession())
            {
                var acc = session.Get<Account>(account.Id);
                acc.ShouldNotBeNull();
            }
        }

        [Test]
        public void when_updating_the_entity_then_2nd_level_cache_should_also_be_updated()
        {
            using(var session = SessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                var acc = session.Get<Account>(account.Id);
                acc.Credit(200m);
                tx.Commit();
            }

            using(var session = SessionFactory.OpenSession())
            {
                var acc = session.Get<Account>(account.Id);
                acc.Balance.ShouldEqual(1200m);
            }
            
        }
    }
}
