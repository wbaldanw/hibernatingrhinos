using FluentNHibernate.Mapping;

namespace Blobs
{
    public class PersonMapper : ClassMap<Person>
    {
        public PersonMapper()
        {
            LazyLoad();
            
            Id(x => x.Id);
            Map(x => x.FirstName);
            Map(x => x.LastName);
            References(x => x.Photo)
                .FetchType.Select()
                .Cascade.All()
                .TheColumnNameIs("PersonPhotoId")
                .WithUniqueConstraint();
        }
    }
}