using System.IO;
using System.Text;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;

namespace ComplexObjectGraph.Tests
{
    [TestFixture]
    public class Schema_Fixture
    {
        private Configuration _configuration;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _configuration = new Configuration();
            _configuration.Configure();
            _configuration.AddAssembly(typeof(Chart).Assembly);
        }

        [Test]
        public void Inspect_database_schema()
        {
            var sb = new StringBuilder();
            var writer = new StringWriter(sb);
            new SchemaExport(_configuration).Execute(true, false, false, false, null, writer);
        }
    }
}