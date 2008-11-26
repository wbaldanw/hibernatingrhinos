using System;
using System.Collections.Generic;
using FluentNHibernate;
using FluentNHibernate.Framework;
using Linq2NH;
using NUnit.Framework;
using System.Linq;

namespace UnitTests
{
    public class TestModel : PersistenceModel
    {
        public TestModel()
        {
            addMappingsFromAssembly(typeof(PersonMap).Assembly);
        }
    }

    public class Person_Fixture : FixtureBase<TestModel>
    {
        protected SampleContext db;

        protected override void Context()
        {
            db = new SampleContext(Session);
        }
    }

    [TestFixture]
    public class when_adding_a_person : Person_Fixture
    {
        [Test]
        public void can_add_person_without_tasks()
        {
            new PersistenceSpecification<Person>(Session)
                .CheckProperty(x => x.Firstname, "Gabriel")
                .CheckProperty(x => x.Lastname, "Schenker")
                .VerifyTheMappings();
        }

        [Test]
        public void can_add_person_with_tasks()
        {
            var tasks = new[]
                            {
                                new Task {TaskName = "Task 1", DueDate = DateTime.Today.AddDays(5)},
                                new Task {TaskName = "Task 2", DueDate = DateTime.Today.AddDays(6)},
                                new Task {TaskName = "Task 3", DueDate = DateTime.Today.AddDays(3)},
                            };
            new PersistenceSpecification<Person>(Session)
                .CheckProperty(x => x.Firstname, "Gabriel")
                .CheckProperty(x => x.Lastname, "Schenker")
                .CheckList(x=>x.Tasks, tasks)
                .VerifyTheMappings();
        }
    }

    public class a_repository_with_persons_having_tasks : Person_Fixture
    {
        protected Person[] persons;
        private IList<Task> tasks1, tasks2, tasks3;

        protected override void Context()
        {
            base.Context();
            tasks1 = new[]
                         {
                             new Task {TaskName = "Task 1.1", DueDate = DateTime.Today.AddDays(5)},
                             new Task {TaskName = "Task 1.2", DueDate = DateTime.Today.AddDays(6)},
                             new Task {TaskName = "Task 1.3", DueDate = DateTime.Today.AddDays(3)},
                         };
            tasks2 = new[]
                         {
                             new Task {TaskName = "Task 2.1", DueDate = DateTime.Today.AddDays(5)},
                             new Task {TaskName = "Task 2.2", DueDate = DateTime.Today.AddDays(6)},
                             new Task {TaskName = "Task 2.3", DueDate = DateTime.Today.AddDays(3)},
                         };
            tasks3 = new[]
                         {
                             new Task {TaskName = "Task 3.1", DueDate = DateTime.Today.AddDays(5)},
                             new Task {TaskName = "Task 3.2", DueDate = DateTime.Today.AddDays(6)},
                             new Task {TaskName = "Task 3.3", DueDate = DateTime.Today.AddDays(2)},
                         };
            persons = new[]
                          {
                              new Person {Firstname = "Gabriel", Lastname = "Schenker", Tasks = tasks1},
                              new Person {Firstname = "John", Lastname = "Doe", Tasks = tasks2},
                              new Person {Firstname = "Ann", Lastname = "Moe", Tasks = tasks3},
                          };
            foreach (var person in persons)
                Session.Save(person);
        }
    }

    [TestFixture]
    public class when_querying_all_persons : a_repository_with_persons_having_tasks
    {
        private IEnumerable<Person> list;

        protected override void Because()
        {
            list = from p in db.Persons
                   select p;
        }

        [Test]
        public void should_return_all_persons()
        {
            list.Count().ShouldEqual(persons.Length);
        }
    }

    [TestFixture]
    public class when_retrieving_filtered_list_of_persons : a_repository_with_persons_having_tasks
    {
        [Test]
        public void can_filter_by_LastName()
        {
            var list = db.Persons.Where(x=>x.Lastname=="Doe");
            list.Count().ShouldEqual(1);
        }

        [Test]
        public void can_filter_by_task()
        {
            var list = from p in db.Persons
                       from t in p.Tasks
                       where t.DueDate == DateTime.Today.AddDays(3)
                       select p;
            list.Count().ShouldEqual(2);
        }
    }

    [TestFixture]
    public class when_retrieving_ordered_list_of_persons : a_repository_with_persons_having_tasks
    {
        [Test]
        public void can_order_by_LastName()
        {
            var list = db.Persons.OrderBy(x => x.Lastname);
            list.First().Lastname.ShouldEqual("Doe");
        }
    }

    [TestFixture]
    public class when_grouping_and_aggregating : a_repository_with_persons_having_tasks
    {
        [Test]
        public void can_get_task_with_the_nearest_due_date_per_person()
        {
            var q = from p in db.Persons
                    from t in p.Tasks
                    select t;
            var minDate = q.Min(t => t.DueDate);
            var maxDate = q.Max(t => t.DueDate);
            minDate.ShouldEqual(DateTime.Today.AddDays(2));
            maxDate.ShouldEqual(DateTime.Today.AddDays(6));
        }
    }
}