using System;
using System.Collections.Generic;
using FirstSolution.Domain;
using NHibernate;
using NHibernate.Criterion;

namespace FirstSolution.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public void Add(Product product)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(product);
                    transaction.Commit();
                }
        }

        public void Update(Product product)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Update(product);
                    transaction.Commit();
                }
        }

        public void Remove(Product product)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(product);
                    transaction.Commit();
                }
        }

        public Product GetById(Guid productId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.Get<Product>(productId);
        }

        //public Product GetByName(string name)
        //{
        //    using (ISession session = NHibernateHelper.OpenSession())
        //    {
        //        Product product = session
        //            .CreateQuery("from Product p where p.Name=:name")
        //            .SetString("name", name)
        //            .UniqueResult<Product>();
        //        return product;
        //    }
        //}

        public Product GetByName(string name)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                Product product = session
                    .CreateCriteria(typeof(Product))
                    .Add(Restrictions.Eq("Name", name))
                    .UniqueResult<Product>();
                return product;
            }
        }

        public ICollection<Product> GetByCategory(string category)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var products = session
                    .CreateCriteria(typeof(Product))
                    .Add(Restrictions.Eq("Category", category))
                    .List<Product>();
                return products;
            }
        }
    }
}            
            
