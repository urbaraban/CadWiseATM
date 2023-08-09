namespace CadWiseAtm.Interfaces
{
    public interface IMoneyCase
    {
        public MoneyType MoneyType { get; }
        public bool CheckIncrement(MoneyBundle moneyType);
        public bool CheckDecrement(MoneyBundle moneyType);
    }
}
