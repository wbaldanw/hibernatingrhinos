using System;
using Castle.ActiveRecord;

namespace NHibernateAndActiveRecord.Domain
{
    [ActiveRecord]
    public class OrderLine
    {
        [PrimaryKey(Generator = PrimaryKeyType.Guid)]
        public virtual Guid Id { get; set; }

        [Property(NotNull = true)]
        public virtual int Amount { get; set; }

        [Property(NotNull = true, Length = 50)]
        public virtual string ProductName { get; set; }
    }
}