namespace Day09Part2
{
    internal class Sequence : List<long>
    {
        private Sequence? SubSequence { get; set; }

        public Sequence(string definition)  : this(definition.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(s => long.Parse(s)))
        { 
            
        }

        public Sequence(IEnumerable<long> numbers) : base(numbers)
        {
            if (!this.All(l => l == 0))
            {
                SubSequence = new Sequence(this.Skip(1).Zip(this, (curr, prev) => curr - prev));
            }
        }

        internal void ExtrapolatePreviousNumber()
        {
            Reverse();
            if (SubSequence != null)
            {
                SubSequence.ExtrapolatePreviousNumber();
                Add(this.Last() - SubSequence.First());
            }
            else
            {
                Add(0);
            }
            Reverse();
        }
    }
}
