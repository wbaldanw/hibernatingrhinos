using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;

namespace CollectionMapping.Tests
{
    public class Fixture_Base
    {
        private ISessionFactory _sessionFactory;
        private Configuration _configuration;
        private ISession _session;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _configuration = new Configuration();
            _configuration.Configure();
            _configuration.AddAssembly(typeof(Customer).Assembly);
            _sessionFactory = _configuration.BuildSessionFactory();
        }

        [SetUp]
        public void SetupContext()
        {
            _session = _sessionFactory.OpenSession();
            new SchemaExport(_configuration).Execute(false, true, false, false, _session.Connection, null);
            CreateInitialData(_session);
            _session.Clear();
        }

        protected virtual void CreateInitialData(ISession session)
        { }

        [TearDown]
        public void TearDownContext()
        {
            _session.Close();
            _session.Dispose();
        }

        protected ISession Session
        {
            get { return _session; }
        }
    }
}