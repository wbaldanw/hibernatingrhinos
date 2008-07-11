using System;
using System.Collections.Generic;
using Iesi.Collections.Generic;

namespace CollectionMapping
{
    public class Person
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual IDictionary<int, string> Hobbies { get; set; }
        public virtual IDictionary<string, Task> Tasks { get; set; }
        public virtual IList<Person> Children { get; set; }

        public Person()
        {
            Hobbies = new Dictionary<int, string>();
            Tasks = new Dictionary<string, Task>();
            Children = new List<Person>();
        }
    }

    public class Task
    {
        public virtual string Description { get; set; }
        public virtual DateTime DueDate { get; set; }
        public virtual bool Done { get; set; }
    }
}