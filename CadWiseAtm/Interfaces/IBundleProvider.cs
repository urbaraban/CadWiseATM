namespace CadWiseAtm.Interfaces
{
    public interface IBundleProvider
    {
        public IEnumerable<MoneyBundle> Decrement(double value, MoneyType likeType);
        public IEnumerable<MoneyBundle> Increment(IEnumerable<MoneyBundle> bundles);

    }
}
