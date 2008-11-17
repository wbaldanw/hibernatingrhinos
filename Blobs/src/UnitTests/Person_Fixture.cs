using System.Text;
using Blobs;
using NHibernate;
using NUnit.Framework;

namespace UnitTests
{
    public class Person_Fixture : FixtureBase<Person>
    { }

    [TestFixture]
    public class when_adding_a_new_person_with_a_photo : Person_Fixture
    {
        private PersonPhoto photo;
        private Person person;

        protected override void Context()
        {
            base.Context();
            photo = new PersonPhoto {Image = Encoding.Default.GetBytes("This is a placeholder for a photo...")};
            person = new Person("Schenker", "Gabriel", photo);
            Session.Save(person);

            // clean up
            Session.Flush();
            Session.Clear();
        }

        // Smoke test
        [Test]
        public void should_execute_without_an_error()
        {
            true.ShouldBeTrue();
        }

        [Test]
        public void person_should_exist_in_the_database()
        {
            var fromDb = Session.Get<Person>(person.Id);
            fromDb.ShouldNotBeNull();
            fromDb.ShouldNotBeTheSameAs(person);
            fromDb.LastName.ShouldEqual(person.LastName);
            fromDb.FirstName.ShouldEqual(person.FirstName);
        }

        [Test]
        public void person_photo_should_exist_in_the_database()
        {
            var fromDb = Session.Get<Person>(person.Id);

            fromDb.Photo.ShouldNotBeNull();
            fromDb.Photo.ShouldNotBeTheSameAs(person.Photo);
            fromDb.Photo.Image.ShouldEqual(person.Photo.Image);
        }

        [Test]
        public void adding_another_person_with_same_photo_should_not_be_possible()
        {
            var otherPerson = new Person("Doe", "John", photo);
            Session.Save(otherPerson);
            try
            {
                Session.Flush();
                Assert.Fail("Expected exception!");
            }
            catch(HibernateException)
            {
                Session.Clear();
            }
        }
    }

    [TestFixture]
    public class when_loading_an_existing_person_from_database : Person_Fixture
    {
        private PersonPhoto photo;
        private Person person;

        protected override void Context()
        {
            base.Context();
            photo = new PersonPhoto { Image = Encoding.Default.GetBytes("This is a placeholder for a photo...") };
            person = new Person("Schenker", "Gabriel", photo);
            Session.Save(person);

            // clean up
            Session.Flush();
            Session.Clear();
        }

        [Test]
        public void Person_photo_should_be_lazy_loaded()
        {
            var fromDb = Session.Load<Person>(person.Id);

            NHibernateUtil.IsInitialized(fromDb.Photo).ShouldBeFalse();

            var image = fromDb.Photo.Image;

            NHibernateUtil.IsInitialized(fromDb.Photo.Image).ShouldBeTrue();
        }
    }

    [TestFixture]
    public class when_adding_a_new_person_with_a_photo_FI : FluentInterfaceFixtureBase
    {
        private PersonPhoto photo;
        private Person person;

        protected override void Context()
        {
            base.Context();
            photo = new PersonPhoto { Image = Encoding.Default.GetBytes("This is a placeholder for a photo...") };
            person = new Person("Schenker", "Gabriel", photo);
            Session.Save(person);

            Session.Flush();
            Session.Clear();
        }

        // Smoke test
        [Test]
        public void should_execute_without_an_error()
        {
            true.ShouldBeTrue();
        }

        [Test]
        public void person_should_exist_in_the_database()
        {
            var fromDb = Session.Get<Person>(person.Id);
            fromDb.ShouldNotBeNull();
            fromDb.ShouldNotBeTheSameAs(person);
            fromDb.LastName.ShouldEqual(person.LastName);
            fromDb.FirstName.ShouldEqual(person.FirstName);
        }

        [Test]
        public void person_photo_should_exist_in_the_database()
        {
            var fromDb = Session.Get<Person>(person.Id);

            fromDb.Photo.ShouldNotBeNull();
            fromDb.Photo.ShouldNotBeTheSameAs(person.Photo);
            fromDb.Photo.Image.ShouldEqual(person.Photo.Image);
        }

        [Test]
        public void adding_another_person_with_same_photo_should_not_be_possible()
        {
            var otherPerson = new Person("Doe", "John", photo);
            Session.Save(otherPerson);
            try
            {
                Session.Flush();
                Assert.Fail("Expected exception!");
            }
            catch (HibernateException)
            {
                Session.Clear();
            }
        }
    }

    [TestFixture]
    public class when_loading_an_existing_person_from_database_FI : FluentInterfaceFixtureBase
    {
        private PersonPhoto photo;
        private Person person;

        protected override void Context()
        {
            base.Context();
            photo = new PersonPhoto { Image = Encoding.Default.GetBytes("This is a placeholder for a photo...") };
            person = new Person("Schenker", "Gabriel", photo);
            Session.Save(person);

            // clean up
            Session.Flush();
            Session.Clear();
        }

        [Test]
        public void Person_photo_should_be_lazy_loaded()
        {
            var fromDb = Session.Load<Person>(person.Id);

            NHibernateUtil.IsInitialized(fromDb.Photo).ShouldBeFalse();

            var image = fromDb.Photo.Image;

            NHibernateUtil.IsInitialized(fromDb.Photo.Image).ShouldBeTrue();
        }
    }
}
