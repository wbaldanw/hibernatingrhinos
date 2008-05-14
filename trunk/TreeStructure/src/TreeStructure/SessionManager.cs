using NHibernate;
using NHibernate.Cfg;

namespace TreeStructure
{
    public class SessionManager : ISessionManager
    {
        private static SessionManager _instance;
        private readonly ISessionFactory _sessionFactory;

        private SessionManager()
        {
            var configuration = new Configuration();
            configuration.Configure();
            configuration.AddAssembly(typeof(Equipment).Assembly);
            _sessionFactory = configuration.BuildSessionFactory();
        }

        public static ISessionManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SessionManager();
                return _instance;
            }
        }

        public ISession OpenSession()
        {
            return _sessionFactory.OpenSession();
        }
    }
}