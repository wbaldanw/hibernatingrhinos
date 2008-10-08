using System;

namespace NHibernateUnitOfWork
{
    public interface IGenericTransaction : IDisposable
    {
        void Commit();
        void Rollback();
    }
}