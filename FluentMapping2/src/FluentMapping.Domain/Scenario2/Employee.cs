using FluentNHibernate.Framework;

namespace FluentMapping.Domain.Scenario2
{
    public class Employee : Entity
    {
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual Address HomeAddress { get; set; }
        public virtual Address WorkAddress { get; set; }
    }
}