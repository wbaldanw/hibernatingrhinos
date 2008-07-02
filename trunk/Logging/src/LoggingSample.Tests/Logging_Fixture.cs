using log4net;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;

// Configure log4net using the .config file
[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace LoggingSample.Tests
{
    [TestFixture]
    public class Logging_Fixture
    {
        private Configuration _cfg;
        private ISessionFactory _factory;
        private static readonly ILog _logger =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _logger.Debug("Starting test series");
            _cfg = new Configuration();
            _cfg.Configure();
            _cfg.AddAssembly(typeof(Person).Assembly);

            _factory = _cfg.BuildSessionFactory();
        }

        [SetUp]
        public void SetupContext()
        {
            _logger.Debug("Setting up context");
            var export = new SchemaExport(_cfg);
            export.Execute(false, true, false, false);
        }

        [Test]
        public void Can_load_list_of_persons()
        {
            _logger.Debug("Start Can_load_list_of_persons");

            using (var session = _factory.OpenSession())
            {
                var persons = session.CreateQuery("from Person").List<Person>();
            }

            _logger.Debug("End Can_load_list_of_persons");
        }
    }
}
