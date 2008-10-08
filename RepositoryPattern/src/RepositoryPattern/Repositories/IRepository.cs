using System.Collections.Generic;

namespace RepositoryPattern.Repositories
{
    public interface IRepository<T>
    {
        T GetById(int id);
        ICollection<T> FindAll();
        void Add(T entity);
        void Remove(T entity);
    }
}