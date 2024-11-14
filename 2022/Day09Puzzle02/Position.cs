namespace Day09Puzzle02
{
    internal class Position
    {
        public Position()
        {

        }

        public Position(Position other)
        {
            SetTo(other);
        }

        public int X { get; set; }
        public int Y { get; set; }

        public int DistanceFrom(Position position)
        {
            return Math.Max(Math.Abs(X - position.X), Math.Abs(Y - position.Y));
        }

        public void SetTo(Position position)
        {
            X = position.X;
            Y = position.Y;
        }

        public override bool Equals(object? obj)
        {
            if(obj is Position position)
            {
                return position.X == X && position.Y == Y;
            }
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return $"{X}|{Y}".GetHashCode();
        }

        public override string ToString()
        {
            return $"{X}, {Y}";
        }
    }
}
