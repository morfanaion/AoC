namespace Day21Puzzle02
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

        public bool LinkedTo(string id) => OtherMonkey1.LinkedTo(id) || OtherMonkey2.LinkedTo(id);

        public abstract long GetNumber();
        public abstract long DetermineNumberForMonkey1(long requiredResult);
        public abstract long DetermineNumberForMonkey2(long requiredResult);

        public long GetNumberToYell(string id, long number)
        {
            if (id == Id) return number;
            if (OtherMonkey1.LinkedTo(id))
            {
                return OtherMonkey1.GetNumberToYell(id, DetermineNumberForMonkey1(number));
            }
            else
            {
                return OtherMonkey2.GetNumberToYell(id, DetermineNumberForMonkey2(number));
            }
        }

        public long GetNumberToYell(string id)
        {
            if (OtherMonkey1.LinkedTo(id))
            {
                long requiredNumber = OtherMonkey2.GetNumber();
                return OtherMonkey1.GetNumberToYell(id, requiredNumber);
            }
            else
            {
                long requiredNumber = OtherMonkey1.GetNumber();
                return OtherMonkey2.GetNumberToYell(id, requiredNumber);
            }
        }
    }
}
