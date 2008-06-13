using Iesi.Collections.Generic;

namespace CollectionMapping
{
    public class Customer
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual ISet<string> Contacts { get; set; }
        public virtual ISet<Contact> BusinessContacts { get; set; }

        public Customer()
        {
            Contacts = new HashedSet<string>();
            BusinessContacts = new HashedSet<Contact>();
        }
    }

    public class Contact
    {
        public virtual ContactTypes Type { get; set; }
        public virtual string Description { get; set; }

        public override bool Equals(object obj)
        {
            if(obj == null || !(obj is Contact)) return false;
            var contact = (Contact)obj;
            return ((Description == null && contact.Description == null) || 
                Description.Equals(contact.Description)) && Type.Equals(contact.Type);
        }

        public override int GetHashCode()
        {
            return string.Format("{0}|{1}", Type, Description).GetHashCode();
        }
    }

    public enum ContactTypes
    {
        Undefined=0,
        Email,
        Phone,
        Mobile,
        Fax
    }
}
