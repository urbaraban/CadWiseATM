using CadWiseAtm.Interfaces;

namespace CadWiseAtm
{
    public class MoneyCasesController : LimitMoneyContainer, IList<IMoneyProvider>, IBundleProvider
    {
        public MoneyType MoneyType { get; }

        public override double Sum => GetSum();

        private List<IMoneyProvider> _cases = new List<IMoneyProvider>();

        public MoneyCasesController(MoneyType moneyType, int maximumitems) : base (maximumitems)
        {
            MoneyType = moneyType;
        }

        public MoneyCasesController(IEnumerable<IMoneyProvider> moneyCases, MoneyType moneyType) : base(0)
        {
            this.MoneyType = moneyType;
        }

        public new void Add(MoneyCase moneyCase)
        {
            
        }

        public bool Increment(IEnumerable<MoneyBundle> bundles)
        {
            throw new NotImplementedException();
        }

        public bool Decrement(IEnumerable<MoneyBundle> bundles)
        {
            throw new NotImplementedException();
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
    }
}
