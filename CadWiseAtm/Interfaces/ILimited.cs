namespace CadWiseAtm.Interfaces
{
    internal interface ILimited
    {
        public bool IsFull { get; }
        public int Count { get; }
        public int Limit { get; }
    }
}
