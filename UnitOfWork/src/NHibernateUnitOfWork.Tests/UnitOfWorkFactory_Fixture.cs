using System;
using NHibernate;
using NUnit.Framework;

namespace NHibernateUnitOfWork.Tests
{
    [TestFixture]
    public class UnitOfWorkFactory_Fixture
    {
        private IUnitOfWorkFactory _factory;

        [SetUp]
        public void SetupContext()
        {
            _factory = (IUnitOfWorkFactory) Activator.CreateInstance(typeof (UnitOfWorkFactory), true);
        }

        [Test]
        public void Can_create_unit_of_work()
        {
            IUnitOfWork implementor = _factory.Create(); 
            Assert.IsNotNull(implementor);
            Assert.IsNotNull(_factory.CurrentSession);
            Assert.AreEqual(FlushMode.Commit, _factory.CurrentSession.FlushMode);
        }

        [Test]
        public void Can_create_and_access_session_factory()
        {
            var sessionFactory = ((UnitOfWorkFactory)_factory).SessionFactory;
            Assert.IsNotNull(sessionFactory);
            Assert.AreEqual("NHibernate.Dialect.MsSql2005Dialect", sessionFactory.Dialect.ToString());
        }
    }
}