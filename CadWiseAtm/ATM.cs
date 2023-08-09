using CadWiseAtm.Interfaces;

namespace CadWiseAtm
{
    public class ATM : IBundleProvider, ILimited
    {
        private List<MoneyCase> cases_ { get; } = new List<MoneyCase>();

        public bool IsFullMoney => GetFullMoneyLimit();

        public bool IsFull => this.Count == this.Limit;
        public int Count => cases_.Count;

        public int Limit { get; }

        public ATM(int caselimit) 
        {
            this.Limit = caselimit;
        }

        public bool Add(MoneyCase moneyCase)
        {
            if (this.IsFull == false && moneyCase.MoneyType.Currency != MoneyCurrency.NONE)
            {
                this.cases_.Add(moneyCase);
                return true;
            }
            return false;
        }

        public bool Remove(MoneyCase moneyCase)
        {
            if (this.cases_.Contains(moneyCase) == true)
            {
                this.cases_.Remove(moneyCase);
                return true;
            }
            return false;
        }

        public IEnumerable<MoneyBundle> Decrement(double value, MoneyType likeType)
        {
            double check = value;
            var bundles = new List<MoneyBundle>();

            var cases = GetCases(likeType.Nominal, likeType.Currency);
            if (cases.Any())
            {
                var controller = new MoneyCasesController(cases, likeType.Nominal, likeType.Currency);
            }
            
            if (controller.Decrement(bundle).IsEmpty == false)
            {
                return false;
            }
        }

        public IEnumerable<MoneyBundle> Increment(IEnumerable<MoneyBundle> bundles)
        {
            var union = MoneyBundle.Defrag(bundles);
            var result = new List<MoneyBundle>();
            foreach (var bundle in union)
            {
                var cases = cases_.Where(x => x.MoneyType == bundle.MoneyType);
                var controller = new MoneyCasesController(cases, bundle.MoneyType);
                MoneyBundle residue = controller.Increment(bundle);
                if (residue.IsEmpty == false)
                {
                    result.Add(residue);
                }
            }
            return result;
        }

        public double GetSum(MoneyCurrency moneyCurrency)
        {
            IEnumerable<MoneyCase> cases = GetCases(-1, moneyCurrency);
            var controller = new MoneyCasesController(cases, new MoneyType(-1, moneyCurrency));
            return controller.GetSum(moneyCurrency);
        }

        public IEnumerable<MoneyCase> GetCases(double nominal, MoneyCurrency moneyCurrency)
        {
            return this.cases_.Where(x =>
            (x.MoneyType.Nominal == nominal || nominal < 0) &&
            x.MoneyType.Currency == moneyCurrency || moneyCurrency == MoneyCurrency.NONE);
        }


        private bool GetFullMoneyLimit()
        {
            bool status = true;
            foreach(var case_ in cases_)
            {
                status &= case_.IsFull;
            }
            return status;
        }
    }
}