namespace CadWiseAtm.Interfaces
{
    public interface IMoneyProvider : IMoneyCase
    {
        public MoneyBundle Increment(MoneyBundle bundles);
        public MoneyBundle Decrement(MoneyBundle bundles);
    }
}
