namespace Day21Puzzle01
{
    internal interface IMonkey
    {
        public static Dictionary<string, IMonkey> Monkeys { get; } = new Dictionary<string, IMonkey>();
        string Id { get; }
        long GetNumber();
    }
}
