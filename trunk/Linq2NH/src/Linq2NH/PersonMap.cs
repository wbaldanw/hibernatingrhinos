using FluentNHibernate.Mapping;

namespace Linq2NH
{
    public class PersonMap : ClassMap<Person>
    {
        public PersonMap()
        {
            Id(x => x.Id);
            Map(x => x.Firstname);
            Map(x => x.Lastname);
            HasMany<Task>(x => x.Tasks)
                .LazyLoad()
                .Cascade.All();
        }
    }
}