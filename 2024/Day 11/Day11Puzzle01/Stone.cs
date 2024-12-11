namespace Day11Puzzle01
{
    internal class Stone
    {
        public Stone? Previous { get; set; }
        public Stone? Next { get; set; }
        public long Number {  get; set; }

        public void SetPrevious(Stone previous)
        {
            Stone? next = previous.Next;
            previous.Next = this;
            Previous = previous;
            Next = next;
            if (next != null)
            {
                next.Previous = this;
            }
        }

        public IEnumerable<Stone> AllSubsequentStones
        {
            get
            {
                Stone current = this;
                yield return current;
                while (current.Next != null)
                {
                    yield return current = current.Next;
                }
            }
        }

        public static void Blink(Stone first)
        {
            Stone? current = first;
            while(current != null)
            {
                Stone? next = current.Next;
                if (current.Number == 0)
                {
                    current.Number = 1;
                    current = next;
                    continue;
                }
                int numDigits = current.Number.ToString().Length;
                int halfDigits = numDigits / 2;
                if (halfDigits * 2 == numDigits)
                {
                    long divider = (long)Math.Pow(10, halfDigits);
                    long number1 = current.Number / divider;
                    long number2 = current.Number - (divider * number1);
                    current.Number = number1;
                    new Stone() { Number = number2 }.SetPrevious(current);
                    current = next;
                    continue;
                }
                current.Number *= 2024;
                current = next;
            }
        }
    }
}
