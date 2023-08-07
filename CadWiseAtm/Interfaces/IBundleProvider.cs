namespace CadWiseAtm.Interfaces
{
    public interface IBundleProvider : IMoneyType
    {
        public bool Increment(IEnumerable<MoneyBundle> bundles);
        public bool Decrement(IEnumerable<MoneyBundle> bundles);
    }
}
