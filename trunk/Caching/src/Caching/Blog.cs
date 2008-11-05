using System.Collections.Generic;

namespace Caching
{
    public class Blog
    {
        public virtual int Id { get; set; }
        public virtual string Author { get; set; }
        public virtual string Name { get; set; }
        public virtual IList<Post> Posts { get; set; }

        public Blog()
        {
            Posts = new List<Post>();
        }

    }
}