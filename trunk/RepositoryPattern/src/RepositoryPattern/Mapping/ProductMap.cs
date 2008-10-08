using FluentNHibernate.Mapping;
using RepositoryPattern.Model;

namespace RepositoryPattern
{
    public class ProductMap : ClassMap<Product>
    {
        public ProductMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.ReorderLevel);
            Map(x => x.Discontinued);
        }
    }
}