using System;

namespace FluentMapping.Domain.Scenario2
{
    public class Address : IEquatable<Address>
    {
        public virtual string AddressLine1 { get; private set; }
        public virtual string AddressLine2 { get; private set; }
        public virtual string PostalCode { get; private set; }
        public virtual string City { get; private set; }
        public virtual string Country { get; private set; }

        private Address()
        {
        }

        public Address(string addressLine1, string addressLine2, string postalCode, string city, string country)
        {
            if(addressLine1==null) throw new ArgumentException("Address Line 1 cannot be null.");
            if(postalCode==null) throw new ArgumentException("Postal Code cannot be null.");
            if(city==null) throw new ArgumentException("City cannot be null.");
            if(country==null) throw new ArgumentException("Country cannot be null.");

            AddressLine1 = addressLine1;
            AddressLine2 = addressLine2;
            PostalCode = postalCode;
            City = city;
            Country = country;
        }

        public bool Equals(Address other)
        {
            if (other == null) return false;
            return AddressLine1.Equals(other.AddressLine1) &&
                   ((AddressLine2 == null && other.AddressLine2 == null) || 
                   (AddressLine2 != null && AddressLine2.Equals(other.AddressLine2))) &&
                   PostalCode.Equals(other.PostalCode) &&
                   City.Equals(other.City) &&
                   Country.Equals(other.Country);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Address);
        }

        public override int GetHashCode()
        {
            return string.Format("{0}|{1}|{2}|{3}|{4}", AddressLine1, AddressLine2, PostalCode, City, Country)
                .GetHashCode();
        }
    }
}