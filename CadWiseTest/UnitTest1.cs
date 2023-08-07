using CadWiseAtm;

namespace CadWiseTest
{
    public class UnitTest1
    {
        [Fact]
        public void MoneyCaseIncrementTest()
        {
            MoneyCase moneyCase = new MoneyCase(new MoneyType(100, MoneyCurrency.RUB), 100);
            MoneyBundle bundle = new MoneyBundle(new MoneyType(100, MoneyCurrency.RUB), 100);
            moneyCase.Increment(bundle);
            for (int i = 0; i < 10; i++)
            {
                bundle = moneyCase.Increment(bundle);
            }
            Assert.True(moneyCase.IsFull);
            Assert.False(bundle.IsEmpty);
        }

        [Fact]
        public void MoneyCaseDecrementTest()
        {
            MoneyCase moneyCase = new MoneyCase(new MoneyType(100, MoneyCurrency.RUB), 100);
            MoneyBundle bundle = new MoneyBundle(new MoneyType(100, MoneyCurrency.RUB), 100);
            moneyCase.Increment(bundle);
            for (int i = 0; i < 10; i++)
            {
                bundle = moneyCase.Decrement(bundle);
            }
            Assert.False(moneyCase.IsFull);
            Assert.True(bundle.IsEmpty);
        }


        [Fact]
        public void MoneyCaseFailIncrementTest()
        {
            MoneyCase moneyCase = new MoneyCase(new MoneyType(100, MoneyCurrency.RUB), 100);
            MoneyBundle bundle = new MoneyBundle(new MoneyType(200, MoneyCurrency.RUB), 100);
            moneyCase.Increment(bundle);
            for (int i = 0; i < 10; i++)
            {
                moneyCase.Increment(bundle);
            }
            Assert.False(moneyCase.IsFull);
        }

        [Fact]
        public void MoneyCaseControllerIncrementTest()
        {
            MoneyCase moneyCase100 = new MoneyCase(new MoneyType(100, MoneyCurrency.RUB), 100);
            MoneyCase moneyCase200 = new MoneyCase(new MoneyType(200, MoneyCurrency.RUB), 100);
            MoneyCasesController moneyProvider = new MoneyCasesController(new MoneyType(0, MoneyCurrency.RUB), -1);
            moneyProvider.Add(moneyCase100);
            moneyProvider.Add(moneyCase200);

            List<MoneyBundle> moneyBundles = new List<MoneyBundle>() {
                new MoneyBundle(new MoneyType(200, MoneyCurrency.RUB), 100)
            };
            moneyProvider.Increment(moneyBundles);

            Assert.True(true);
        }
    }
}