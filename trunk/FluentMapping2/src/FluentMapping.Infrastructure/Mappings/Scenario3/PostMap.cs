using FluentMapping.Domain.Scenario3;
using FluentNHibernate.Mapping;

namespace FluentMapping.Infrastructure.Mappings.Scenario3
{
    public class PostMap : ClassMap<Post>
    {
        public PostMap()
        {
            Id(x => x.Id);
            Map(x => x.Title);
            Map(x => x.Body);
            Map(x => x.PublicationDate);
            HasMany<Comment>(x => x.Comments)
                .Component(c =>
                               {
                                   c.Map(x => x.Text);
                                   c.Map(x => x.AuthorEmail);
                                   c.Map(x => x.CreationDate);
                               }).AsSet();
        }
    }
}