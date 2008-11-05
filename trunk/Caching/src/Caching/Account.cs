namespace Caching
{
    public class Account
    {
        public virtual int Id { get; private set; }
        public virtual string Name { get; private set; }
        public virtual decimal Balance { get; private set; }

        protected Account()
        {
        }

        public Account(string name, decimal balance)
        {
            Name = name;
            Balance = balance;
        }

        public virtual void Credit(decimal amount)
        {
            Balance += amount;
        }
    }
}
