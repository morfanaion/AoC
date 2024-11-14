namespace Day20Puzzle01
{
    internal class Number
    {
        private static int _counter = 0;
        public int OriginalPosition { get; private set; }
        public int Value { get; set; }
        public Number Next {get;set;}
        public Number Prev { get; set; }

        public Number(int value, Number? prev)
        {
            OriginalPosition = _counter++;
            Value = value;
            if (prev == null)
            {
                Next = this;
                Prev = this;
            }
            else
            {
                Prev = prev;
                Next = prev.Next;
                prev.Next = this;
                Next.Prev = this;
            }
        }

        public static void RemoveNumber(Number number)
        {
            number.Prev.Next = number.Next;
            number.Next.Prev = number.Prev;
        }

        public static void InsertNumber(Number number, Number previous)
        {
            number.Prev = previous;
            number.Next = previous.Next;
            number.Next.Prev = number;
            previous.Next = number;
        }
    }
}
