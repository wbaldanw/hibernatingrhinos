using System;
using Castle.ActiveRecord;
using Iesi.Collections.Generic;
using NHibernateAndActiveRecord.Domain;

namespace NHibernateAndActiveRecord.Domain
{
    [ActiveRecord(Table = "Orders")]
    public class Order
    {
        public Order()
        {
            OrderLines = new HashedSet<OrderLine>();
        }

        [PrimaryKey(Generator = PrimaryKeyType.Guid)]
        public virtual Guid Id { get; set; }

        [BelongsTo(NotNull = true)]
        public virtual Customer Customer { get; set; }

        [Property]
        public virtual string OrderNumber { get; set; }

        [Property]
        public virtual DateTime OrderDate { get; set; }

        [HasMany(Table = "OrderLine", 
                 ColumnKey = "OrderId", 
                 Cascade = ManyRelationCascadeEnum.AllDeleteOrphan)]
        public virtual ISet<OrderLine> OrderLines { get; set; }
    }
}