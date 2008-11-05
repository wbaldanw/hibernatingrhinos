namespace Caching
{
    public class Post
    {
        public virtual int Id { get; private set; }
        public virtual string Title { get; set; }
        public virtual string Body { get; set; }
    }
}