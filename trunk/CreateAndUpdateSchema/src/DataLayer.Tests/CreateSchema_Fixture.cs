using System.IO;
using System.Reflection;
using System.Text;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;

namespace DataLayer.Tests
{
    [TestFixture]
    public class CreateSchema_Fixture
    {
        private Configuration _cfg;

        [SetUp]
        public void SetupContext()
        {
            _cfg = new Configuration();
            _cfg.Configure();
            _cfg.AddAssembly(Assembly.LoadFrom("DataLayer.dll"));
        }

        [Test]
        public void Create_a_database_schema_creation_script()
        {
            var export = new SchemaExport(_cfg);
            var sb = new StringBuilder();
            TextWriter output = new StringWriter(sb);
            export.Execute(true, false, false, false, null, output);
        }
    }
}
