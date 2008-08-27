using FluentMapping.Domain.Scenario1;
using FluentNHibernate.Mapping;

namespace FluentMapping.Infrastructure.Mappings.Scenario1
{
    public class AccountMap : ClassMap<Account>
    {
        public AccountMap()
        {
            Id(x => x.Id);

            Map(x => x.Name)
                .CanNotBeNull()
                .WithLengthOf(50);
            
            References(x => x.Customer);
            
            Component<Money>(x => x.Balance, m =>
                                                 {
                                                     m.Map(x => x.Amount, "BalanceAmount");
                                                     m.Map(x => x.Currency, "BalanceCurrency");
                                                 });
        }
    }
}