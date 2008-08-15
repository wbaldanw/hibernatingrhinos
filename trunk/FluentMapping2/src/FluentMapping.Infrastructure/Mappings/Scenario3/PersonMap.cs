using FluentMapping.Domain.Scenario3;
using FluentNHibernate.Mapping;

namespace FluentMapping.Infrastructure.Mappings.Scenario3
{
    public class PersonMap : ClassMap<Person>
    {
        public PersonMap()
        {
            Id(x => x.Id);
            Map(x => x.FirstName);
            Map(x => x.LastName);
        }
    }
}
