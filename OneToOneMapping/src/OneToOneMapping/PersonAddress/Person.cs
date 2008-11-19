using FluentNHibernate.Mapping;

namespace OneToOneMapping.PersonAddress
{
    public class Person
    {
        public virtual int Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual Address Address { get; private set; }

        public virtual void AssignAddress(Address address)
        {
            Address = address;
            address.Owner = this;
        }
    }

    public class Address
    {
        public virtual int Id { get; set; }
        public virtual string AddressLine1 { get; set; }
        public virtual string AddressLine2 { get; set; }
        public virtual string PostalCode { get; set; }
        public virtual string City { get; set; }
        public virtual Person Owner { get; set; }
    }

    public class PersonMapper : ClassMap<Person>
    {
        public PersonMapper()
        {
            LazyLoad();

            Id(x => x.Id);
            Map(x => x.FirstName);
            Map(x => x.LastName);
            HasOne(x => x.Address)
                //.Constrained()        // affects the order in which save and update are cascaded 
                .PropertyRef(p => p.Owner)
                .Cascade.All()
                .FetchType.Join();
        }
    }

    public class AddressMapper : ClassMap<Address>
    {
        public AddressMapper()
        {
            LazyLoad();

            Id(x => x.Id);
            Map(x => x.AddressLine1);
            Map(x => x.AddressLine2);
            Map(x => x.PostalCode);
            Map(x => x.City);
            References(x => x.Owner)
                .WithUniqueConstraint()
                .TheColumnNameIs("PersonId")
                .LazyLoad()
                .Cascade.None();
        }
    }
}