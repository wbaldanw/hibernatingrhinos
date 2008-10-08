using System.Collections.Generic;
using RepositoryPattern.Model;

namespace RepositoryPattern.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        ICollection<Product> FindAllDiscontinuedProducts();
    }
}