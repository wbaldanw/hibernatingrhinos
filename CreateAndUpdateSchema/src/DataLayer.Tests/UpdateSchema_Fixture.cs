using System.Reflection;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;

namespace DataLayer.Tests
{
    [TestFixture]
    public class UpdateSchema_Fixture
    {
        private Configuration _cfg;

        public const string product_xml =
            "<?xml version='1.0' encoding='utf-8' ?>"+
            "<hibernate-mapping xmlns='urn:nhibernate-mapping-2.2'"+
            "                   namespace='Domain'"+
            "                   assembly='Domain'>"+
            "  <class name='Product'>"+
            "    <id name='Id'>"+
            "      <generator class='guid'/>"+
            "    </id>"+
            "    <property name='Name' not-null='true' length='20'/>" +
            "  </class>"+
            "</hibernate-mapping>";
        public const string category_xml = 
            "<?xml version='1.0' encoding='utf-8' ?>"+
            "<hibernate-mapping xmlns='urn:nhibernate-mapping-2.2'"+
            "                   namespace='Domain'"+
            "                   assembly='Domain'>"+
            "  <class name='Category'>"+
            "    <id name='Id'>"+
            "      <generator class='guid'/>"+
            "    </id>"+
            "  </class>"+
            "</hibernate-mapping>";

        [SetUp]
        public void SetupContext()
        {
            // Setup "old" database schema
            _cfg = new Configuration();
            _cfg.Configure();
            _cfg.AddXml(product_xml);
            _cfg.AddXml(category_xml);
            var export = new SchemaExport(_cfg);
            export.Execute(false, true, false, false);
        }

        [Test]
        public void Update_an_existing_database_schema()
        {
            _cfg = new Configuration();
            _cfg.Configure();
            _cfg.AddAssembly(Assembly.LoadFrom("DataLayer.dll"));
            var update = new SchemaUpdate(_cfg);
            update.Execute(true, false);
        }
    }
}