using System;
using Iesi.Collections.Generic;

namespace Caching
{
    public class Blog
    {
        public virtual int Id { get; set; }
        public virtual string Author { get; set; }
        public virtual string Name { get; set; }
        public virtual ISet<Post> Posts { get; set; }

        public Blog()
        {
            Posts = new HashedSet<Post>();
        }
    }
}