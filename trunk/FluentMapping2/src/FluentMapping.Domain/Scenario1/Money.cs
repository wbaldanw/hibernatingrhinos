using System;

namespace FluentMapping.Domain.Scenario1
{
    public class Money : IEquatable<Money>
    {
        private Money()
        {
        }

        public Money(decimal amount, string currency)
        {
            if(string.IsNullOrEmpty(currency)) throw new ArgumentException("Currency cannot be undefined!");

            Amount = amount;
            Currency = currency;
        }

        public virtual decimal Amount { get; private set; }
        public virtual string Currency { get; private set; }

        public bool Equals(Money other)
        {
            if (other == null) return false;
            return Amount == other.Amount && Currency == other.Currency;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Money);
        }

        public override int GetHashCode()
        {
            return string.Format("{0}|{1}", Amount, Currency).GetHashCode();
        }
    }
}