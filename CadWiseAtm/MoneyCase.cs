using CadWiseAtm.Interfaces;

namespace CadWiseAtm
{
    public class MoneyCase : IMoneyProvider, ILimited
    {
        public event EventHandler Removed;
        public event EventHandler CountChanged;

        public MoneyType MoneyType { get; }

        public double Sum => Count * MoneyType.Nominal;

        public int Count 
        {
            get => _count;
            protected set
            {
                _count = value;
                this.CountChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        private int _count = 0;
        public int Limit { get; }
        public bool IsFull => this.Count == this.Limit;

        public MoneyCase(MoneyType moneytype, int limit, int count = 0)
        {
            this.MoneyType = moneytype;
            this.Count = count;
            this.Limit = limit;
        }

        public MoneyCase(int nominal, MoneyCurrency currency, int limit, int count = 0) : this(new MoneyType(nominal, currency), limit, count) { }


        public MoneyBundle Decrement(MoneyBundle bundle)
        {
            if (CheckMoneyType(bundle.MoneyType) == true && bundle.Count > -1)
            {
                int operation_count = Math.Min(bundle.Count, this.Count);
                this.Count -= operation_count;
                bundle -= operation_count;
            }
            return bundle;
        }

        public MoneyBundle Increment(MoneyBundle bundle)
        {
            if (CheckMoneyType(bundle.MoneyType) == true && bundle.Count > -1)
            {
                int operation_count = Math.Min(bundle.Count, this.Limit - Count);
                this.Count += operation_count;
                bundle -= operation_count;
            }
            return bundle;
        }

        public bool CheckMoneyType(MoneyType moneyType)
        {
            return MoneyType == moneyType;
        }

        public void Remove()
        {
            this.Removed?.Invoke(this, EventArgs.Empty);
        }

        public override string ToString()
        {
            return $"{MoneyType} {Count}/{Limit}";
        }
    }

}
