using System;
using FluentMapping.Domain.Scenario2;
using FluentNHibernate.Mapping;

namespace FluentMapping.Infrastructure.Mappings.Scenario2
{
    public class EmployeeMap : ClassMap<Employee>
    {
        public EmployeeMap()
        {
            Id(x => x.Id);
            Map(x => x.FirstName).CanNotBeNull().WithLengthOf(20);
            Map(x => x.LastName).CanNotBeNull().WithLengthOf(20);

            Component(x => x.HomeAddress, AddressMap.WithColumnPrefix("Home_"));
            Component(x => x.WorkAddress, AddressMap.WithColumnPrefix("Work_"));
        }
    }

    public class AddressMap
    {
        public static Action<ComponentPart<Address>> WithColumnPrefix(string columnPrefix)
        {
            return a =>
            {
                a.Map(x => x.AddressLine1, columnPrefix + "AddressLine1");
                a.Map(x => x.AddressLine2, columnPrefix + "AddressLine2");
                a.Map(x => x.PostalCode, columnPrefix + "PostalCode");
                a.Map(x => x.City, columnPrefix + "City");
                a.Map(x => x.Country, columnPrefix + "Country");
            };
        }
    }
}