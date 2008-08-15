using System;
using FluentNHibernate.Framework;

namespace FluentMapping.Domain
{
    public class Product : Entity
    {
        public virtual string Name { get; set; }
        public virtual Decimal UnitPrice { get; set; }
        public virtual int UnitsOnStock { get; set; }
        public virtual bool Discontinued { get; set; }
    }
}