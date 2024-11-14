namespace Day23Puzzle01
{
    internal struct Position
    {
        public int X { get; set; }
        public int Y { get; set; }

        internal bool WithinRange(Position position, int range) => Math.Abs(X - position.X) <= range && Math.Abs(Y - position.Y) <= range;
    }
}
