using System;
using System.Collections.Generic;
using System.Linq;
using FluentNHibernate.Mapping;
using NHibernate;
using NHibernate.Linq;

namespace Linq2NH
{
    public class Person
    {
        public Person()
        {
            Tasks = new List<Task>();
        }

        public virtual long Id { get; set; }
        public virtual string Lastname { get; set; }
        public virtual string Firstname { get; set; }
        public virtual IList<Task> Tasks { get; set; }
    }

    public class Task
    {
        public virtual long Id { get; set; }
        public virtual string TaskName { get; set; }
        public virtual DateTime DueDate { get; set; }
    }

    public class PersonMap : ClassMap<Person>
    {
        public PersonMap()
        {
            Id(x => x.Id);
            Map(x => x.Firstname);
            Map(x => x.Lastname);
            HasMany<Task>(x => x.Tasks)
                .LazyLoad()
                .Cascade.All();
        }
    }

    public class TaskMap : ClassMap<Task>
    {
        public TaskMap()
        {
            Id(x => x.Id);
            Map(x => x.TaskName);
            Map(x => x.DueDate);
        }
    }

    public class SampleContext : NHibernateContext
    {
        public SampleContext(ISession session)
            : base(session)
        {
        }

        public IOrderedQueryable<Person> Persons
        {
            get { return Session.Linq<Person>(); }
        }
    }
}
