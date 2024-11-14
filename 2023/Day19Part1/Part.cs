namespace Day19Part1
{
    internal class Part
    {
        public Part() { }

        public int X { get; set; }
        public int M { get; set; }
        public int A { get; set; }
        public int S { get; set; }

        public int XMAS => X + M + A + S;

        public bool? Accepted { get; set; }
    }
}
