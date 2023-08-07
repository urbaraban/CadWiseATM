namespace CadWiseAtm
{
    public class ATM : MoneyCasesController
    {
        public ATM(int maximumCaseCount) : base(new MoneyType(), maximumCaseCount) { }
    }
}