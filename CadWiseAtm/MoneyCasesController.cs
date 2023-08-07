using CadWiseAtm.Interfaces;
using System.Collections;

namespace CadWiseAtm
{
    public class MoneyCasesController : LimitMoneyContainer, IList<IMoneyProvider>, IBundleProvider
    {
        public MoneyType MoneyType { get; }

        public override double Sum => GetSum();

        public IMoneyProvider this[int index] { get => ((IList<IMoneyProvider>)_cases)[index]; set => ((IList<IMoneyProvider>)_cases)[index] = value; }

        public MoneyCasesController(MoneyType moneyType, int maximumitems) : base (maximumitems)
        {
            MoneyType = moneyType;
        }

        public MoneyCasesController(IEnumerable<IMoneyProvider> moneyCases, MoneyType moneyType) : base(0)
        {
            this.MoneyType = moneyType;
            this._cases.AddRange(moneyCases);
        }

        public bool Increment(IEnumerable<MoneyBundle> bundles)
        {
            throw new NotImplementedException();
        }

        public bool Decrement(IEnumerable<MoneyBundle> bundles)
        {
            throw new NotImplementedException();
        }

        public bool CheckMoneyType(MoneyType moneyType)
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


        private List<IMoneyProvider> _cases = new List<IMoneyProvider>();

        public int Count => ((ICollection<IMoneyProvider>)_cases).Count;

        public bool IsReadOnly => ((ICollection<IMoneyProvider>)_cases).IsReadOnly;

        public int IndexOf(IMoneyProvider item)
        {
            return ((IList<IMoneyProvider>)_cases).IndexOf(item);
        }

        public void Insert(int index, IMoneyProvider item)
        {
            ((IList<IMoneyProvider>)_cases).Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            ((IList<IMoneyProvider>)_cases).RemoveAt(index);
        }

        public void Add(IMoneyProvider item)
        {
            if (this.IsFull == false && CheckMoneyType(item.MoneyType) == true)
            {
                ((ICollection<IMoneyProvider>)_cases).Add(item);
            }
        }

        public void Clear()
        {
            ((ICollection<IMoneyProvider>)_cases).Clear();
        }

        public bool Contains(IMoneyProvider item)
        {
            return ((ICollection<IMoneyProvider>)_cases).Contains(item);
        }

        public void CopyTo(IMoneyProvider[] array, int arrayIndex)
        {
            ((ICollection<IMoneyProvider>)_cases).CopyTo(array, arrayIndex);
        }

        public bool Remove(IMoneyProvider item)
        {
            return ((ICollection<IMoneyProvider>)_cases).Remove(item);
        }

        public IEnumerator<IMoneyProvider> GetEnumerator()
        {
            return ((IEnumerable<IMoneyProvider>)_cases).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_cases).GetEnumerator();
        }
    }
}
