using System.Linq;
using NHibernate;
using NHibernate.Linq;

namespace Linq2NH
{
    public class SampleContext : NHibernateContext
    {
        public SampleContext(ISession session)
            : base(session)
        { }

        public IOrderedQueryable<Person> Persons
        {
            get { return Session.Linq<Person>(); }
        }

        public IOrderedQueryable<Task> Tasks
        {
            get { return Session.Linq<Task>(); }
        }
    }
}