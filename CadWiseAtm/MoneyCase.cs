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

        public MoneyBundle Decrement(MoneyBundle bundel)
        {
            if (CheckMoneyType(bundel.MoneyType) == true)
            {
                int operation_count = Math.Min(bundel.Count, this.ItemsCount);
                this.ItemsCount -= operation_count;
                bundel -= operation_count;
            }
            return bundel;
        }

        public MoneyBundle Increment(MoneyBundle bundel)
        {
            if (CheckMoneyType(bundel.MoneyType) == true)
            {
                int operation_count = Math.Min(bundel.Count, this.ItemsMaximum - ItemsCount);
                this.ItemsCount += operation_count;
                bundel -= operation_count;
            }
            return bundel;
        }

        public bool CheckMoneyType(MoneyType moneyType)
        {
            return MoneyType == moneyType;
        }
    }

}
