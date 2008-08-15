using FluentNHibernate.Framework;

namespace FluentMapping.Domain.Scenario1
{
    public class Account : Entity
    {
        public virtual string Name { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Money Balance { get; set; }
    }
}