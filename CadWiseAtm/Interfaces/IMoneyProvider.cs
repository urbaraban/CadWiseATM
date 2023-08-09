namespace CadWiseAtm.Interfaces
{
    public interface IMoneyProvider : IMoneyCase
    {
        public bool CheckMoneyType(MoneyType moneyType);
    }
}
