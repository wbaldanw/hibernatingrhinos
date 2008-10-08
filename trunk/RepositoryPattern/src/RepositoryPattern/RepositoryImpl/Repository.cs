using System.Collections.Generic;
using NHibernate;
using NHibernateUnitOfWork;
using RepositoryPattern.Repositories;

namespace RepositoryPattern.RepositoryImpl
{
    public class Repository<T> : IRepository<T>
    {
        public ISession Session { get { return UnitOfWork.CurrentSession; } }

        public T GetById(int id)
        {
            return Session.Get<T>(id);
        }

        public ICollection<T> FindAll()
        {
            return Session.CreateCriteria(typeof(T)).List<T>();
        }

        public void Add(T product)
        {
            Session.Save(product);
        }

        public void Remove(T product)
        {
            Session.Delete(product);
        }
    }
}