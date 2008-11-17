using FluentNHibernate.Mapping;

namespace Blobs
{
    public class PersonPhotoMapper : ClassMap<PersonPhoto>
    {
        public PersonPhotoMapper()
        {
            SetAttribute("lazy", "true");
            Id(x => x.Id);
            Map(x => x.Image);
            HasOne(x => x.Owner)
                .PropertyRef(x=>x.Photo)
                .Constrained();
        }
    }
}