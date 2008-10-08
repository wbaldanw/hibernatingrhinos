using NHibernate;
using NHibernate.Cfg;
using RepositoryPattern.Model;

namespace RepositoryPattern
{
    public class SessionProvider
    {
        private static Configuration configuration;
        private static ISessionFactory sessionFactory;

        public static Configuration Configuration
        {
            get
            {
                if(configuration == null)
                {
                    configuration = new Configuration();
                    configuration.Configure();                              // A
                    configuration.AddAssembly(typeof (Product).Assembly);   // B
                }
                return configuration;
            }
        }

        public static ISessionFactory SessionFactory
        {
            get
            {
                if (sessionFactory == null)
                    sessionFactory = Configuration.BuildSessionFactory();
                return sessionFactory;
            }
        }

        private SessionProvider()
        { }

        public static ISession GetSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}