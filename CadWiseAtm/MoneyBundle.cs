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
            foreach (var bundle in moneyBundles)
            {
                IEnumerable<MoneyBundle> collection = moneyBundles.Where(x => x.MoneyType == bundle.MoneyType);
                if (collection.Count() == 1)
                {
                    result.AddRange(collection);
                }
                else if (collection.Count() > 1)
                {
                    MoneyBundle union = collection.ElementAt(0);
                    for (int i = 1; i < collection.Count(); i += 1)
                    {
                        union += collection.ElementAt(i);
                    }
                    result.Add(union);
                }
            }
            return result;
        }
    }
}
