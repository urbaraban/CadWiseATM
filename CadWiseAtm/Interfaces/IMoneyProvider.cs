namespace CadWiseAtm.Interfaces
{
    public interface IMoneyProvider : IMoneyType
    {
        public MoneyBundle Increment(MoneyBundle bundles);
        public MoneyBundle Decrement(MoneyBundle bundles);
    }
}
