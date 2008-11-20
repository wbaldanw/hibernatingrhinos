using System;

namespace Blobs.UniDirectional
{
    public class Person
    {
        public virtual Guid Id { get; private set; }
        public virtual string LastName { get; private set; }
        public virtual string FirstName { get; private set; }
        public virtual PersonPhoto Photo { get; private set; }

        // to satisfy NHibernate only!
        public Person() { }

        public Person(string lastName, string firstName, PersonPhoto personPhoto)
        {
            LastName = lastName;
            FirstName = firstName;
            AssignPhoto(personPhoto);
        }

        public virtual void AssignPhoto(PersonPhoto photo)
        {
            Photo = photo;
        }
    }
}
