using System.Collections.Generic;
using NHibernate;
using NHibernateUnitOfWork;
using RepositoryPattern.Model;
using RepositoryPattern.Repositories;

namespace RepositoryPattern.RepositoryImpl
{
    public class ProductRepository : IProductRepository
    {
        public ISession Session { get { return UnitOfWork.CurrentSession; } }

        public Product GetById(int id)
        {
            return Session.Get<Product>(id);
        }

        public ICollection<Product> FindAll()
        {
            return Session.CreateCriteria(typeof(Product)).List<Product>();
        }

        public void Add(Product product)
        {
            Session.Save(product);
        }

        public void Remove(Product product)
        {
            Session.Delete(product);
        }
    }
}