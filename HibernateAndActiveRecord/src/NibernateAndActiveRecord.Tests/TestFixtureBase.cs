using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework.Config;
using NHibernateAndActiveRecord.Domain;
using NUnit.Framework;

namespace NibernateAndActiveRecord.Tests
{
    [TestFixture]
    public class TestFixtureBase
    {
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            var configurationSource = new XmlConfigurationSource("ActiveRecord.cfg.xml");
            ActiveRecordStarter.ResetInitializationFlag();
            ActiveRecordStarter.Initialize(typeof(Customer).Assembly, configurationSource);
        }

        [SetUp]
        public void SetupContext()
        {
            ActiveRecordStarter.CreateSchema();
            Before_each_test();
        }

        [TearDown]
        public void TearDownContext()
        {
            After_each_test();
        }

        protected virtual void Before_each_test()
        { }

        protected virtual void After_each_test()
        { }
    }
}