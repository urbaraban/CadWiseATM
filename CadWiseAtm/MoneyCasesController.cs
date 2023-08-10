using CadWiseAtm.Interfaces;

namespace CadWiseAtm
{
    public class MoneyCasesController : List<MoneyCase>, IMoneyProvider
    {
        public MoneyType MoneyType { get; }

        public MoneyCasesController(MoneyType moneyType)
        {
            MoneyType = moneyType;
        }

        public MoneyCasesController(IEnumerable<MoneyCase> moneyCases, MoneyType moneyType) : base(moneyCases)
        {
            this.MoneyType = moneyType;
        }

        public MoneyCasesController(IEnumerable<MoneyCase> moneyCases, int nominal, MoneyCurrency currency) : 
            this(moneyCases, new MoneyType(nominal, currency))
            { }

        public MoneyCasesController(IEnumerable<MoneyCase> moneyCases) :
        this(moneyCases, new MoneyType(moneyCases.ElementAt(0).MoneyType.Nominal, moneyCases.ElementAt(0).MoneyType.Currency))
        { 
            if (moneyCases.GroupBy(x => x.MoneyType).Count() > 2)
            {
                throw new ArgumentException("Many MoneyType in cases");
            }
        }

        public MoneyBundle Increment(MoneyBundle bundle)
        {
            if (CheckIncrement(bundle) == true)
            {
                int i = 0;
                while (bundle.IsEmpty == false && i < this.Count)
                {
                    bundle = this[i].Increment(bundle);
                    i += 1;
                }
            }
            return bundle;
        }

        public MoneyBundle GetMaxBundle(double value)
        {
            if (this.GetCount() > 0 && this.MoneyType.Nominal > 0 && 
                this.MoneyType.Currency != MoneyCurrency.NONE)
            {
                int max_count = Math.Min(this.GetCount(), (int)value / this.MoneyType.Nominal);
                return new MoneyBundle(this.MoneyType, max_count);
            }
            else
            {
                return new MoneyBundle();
            }
        }

        public MoneyBundle Decrement(MoneyBundle bundle)
        {
            if (CheckDecrement(bundle) == true)
            {
                int i = 0;
                while (bundle.IsEmpty == false && i < this.Count)
                {
                    bundle = this[i].Decrement(bundle);
                    i += 1;
                }
            }
            return bundle;
        }

        private bool CheckIncrement(MoneyBundle bundle)
        {
            return CheckMoneyType(bundle.MoneyType) && bundle.Count > -1 && bundle.Count <= this.GetFreeCount();
        }

        private bool CheckDecrement(MoneyBundle bundle)
        {
            return CheckMoneyType(bundle.MoneyType) && bundle.Count > -1 && bundle.Count <= this.GetCount();
        }

        public bool CheckMoneyType(MoneyType moneyType)
        {
            return (this.MoneyType.Nominal == moneyType.Nominal &&
                this.MoneyType.Currency == moneyType.Currency) 
                ||
                (this.MoneyType.Nominal <= 0 &&
                this.MoneyType.Currency == moneyType.Currency);
        }


        public double GetSum(MoneyCurrency currency)
        {
            double sum = 0;
            foreach (MoneyCase moneyCase in this)
            {
                if (moneyCase.MoneyType.Currency == currency
                    ||
                    moneyCase.MoneyType.Currency == MoneyCurrency.NONE)
                {
                    // Тут нужно сделать подгрузку курса валют
                    sum += moneyCase.Sum;
                }
                
            }
            return sum;
        }

        private int GetFreeCount()
        {
            int max = this.Sum(x => x.Limit);
            int count = this.Sum(x => x.Count);
            return max - count;
        }
        private int GetCount()
        {
            int count = this.Sum(x => x.Count);
            return count;
        }

        public MoneyBundle GetMaxBundle(double value, MoneyType moneyType)
        {
            if (CheckMoneyType(moneyType) == true)
            {
                int count = (int)value / moneyType.Nominal;
                return new MoneyBundle(moneyType, Math.Max(this.GetCount(), count));
            }
            return new MoneyBundle();
        }   
    }
}
