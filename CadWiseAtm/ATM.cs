using CadWiseAtm.Interfaces;

namespace CadWiseAtm
{
    public class ATM : List<MoneyCase>, IBundleProvider, ILimited
    {
        public bool IsFullMoney => GetFullMoneyLimit();

        public bool IsFull => this.Count == this.Limit;
        public int Limit { get; }

        public IEnumerable<MoneyCurrency> CurrencyList => this.Select(x => x.MoneyType.Currency).Distinct();

        public ATM(int caselimit) 
        {
            this.Limit = caselimit;
        }

        public new bool Add(MoneyCase moneyCase)
        {
            if (this.IsFull == false && moneyCase.MoneyType.Currency != MoneyCurrency.NONE)
            {
                moneyCase.Removed += MoneyCase_Removed;
                this.Add(moneyCase);
                return true;
            }
            return false;
        }

        public new bool AddRange(IEnumerable<MoneyCase> cases)
        {
            foreach(var case_ in cases)
            {
                if (this.Add(case_) == false)
                {
                    return false;
                }
            }
            return true;
        }

        public new bool Remove(MoneyCase moneyCase)
        {
            if (this.Contains(moneyCase) == true)
            {
                this.Remove(moneyCase);
                return true;
            }
            return false;
        }

        public (bool, IEnumerable<MoneyBundle>) Decrement(double check, MoneyCurrency currency)
        {
           return this.Decrement(check, new MoneyType(-1, currency));
        }

        public (bool, IEnumerable<MoneyBundle>) Decrement(double check, MoneyType likeType)
        {
            var bundles = new List<MoneyBundle>();

            var groups = this.Where(x =>
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
                var cases = this.Where(x => x.MoneyType == bundle.MoneyType);
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
                var cases = this.Where(x => x.MoneyType == bundle.MoneyType);
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
            return this.Where(x =>
            (x.MoneyType.Nominal == nominal || nominal < 0) &&
            x.MoneyType.Currency == moneyCurrency || moneyCurrency == MoneyCurrency.NONE);
        }


        private bool GetFullMoneyLimit()
        {
            bool status = true;
            foreach(var case_ in this)
            {
                status &= case_.IsFull;
            }
            return status;
        }

        private void MoneyCase_Removed(object? sender, EventArgs e)
        {
            if (sender is MoneyCase moneycase)
            {
                this.Remove(moneycase);
            }
        }
    }
}