namespace Day21Puzzle01
{
    internal class NumberMonkey : IMonkey
    {
        public string Id { get; set; }
        public long Number { get; set; }
        
        public NumberMonkey(string id, long number)
        {
            Id = id;
            Number = number;
        }

        public long GetNumber() => Number;
    }
}
