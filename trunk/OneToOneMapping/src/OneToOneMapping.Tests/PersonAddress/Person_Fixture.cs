using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using OneToOneMapping.PersonAddress;

namespace UnitTests.PersonAddress
{
    public class Person_Fixture : FluentInterfaceFixtureBase
    { }

    [TestFixture]
    public class when_creating_the_schema 
    {
        [SetUp]
        protected void Context()
        {
            var model = new TestModel();
            var config = new Configuration();
            config.Configure();
            model.Configure(config);
            var factory = config.BuildSessionFactory();
            var session = factory.OpenSession();
            new SchemaExport(config).Execute(true, false, false, false, session.Connection, null);
        }

        [Test]
        public void smoke_test()
        {
            true.ShouldBeTrue();
        }
    }

    [TestFixture]
    public class when_adding_a_new_person_with_an_address : Person_Fixture
    {
        private Address address;
        private Person person;

        protected override void Context()
        {
            base.Context();
            address = new Address
                          {
                              AddressLine1 = "Some Street 1",
                              PostalCode = "8000",
                              City = "Zurich"
                          };
            person = new Person {FirstName = "Gabriel", LastName = "Schenker"};
            person.AssignAddress(address);
            Session.Save(person);
            Session.Flush();
            Session.Clear();
        }

        [Test]
        public void smoke_test()
        {
            true.ShouldBeTrue();
        }

        [Test]
        public void should_add_person_to_database()
        {
            var fromDb = Session.Get<Person>(person.Id);
            fromDb.ShouldNotBeNull();
            fromDb.ShouldNotBeTheSameAs(person);
            fromDb.LastName.ShouldEqual(person.LastName);
            fromDb.FirstName.ShouldEqual(person.FirstName);
        }
    }

    [TestFixture]
    public class when_querying_persons : Person_Fixture
    {
        private Person[] persons;

        protected override void Context()
        {
            base.Context();
            persons = new[]
                          {
                              CreatePerson("Schenker", "Gabriel", "Zurich"),
                              CreatePerson("Doe", "John", "Berlin"),
                              CreatePerson("Mosh", "Sue", "New York"),
                          };
            foreach (var person in persons)
                Session.Save(person);

            Session.Flush();
            Session.Clear();
        }

        private Person CreatePerson(string last, string first, string city)
        {
            var address = new Address { City = "Zurich" };
            var person = new Person { FirstName = "Gabriel", LastName = "Schenker" };
            person.AssignAddress(address);
            return person;
        }

        [Test]
        public void smoke_test()
        {
            true.ShouldBeTrue();
        }

        [Test]
        public void can_load_all_persons()
        {
            var list = Session.CreateQuery("from Person").List<Person>();
        }

        [Test]
        public void can_load_all_persons_revisited()
        {
            var list = Session.CreateQuery("select p.Id, p.LastName from Person p").List();
            list.Count.ShouldEqual(persons.Length);
        }
    }
}