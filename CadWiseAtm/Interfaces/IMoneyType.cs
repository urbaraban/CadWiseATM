namespace CadWiseAtm.Interfaces
{
    public interface IMoneyType
    {
        public MoneyType MoneyType { get; }
        public bool CheckMoneyType(MoneyType moneyType);
    }
}
