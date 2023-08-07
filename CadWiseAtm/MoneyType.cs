﻿namespace CadWiseAtm
{
    public struct MoneyType
    {
        public bool IsEmpty => Currency == MoneyCurrency.NONE;
        public double Nominal { get; }
        public MoneyCurrency Currency { get; }

        public MoneyType()
        {
            Nominal = 0;
            Currency = MoneyCurrency.NONE;
        }

        public MoneyType(double value, MoneyCurrency currency)
        {
            this.Nominal = value;
            this.Currency = currency;
        }

        public static bool operator ==(MoneyType s1, MoneyType s2)
        {
            return s1.Currency == s2.Currency && s1.Nominal == s2.Nominal &&
                s1.IsEmpty == false && s2.IsEmpty == false;
        }

        public static bool operator !=(MoneyType s1, MoneyType s2)
        {
            return s1.Currency != s2.Currency || s1.Nominal != s2.Nominal &&
                (s1.IsEmpty == false || s2.IsEmpty);
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
