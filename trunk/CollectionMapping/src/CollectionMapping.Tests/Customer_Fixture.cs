using NUnit.Framework;

namespace CollectionMapping.Tests
{
    [TestFixture]
    public class When_no_customer_exists : Fixture_Base
    {
        [Test]
        public void Can_create_customer()
        {
            var customer = new Customer {Name = "John Doe"};
            Session.Save(customer);
            
            Session.Flush();
            Session.Clear();
            var fromDb = Session.Get<Customer>(customer.Id);
            Assert.AreNotSame(customer, fromDb);
            Assert.AreEqual(customer.Name, fromDb.Name);
        }

        [Test]
        public void Can_create_customer_with_contacts()
        {
            var customer = new Customer {Name = "John Doe"};
            customer.Contacts.Add("Business phone: 123-12 34 56");
            customer.Contacts.Add("Mobile: 555-72 44 55");
            customer.Contacts.Add("Email: john.doe@somecompany.com");

            Session.Save(customer);
            Session.Flush();
            Session.Clear();

            // Assertions
            var fromDb = Session.Get<Customer>(customer.Id);
            Assert.AreNotSame(customer, fromDb);
            Assert.AreEqual(customer.Name, fromDb.Name);
            Assert.AreEqual(customer.Contacts.Count, fromDb.Contacts.Count);
        }

        [Test]
        public void Can_create_customer_with_business_contacts()
        {
            var customer = new Customer {Name = "John Doe"};
            customer.BusinessContacts.Add(new Contact
                                              {
                                                  Type = ContactTypes.Phone,
                                                  Description = "123-12 34 56"
                                              });
            customer.BusinessContacts.Add(new Contact
                                              {
                                                  Type = ContactTypes.Mobile,
                                                  Description = "555-72 44 55"
                                              });
            customer.BusinessContacts.Add(new Contact
                                              {
                                                  Type = ContactTypes.Email,
                                                  Description = "john.doe@somecompany.com"
                                              });

            Session.Save(customer);
            Session.Flush();
            Session.Clear();

            // Assertions
            var fromDb = Session.Get<Customer>(customer.Id);
            Assert.AreNotSame(customer, fromDb);
            Assert.AreEqual(customer.Name, fromDb.Name);
            Assert.AreEqual(customer.BusinessContacts.Count, fromDb.BusinessContacts.Count);
        }
    }


}