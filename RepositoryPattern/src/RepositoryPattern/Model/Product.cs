namespace RepositoryPattern.Model
{
    public class Product
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int ReorderLevel { get; set; }
        public virtual bool Discontinued { get; set; }
    }
}