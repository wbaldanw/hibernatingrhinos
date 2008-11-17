using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;

namespace UnitTests
{
    public class FixtureBase<T>
    {
        private Configuration configuration;
        private ISessionFactory sessionFactory;

        public ISessionFactory SessionFactory
        {
            get { return sessionFactory; }
        }

        private ISession session;

        public ISession Session
        {
            get { return session; }
        }

        [SetUp]
        public void SetUp()
        {
            Context();
        }

        [TearDown]
        public void TearDown()
        {
            TearDownContext();
        }

        protected virtual void TearDownContext()
        {
            session.Flush();
            session.Dispose();
        }

        protected virtual void Context()
        {
            configuration = new Configuration();
            Configure();
            configuration.AddAssembly(typeof (T).Assembly);
            sessionFactory = configuration.BuildSessionFactory();
            session = sessionFactory.OpenSession();
            new SchemaExport(configuration).Execute(false, true, false, false, session.Connection, null);
        }
         
        private void Configure()
        {
            if (ConfigurationName == null)
                configuration.Configure();
            else
                configuration.Configure(ConfigurationName);
        }

        protected virtual string ConfigurationName{get{ return null; }}
    }
}