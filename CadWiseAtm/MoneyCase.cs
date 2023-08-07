using CadWiseAtm.Interfaces;

namespace CadWiseAtm
{
    public class MoneyCase : LimitMoneyContainer, IMoneyProvider
    {
        public MoneyType MoneyType { get; }

        public override double Sum => ItemsCount * MoneyType.Nominal;

        public MoneyCase(MoneyType moneytype, int maximumcount, int count = 0) : base(maximumcount)
        {
            this.MoneyType = moneytype;
            this.ItemsCount = count;
        }

        public MoneyBundle Decrement(MoneyBundle bundles)
        {
            int operation_count = Math.Min(bundles.Count, this.ItemsCount);
            this.ItemsCount -= operation_count;
            return bundles - operation_count;
        }

        public MoneyBundle Increment(MoneyBundle bundles)
        {
            int operation_count = Math.Min(bundles.Count, this.ItemsMaximum - ItemsCount);
            this.ItemsCount += operation_count;
            return bundles - operation_count;
        }

        public bool CheckMoneyType(MoneyType moneyType)
        {
            return MoneyType == moneyType;
        }
    }

}
