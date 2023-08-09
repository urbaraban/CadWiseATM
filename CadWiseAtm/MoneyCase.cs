using CadWiseAtm.Interfaces;

namespace CadWiseAtm
{
    public class MoneyCase : IMoneyProvider
    {
        public MoneyType MoneyType { get; }

        public double Sum => Count * MoneyType.Nominal;

        public int Count { get; protected set; } = 0;
        public int Limit { get; }
        public bool IsFull => this.Count == this.Limit;


        public MoneyCase(MoneyType moneytype, int limit, int count = 0)
        {
            this.MoneyType = moneytype;
            this.Count = count;
            this.Limit = limit;
        }

        public MoneyBundle Decrement(MoneyBundle bundel)
        {
            if (CheckMoneyType(bundel.MoneyType) == true)
            {
                int operation_count = Math.Min(bundel.Count, this.Count);
                this.Count -= operation_count;
                bundel -= operation_count;
            }
            return bundel;
        }

        public MoneyBundle Increment(MoneyBundle bundel)
        {
            if (CheckMoneyType(bundel.MoneyType) == true)
            {
                int operation_count = Math.Min(bundel.Count, this.Limit - Count);
                this.Count += operation_count;
                bundel -= operation_count;
            }
            return bundel;
        }

        public bool CheckIncrement(MoneyBundle bundle)
        {
            return true;
        }

        public bool CheckDecrement(MoneyBundle bundle)
        {
            return true;
        }

        public bool CheckMoneyType(MoneyType moneyType)
        {
            return MoneyType == moneyType;
        }
    }

}
