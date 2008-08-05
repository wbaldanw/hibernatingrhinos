using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework.Config;
using NHibernateAndActiveRecord.Domain;
using NUnit.Framework;

namespace NibernateAndActiveRecord.Tests
{
    [TestFixture]
    public class CreateSchema_Fixture
    {
        [Test]
        public void Can_initialize_and_create_schema()
        {
            var configurationSource = new XmlConfigurationSource("ActiveRecord.cfg.xml");
            ActiveRecordStarter.Initialize(typeof(Customer).Assembly, configurationSource);
            ActiveRecordStarter.CreateSchema();
        }
    }
}