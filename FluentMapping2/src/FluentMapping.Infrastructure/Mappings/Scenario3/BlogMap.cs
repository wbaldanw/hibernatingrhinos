using FluentMapping.Domain.Scenario3;
using FluentNHibernate.Mapping;

namespace FluentMapping.Infrastructure.Mappings.Scenario3
{
    public class BlogMap : ClassMap<Blog>
    {
        public BlogMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            References(x => x.Author);
            HasMany<Post>(x => x.Posts).AsSet().CascadeAll();
        }
    }
}