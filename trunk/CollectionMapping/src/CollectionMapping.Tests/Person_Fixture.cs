using System;
using NUnit.Framework;

namespace CollectionMapping.Tests
{
    [TestFixture]
    public class When_no_person_exists : Fixture_Base
    {
        [Test]
        public void Can_add_person_with_hobbies()
        {
            var person = new Person {Name = "John Doe"};
            person.Hobbies.Add(1, "Fishing");
            person.Hobbies.Add(2, "Diving");
            person.Hobbies.Add(3, "Singing");
            person.Hobbies.Add(4, "Cooking");

            Session.Save(person);
            Session.Flush();
            Session.Clear();

            // Assertions
            var fromDb = Session.Get<Person>(person.Id);
            Assert.AreNotSame(person, fromDb);
            Assert.AreEqual(person.Name, fromDb.Name);
            Assert.AreEqual(person.Hobbies.Count, fromDb.Hobbies.Count);
            Assert.AreEqual(person.Hobbies[2], fromDb.Hobbies[2]);
        }

        [Test]
        public void Can_add_person_with_tasks()
        {
            var person = new Person {Name = "John Doe"};
            person.Tasks.Add("key1", new Task{Description = "Task 1", DueDate = DateTime.Today, Done = false});
            person.Tasks.Add("key2", new Task{Description = "Task 2", DueDate = DateTime.Today, Done = true});

            Session.Save(person);
            Session.Flush();
            Session.Clear();

            // Assertions
            var fromDb = Session.Get<Person>(person.Id);
            Assert.AreNotSame(person, fromDb);
            Assert.AreEqual(person.Tasks.Count, fromDb.Tasks.Count);
        }

        [Test]
        public void Can_add_person_with_children()
        {
            var person = new Person {Name = "John Doe"};
            person.Children.Add(new Person {Name = "Samanta Carter"});
            person.Children.Add(new Person {Name = "Daniel Jackson"});
            person.Children.Add(new Person {Name = "Ann Harbor"});

            Session.Save(person);
            Session.Flush();
            Session.Clear();

            // Assertions
            var fromDb = Session.Get<Person>(person.Id);
            Assert.AreNotSame(person, fromDb);
            Assert.AreEqual(person.Children.Count, fromDb.Children.Count);
        }
    }
}