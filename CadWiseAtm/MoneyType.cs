namespace CadWiseAtm
{
    public struct MoneyType
    {
        public bool IsEmpty => Currency == MoneyCurrency.NONE;
        public int Nominal { get; }
        public MoneyCurrency Currency { get; }

        public MoneyType()
        {
            Nominal = -1;
            Currency = MoneyCurrency.NONE;
        }

        public MoneyType(int nominal, MoneyCurrency currency)
        {
            this.Nominal = nominal;
            this.Currency = currency;
        }

        public static bool operator ==(MoneyType s1, MoneyType s2)
        {
            return s1.Equals(s2);
        }

        public static bool operator !=(MoneyType s1, MoneyType s2)
        {
            return s1.Currency != s2.Currency || s1.Nominal != s2.Nominal &&
                (s1.IsEmpty == false || s2.IsEmpty);
        }

        public override bool Equals(object obj)
        {
            if (obj is MoneyType type)
            {
                return this.Currency == type.Currency && this.Nominal == type.Nominal &&
                this.IsEmpty == false && type.IsEmpty == false;
            }
            return false;
        }

        public override string ToString()
        {
            return $"{this.Nominal} {this.Currency}";
        }
    }

    public enum MoneyCurrency
    {
        USD,
        EUR,
        RUB,
        NONE
    }
}
