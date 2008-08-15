using FluentMapping.Domain;
using FluentNHibernate.Mapping;

namespace FluentMapping.Infrastructure.Mappings
{
    public class ProductMap : ClassMap<Product>
    {
        public ProductMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.UnitPrice);
            Map(x => x.UnitsOnStock);
            Map(x => x.Discontinued);
        }

    }
}