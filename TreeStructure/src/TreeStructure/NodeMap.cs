using FluentNHibernate.Mapping;

namespace TreeStructure
{
    public class NodeMap : ClassMap<Node>
    {
        public NodeMap()
        {
            LazyLoad();

            Id(x => x.Id);
            Map(x => x.Name);
            References(x => x.Parent)
                .TheColumnNameIs("ParentId");
            HasMany<Node>(x => x.Children)
                .WithKeyColumn("ParentId")
                .WithForeignKeyConstraintName("fk_Node_ParentNode")
                .Cascade.All()
                .AsSet();
            HasManyToMany<Node>(x => x.Descendants)
                .WithParentKeyColumn("ParentId")
                .WithChildKeyColumn("ChildId")
                .WithTableName("NodeHierarchy")
                .AsSet()
                .IsInverse();
            HasManyToMany<Node>(x => x.Ancestors)
                .WithParentKeyColumn("ChildId")
                .WithChildKeyColumn("ParentId")
                .WithTableName("NodeHierarchy")
                .AsSet();
        }
    }
}