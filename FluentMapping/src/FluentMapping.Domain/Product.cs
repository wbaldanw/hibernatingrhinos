using System;

namespace FluentMapping.Domain
{
    public class Product : EntityBase
    {
        public virtual string Name { get; set; }
        public virtual Decimal UnitPrice { get; set; }
        public virtual int UnitsOnStock { get; set; }
        public virtual bool Discontinued { get; set; }
    }
}