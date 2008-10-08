using FluentNHibernate.Framework;
using NHibernate;

namespace RepositoryPattern
{
    public class SessionProviderEx
    {
        private static SessionSource sessionSource;

        public static SessionSource SessionSource
        {
            get
            {
                if(sessionSource==null) sessionSource = new SessionSource(new MyModel());
                return sessionSource;
            }
        }

        private SessionProviderEx()
        { }

        public static ISession GetSession()
        {
            return SessionSource.CreateSession();
        }
    }
}