using System.Collections.Generic;

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
}
