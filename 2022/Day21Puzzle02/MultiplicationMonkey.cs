namespace Day21Puzzle02
{
    internal class MultiplicationMonkey : BaseOperationMonkey
    {
        public MultiplicationMonkey(string id, string otherMonkey1Id, string otherMonkey2Id) : base(id, otherMonkey1Id, otherMonkey2Id)
        {
        }

        private long? _number;
        public override long GetNumber() => _number ??= OtherMonkey1.GetNumber() * OtherMonkey2.GetNumber();

        public override long DetermineNumberForMonkey1(long requiredResult) => requiredResult / OtherMonkey2.GetNumber();

        public override long DetermineNumberForMonkey2(long requiredResult) => requiredResult / OtherMonkey1.GetNumber();
    }
}
