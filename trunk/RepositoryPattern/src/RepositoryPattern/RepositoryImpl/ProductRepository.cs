using System.Collections.Generic;
using NHibernate.Criterion;
using RepositoryPattern.Model;
using RepositoryPattern.Repositories;

namespace RepositoryPattern.RepositoryImpl
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ICollection<Product> FindAllDiscontinuedProducts()
        {
            return Session.CreateCriteria(typeof (Product))
                .Add(Restrictions.Eq("Discontinued", true))
                .List<Product>();
        }
    }
}