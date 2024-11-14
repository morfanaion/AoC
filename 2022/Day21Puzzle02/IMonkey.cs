namespace Day21Puzzle02
{
    internal interface IMonkey
    {
        public static Dictionary<string, IMonkey> Monkeys { get; } = new Dictionary<string, IMonkey>();
        string Id { get; }
        long GetNumber();
        bool LinkedTo(string id);
        long GetNumberToYell(string id, long number);
        long GetNumberToYell(string id);
    }
}
