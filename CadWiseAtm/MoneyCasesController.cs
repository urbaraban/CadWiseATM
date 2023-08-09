using CadWiseAtm.Interfaces;

namespace CadWiseAtm
{
    public class MoneyCasesController : List<MoneyCase>, IMoneyProvider
    {
        public MoneyType MoneyType { get; }

        public double MoneySum => GetSum();

        public MoneyCasesController(MoneyType moneyType)
        {
            MoneyType = moneyType;
        }

        public MoneyCasesController(IEnumerable<MoneyCase> moneyCases, MoneyType moneyType) : base(0)
        {
            this.MoneyType = moneyType;
            this.AddRange(moneyCases);
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

        public MoneyBundle Decrement(MoneyBundle bundle)
        {
            throw new NotImplementedException();
        }

        public bool CheckIncrement(MoneyBundle bundle)
        {
            return CheckMoneyType(bundle.MoneyType) && bundle.Count < this.GetFreeCount();
        }

        public bool CheckDecrement(MoneyBundle bundle)
        {
            return true;
        }

        private bool CheckMoneyType(MoneyType moneyType)
        {
            return (this.MoneyType.Nominal == moneyType.Nominal &&
                this.MoneyType.Currency == moneyType.Currency) 
                ||
                (this.MoneyType.Nominal <= 0 &&
                this.MoneyType.Currency == moneyType.Currency);
        }


        private double GetSum()
        {
            double sum = 0;
            foreach (MoneyCase moneyCase in this)
            {
                sum += moneyCase.Sum;
            }
            return sum;
        }

        private int GetFreeCount()
        {
            int max = this.Sum(x => x.Limit);
            int count = this.Sum(x => x.Count);
            return max - count;
        }


        private List<MoneyCase> _cases = new List<MoneyCase>();
   
    }
}
