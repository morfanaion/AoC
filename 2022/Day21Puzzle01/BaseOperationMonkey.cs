namespace Day21Puzzle01
{
    internal abstract class BaseOperationMonkey : IMonkey
    {
        public string Id { get; set; }
        public string OtherMonkey1Id { get; set; }
        public string OtherMonkey2Id { get; set; }
        public IMonkey OtherMonkey1 => IMonkey.Monkeys[OtherMonkey1Id];
        public IMonkey OtherMonkey2 => IMonkey.Monkeys[OtherMonkey2Id];

        public BaseOperationMonkey(string id, string otherMonkey1Id, string otherMonkey2Id)
        {
            Id = id;
            OtherMonkey1Id = otherMonkey1Id;
            OtherMonkey2Id = otherMonkey2Id;
        }

        public abstract long GetNumber();
    }
}
