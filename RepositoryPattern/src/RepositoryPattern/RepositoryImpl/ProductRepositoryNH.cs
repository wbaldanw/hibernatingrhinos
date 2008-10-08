using System.Collections.Generic;
using NHibernate;
using RepositoryPattern.Model;
using RepositoryPattern.Repositories;

namespace RepositoryPattern.RepositoryImpl
{
    public class ProductRepositoryNH : IProductRepository
    {
        private static ISession GetSession() { return SessionProvider.GetSession(); }

        public Product GetById(int id)
        {
            using (var session = GetSession())
                return session.Get<Product>(id);
        }

        public ICollection<Product> FindAll()
        {
            using (var session = GetSession())
                return session.CreateCriteria(typeof(Product)).List<Product>();
        }

        public void Add(Product product)
        {
            using (var session = GetSession())
                session.Save(product);
        }

        public void Remove(Product product)
        {
            using (var session = GetSession())
                session.Delete(product);
        }

        public ICollection<Product> FindAllDiscontinuedProducts()
        {
            throw new System.NotImplementedException();
        }
    }
}