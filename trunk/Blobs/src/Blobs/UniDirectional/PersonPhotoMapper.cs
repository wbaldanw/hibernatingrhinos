using FluentNHibernate.Mapping;

namespace Blobs.UniDirectional
{
    public class PersonPhotoMapper : ClassMap<PersonPhoto>
    {
        public PersonPhotoMapper()
        {
            LazyLoad();

            Id(x => x.Id);
            Map(x => x.Image);
        }
    }
}