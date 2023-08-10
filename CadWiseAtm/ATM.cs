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

        public (bool, IEnumerable<MoneyBundle>) Decrement(double value, MoneyCurrency currency)
        {
           return this.Decrement(value, new MoneyType(-1, currency));
        }

        public (bool, IEnumerable<MoneyBundle>) Decrement(double check, MoneyType likeType)
        {
            var bundles = new List<MoneyBundle>();

            var groups = cases_.Where(x =>
            x.MoneyType.Currency == likeType.Currency && 
            (x.MoneyType.Nominal <= likeType.Nominal || likeType.Nominal <= 0))
                .GroupBy(x => x.MoneyType)
                .Reverse();

            for (int i = 0; check > 0 && i < groups.Count(); i += 1)
            {
                var bundle = new MoneyCasesController(groups.ElementAt(i)).GetMaxBundle(check);
                if (bundle.IsEmpty == false)
                {
                    bundles.Add(bundle);
                }
                check -= bundle.Sum;
            }
            
            if (bundles.Count() > 0 && check == 0)
            {
                this.Decrement(bundles);
            }

            return (check == 0, bundles);
        }

        public IEnumerable<MoneyBundle> Decrement(IEnumerable<MoneyBundle> bundles)
        {
            var result = new List<MoneyBundle>();
            var union = MoneyBundle.Defrag(bundles);
            foreach (var bundle in union)
            {
                var cases = cases_.Where(x => x.MoneyType == bundle.MoneyType);
                var controller = new MoneyCasesController(cases, bundle.MoneyType);
                MoneyBundle residue = controller.Decrement(bundle);
                if (residue.IsEmpty == false)
                {
                    result.Add(residue);
                }
            }
            return result;
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