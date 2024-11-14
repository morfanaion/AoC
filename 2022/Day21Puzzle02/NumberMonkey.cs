namespace Day21Puzzle02
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

        public bool LinkedTo(string id) => id == Id;

        public long GetNumberToYell(string id) => throw new InvalidOperationException("Should never be called");

        public long GetNumberToYell(string id, long number) => id == Id ? number : throw new ArgumentException("Numbermonkeys can only respond to this if you adress them with their Id");
    }
}
