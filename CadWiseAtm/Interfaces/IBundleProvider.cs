namespace CadWiseAtm.Interfaces
{
    public interface IBundleProvider : IMoneyCase
    {
        public bool Increment(IEnumerable<MoneyBundle> bundles);
        public bool Decrement(IEnumerable<MoneyBundle> bundles);
    }
}
