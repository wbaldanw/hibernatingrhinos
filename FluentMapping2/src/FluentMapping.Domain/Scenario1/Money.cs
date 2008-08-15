namespace FluentMapping.Domain
{
    public class Money
    {
        private Money()
        {
        }

        public Money(decimal amount, string currency)
        {
            Amount = amount;
            Currency = currency;
        }

        public virtual decimal Amount { get; private set; }
        public virtual string Currency { get; private set; }
    }
}