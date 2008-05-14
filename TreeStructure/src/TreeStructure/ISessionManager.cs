using NHibernate;

namespace TreeStructure
{
    public interface ISessionManager
    {
        ISession OpenSession();
    }
}