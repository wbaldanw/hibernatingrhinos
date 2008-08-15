using FluentMapping.Domain.Scenario1;
using FluentNHibernate.Mapping;

namespace FluentMapping.Infrastructure.Mappings.Scenario1
{
    public class CustomerMap : ClassMap<Customer>
    {
        public CustomerMap()
        {
            Id(x => x.Id);

            Map(x => x.Name)
                .CanNotBeNull()
                .WithLengthOf(50);
        }
    }
}