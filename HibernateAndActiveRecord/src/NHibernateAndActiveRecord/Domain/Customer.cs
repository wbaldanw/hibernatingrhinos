using System;
using Castle.ActiveRecord;

namespace NHibernateAndActiveRecord.Domain
{
    [ActiveRecord]
    public class Customer
    {
        [PrimaryKey(Generator = PrimaryKeyType.Guid)]
        public virtual Guid Id { get; set; }

        [Property(NotNull = true, Length = 50)]
        public virtual string CompanyName { get; set; }
    }
}