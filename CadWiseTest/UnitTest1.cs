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
        public void MoneyCaseControllerCheckMoneyType()
        {
            IEnumerable<MoneyCase> cases = new MoneyCase[]
            {
                new MoneyCase(new MoneyType(100, MoneyCurrency.RUB), 100),
                new MoneyCase(new MoneyType(100, MoneyCurrency.RUB), 100)
            };

            var moneyCases = new MoneyCasesController(cases, new MoneyType(100, MoneyCurrency.RUB));
            var bundle = new MoneyBundle(100, MoneyCurrency.RUB, 150);

            Assert.True(moneyCases.CheckMoneyType(bundle.MoneyType));
        }


        [Fact]
        public void MoneyCaseControllerCheckMoneyTypeFail()
        {
            IEnumerable<MoneyCase> cases = new MoneyCase[]
            {
                new MoneyCase(new MoneyType(100, MoneyCurrency.RUB), 100),
                new MoneyCase(new MoneyType(100, MoneyCurrency.RUB), 100)
            };

            var moneyCases = new MoneyCasesController(cases, new MoneyType(100, MoneyCurrency.RUB));

            var bundle = new MoneyBundle(200, MoneyCurrency.RUB, 150);

            Assert.False(moneyCases.CheckMoneyType(bundle.MoneyType));
        }

        [Fact]
        public void MoneyCaseControllerCheckMoneyTypeFail2()
        {
            IEnumerable<MoneyCase> cases = new MoneyCase[]
            {
                new MoneyCase(new MoneyType(100, MoneyCurrency.RUB), 100),
                new MoneyCase(new MoneyType(100, MoneyCurrency.RUB), 100)
            };

            var moneyCases = new MoneyCasesController(cases, new MoneyType(100, MoneyCurrency.RUB));

            var bundle = new MoneyBundle(100, MoneyCurrency.EUR, 150);

            Assert.False(moneyCases.CheckMoneyType(bundle.MoneyType));
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

        [Fact]
        public void TestATMMoneyLimit()
        {
            ATM atm = new ATM(8);
            for (int i = 0; i < 8; i += 1)
            {
                Assert.True(atm.Add(new MoneyCase(100, MoneyCurrency.RUB, 100, 100)));
            }

            Assert.False(atm.Add(new MoneyCase(100, MoneyCurrency.RUB, 100, 100)));
            Assert.True(atm.IsFullMoney);
        }

        [Fact]
        public void TestATMAddCase()
        {
            ATM atm = new ATM(7);
            for (int i = 1; i < 8; i += 1)
            {
                Assert.True(atm.Add(new MoneyCase(i * 100, MoneyCurrency.RUB, 100, 100)));
            }

            Assert.False(atm.Add(new MoneyCase(100, MoneyCurrency.RUB, 100, 100)));
            Assert.True(atm.IsFullMoney);
        }

        [Fact]
        public void TestATMAddMoney()
        {
            ATM atm = new ATM(7);
            for (int i = 1; i < 8; i += 1)
            {
                Assert.True(atm.Add(new MoneyCase(i * 100, MoneyCurrency.RUB, 100)));
            }

            List<MoneyBundle> bundles = new List<MoneyBundle>();

            for (int i = 1; i < 8; i += 1)
            {
                bundles.Add(new MoneyBundle(i * 100, MoneyCurrency.RUB, 100));
            }
            Assert.True(atm.Increment(bundles).Count() == 0);

            Assert.False(atm.Add(new MoneyCase(100, MoneyCurrency.RUB, 100, 100)));
            Assert.True(atm.IsFullMoney);
        }


        [Fact]
        public void TestATMCheckSum()
        {
            ATM atm = new ATM(7);
            for (int i = 1; i < 6; i += 1)
            {
                Assert.True(atm.Add(new MoneyCase(i * 100, MoneyCurrency.RUB, 100)));
            }

            Assert.True(atm.Add(new MoneyCase(100, MoneyCurrency.EUR, 100, 50)));
            Assert.True(atm.Add(new MoneyCase(100, MoneyCurrency.USD, 100, 45)));

            List<MoneyBundle> bundles = new List<MoneyBundle>();

            for (int i = 1; i < 8; i += 1)
            {
                bundles.Add(new MoneyBundle(i * 100, MoneyCurrency.RUB, 100));
            }
            Assert.False(atm.Increment(bundles).Count() == 0);

            Assert.Equal(atm.GetSum(MoneyCurrency.EUR), 5000, 5);
            Assert.Equal(atm.GetSum(MoneyCurrency.USD), 4500, 5);
            Assert.Equal(atm.GetSum(MoneyCurrency.RUB), 150000, 5);

            Assert.False(atm.Add(new MoneyCase(100, MoneyCurrency.RUB, 100, 100)));
            Assert.False(atm.IsFullMoney);
        }

        [Fact]
        public void TestATMLimit()
        {
            ATM atm = new ATM(8);
            Assert.False(atm.IsFull);
            Assert.True(atm.IsFullMoney);
        }

        [Fact]
        public void TestATMDecrement()
        {
            ATM atm = new ATM(8);

            for (int i = 1; i < 6; i += 1)
            {
                Assert.True(atm.Add(new MoneyCase(i * 100, MoneyCurrency.RUB, 100, 100)));
            }
            Assert.Equal(atm.GetSum(MoneyCurrency.RUB), 150000, 5);
            atm.Decrement(300, MoneyCurrency.RUB);
            Assert.Equal(atm.GetSum(MoneyCurrency.RUB), 149700, 5);

            Assert.False(atm.IsFull);
            Assert.False(atm.IsFullMoney);
        }

        [Fact]
        public void TestATMDecrement2()
        {
            ATM atm = new ATM(8);

            Assert.True(atm.Add(new MoneyCase(10, MoneyCurrency.RUB, 100, 100)));
            Assert.True(atm.Add(new MoneyCase(50, MoneyCurrency.RUB, 100, 100)));
            Assert.True(atm.Add(new MoneyCase(100, MoneyCurrency.RUB, 100, 3)));
            Assert.True(atm.Add(new MoneyCase(500, MoneyCurrency.RUB, 100, 100)));

            Assert.Equal(atm.GetSum(MoneyCurrency.RUB), 56300);
            var result = atm.Decrement(435, MoneyCurrency.RUB);
            Assert.False(result.Item1);
            Assert.Equal(atm.GetSum(MoneyCurrency.RUB), 56300);

            var ost = atm.Decrement(result.Item2);
            Assert.True(ost.Count() == 0);
            Assert.Equal(atm.GetSum(MoneyCurrency.RUB), 55870);

            Assert.False(atm.IsFull);
            Assert.False(atm.IsFullMoney);
        }
    }
}