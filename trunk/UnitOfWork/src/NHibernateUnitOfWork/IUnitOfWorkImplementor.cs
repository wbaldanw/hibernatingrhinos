namespace NHibernateUnitOfWork
{
    public interface IUnitOfWorkImplementor : IUnitOfWork
    {
        void IncrementUsages();
    }
}