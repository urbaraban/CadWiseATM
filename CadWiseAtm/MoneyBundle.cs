namespace CadWiseAtm
{
    public readonly struct MoneyBundle
    {
        public readonly bool IsEmpty => MoneyType.IsEmpty || Count == 0;
        public MoneyType MoneyType { get; }
        public int Count { get; }

        public MoneyBundle()
        {
            MoneyType = new MoneyType();
            Count = 0;
        }

        public MoneyBundle(MoneyType moneyType, int count)
        {
            MoneyType = moneyType;
            Count = count;
        }

        public MoneyBundle(int nominal, MoneyCurrency currency, int count) : 
            this(new MoneyType(nominal, currency), count)
        { }

        public static MoneyBundle operator + (MoneyBundle s1, MoneyBundle s2)
        {
            if (s1.MoneyType == s2.MoneyType)
            {
                return new MoneyBundle(s1.MoneyType, s1.Count + s2.Count);
            }
            else
            {
                throw new ArgumentException("Invalid MonetType");
            }
        }

        public static MoneyBundle operator -(MoneyBundle s1, int s2)
        {
            if (s1.Count >= s2)
            {
                return new MoneyBundle(s1.MoneyType, s1.Count - s2);
            }
            else
            {
                throw new ArgumentException("Negative substraction");
            }
        }

        public static IEnumerable<MoneyBundle> Defrag(IEnumerable<MoneyBundle> moneyBundles)
        {
            var result = new List<MoneyBundle>();
            var groups = moneyBundles.GroupBy(x => x.MoneyType);

            foreach(var group in groups)
            {
                if (group.Count() > 0)
                {
                    MoneyBundle sum = group.ElementAt(0);
                    for (int i = 1; i < group.Count(); i += 1)
                    {
                        sum += group.ElementAt(i);
                    }
                    result.Add(sum);
                }
                
            }
            return result;
        }
    }
}
