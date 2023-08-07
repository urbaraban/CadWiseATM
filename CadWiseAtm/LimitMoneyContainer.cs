namespace CadWiseAtm
{
    public abstract class LimitMoneyContainer
    {
        public abstract double Sum { get; }

        protected virtual int ItemsCount { get; set; }
        public int ItemsMaximum { get; }
        public bool IsFull => ItemsMaximum == ItemsCount;

        public LimitMoneyContainer(int maximumCount)
        {
            ItemsMaximum = maximumCount;
        }
    }
}
