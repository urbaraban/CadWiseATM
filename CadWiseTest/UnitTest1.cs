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
            var moneyCase = new MoneyCase(new MoneyType(100, MoneyCurrency.RUB), 100);
            var bundle = new MoneyBundle(new MoneyType(200, MoneyCurrency.RUB), 100);
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
            IEnumerable<MoneyCase> cases = new MoneyCase[]
            {
                new MoneyCase(new MoneyType(100, MoneyCurrency.RUB), 100),
                new MoneyCase(new MoneyType(100, MoneyCurrency.RUB), 100)
            };

            var moneyCases = new MoneyCasesController(cases, new MoneyType(100, MoneyCurrency.RUB));

            var bundle = new MoneyBundle(new MoneyType(100, MoneyCurrency.RUB), 150);

            bundle = moneyCases.Increment(bundle);

            Assert.True(bundle.IsEmpty);
        }

        [Fact]
        public void MoneyBundleUnionOne()
        {
            List<MoneyBundle> moneyBundles = new List<MoneyBundle>() {
                new MoneyBundle(new MoneyType(200, MoneyCurrency.RUB), 100),
                new MoneyBundle(new MoneyType(200, MoneyCurrency.RUB), 100),
                new MoneyBundle(new MoneyType(200, MoneyCurrency.RUB), 100)
            };
            IEnumerable<MoneyBundle> mb = MoneyBundle.Defrag(moneyBundles);

            Assert.True(mb.Count() == 1);
        }

        [Fact]
        public void MoneyBundleUnionTwo()
        {
            List<MoneyBundle> moneyBundles = new List<MoneyBundle>() {
                new MoneyBundle(new MoneyType(200, MoneyCurrency.RUB), 100),
                new MoneyBundle(new MoneyType(200, MoneyCurrency.RUB), 100),
                new MoneyBundle(new MoneyType(100, MoneyCurrency.RUB), 100)
            };
            IEnumerable<MoneyBundle> mb = MoneyBundle.Defrag(moneyBundles);

            Assert.True(mb.Count() == 2);
        }

        [Fact]
        public void MoneyBundleUnionDef()
        {
            List<MoneyBundle> moneyBundles = new List<MoneyBundle>() {
                new MoneyBundle(new MoneyType(100, MoneyCurrency.RUB), 100),
                new MoneyBundle(new MoneyType(200, MoneyCurrency.RUB), 100),
                new MoneyBundle(new MoneyType(100, MoneyCurrency.RUB), 100)
            };
            IEnumerable<MoneyBundle> mb = MoneyBundle.Defrag(moneyBundles);

            Assert.True(mb.Count() == 2);
        }
    }
}