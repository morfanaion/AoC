﻿namespace Day21Puzzle01
{
    internal class AdditionMonkey : BaseOperationMonkey
    {
        public AdditionMonkey(string id, string otherMonkey1Id, string otherMonkey2Id) : base(id, otherMonkey1Id, otherMonkey2Id)
        {
        }

        private long? _number;
        public override long GetNumber() => _number ??= OtherMonkey1.GetNumber() + OtherMonkey2.GetNumber();
    }
}
