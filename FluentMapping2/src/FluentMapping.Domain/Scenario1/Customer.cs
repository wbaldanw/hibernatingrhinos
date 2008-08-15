using FluentNHibernate.Framework;

namespace FluentMapping.Domain.Scenario1
{
    public class Customer : Entity
    {
        public virtual string Name { get; set; }
    }
}