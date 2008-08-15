using System;
using FluentNHibernate.Framework;
using Iesi.Collections.Generic;

namespace FluentMapping.Domain.Scenario3
{
    public class Blog : Entity
    {
        public Blog()
        {
            Posts = new HashedSet<Post>();
        }

        public virtual string Name { get; set; }
        public virtual Person Author { get; set; }
        public virtual ISet<Post> Posts { get; private set; }
    }

    public class Post : Entity
    {
        public virtual string Title { get; set; }
        public virtual string Body { get; set; }
        public virtual DateTime PublicationDate { get; set; }
        public virtual ISet<Comment> Comments { get; private set; }
        public virtual Comment SpecialComment { get; set; }
    }

    public class Comment
    {
        private Comment()
        { }

        public Comment(string text, DateTime creationDate, string authorEmail)
        {
            Text = text;
            CreationDate = creationDate;
            AuthorEmail = authorEmail;
        }

        public virtual string Text { get; private set; }
        public virtual DateTime CreationDate { get; set; }
        public virtual string AuthorEmail { get; private set; }
    }

    public class Person : Entity
    {
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
    }
}