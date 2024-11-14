namespace Day16Part2
{
    internal abstract class Tile
    {
        public static Tile CreateTile(char c)
        {
            switch(c)
            {
                case '.': return new Empty();
                case '|': return new VerticalSplitter();
                case '-': return new HorizontalSplitter();
                case '\\': return new LeftRightMirror();
                case '/': return new RightLeftMirror();
                default: throw new NotImplementedException();
            }

        }

        public Tile? North { get; set; }
        public Tile? East { get; set; }
        public Tile? South { get; set; }
        public Tile? West { get; set; }

        public int X { get; set; }
        public int Y { get; set; }

        public bool IsEnergized { get; set; }

        public Direction AlreadyApproachedTo { get; set; }

        public void SetNorth(Tile north)
        {
            North = north;
            North.South = this;
            if (North.West != null)
            {
                SetWest(North.West.South ?? throw new InvalidOperationException("If North has a West, West must have a South"));
            }
        }

        public void SetWest(Tile west)
        {
            West = west;
            west.East = this;
        }

        public abstract void Visit(Direction direction, Action<Tile?, Direction> addToQueue);
    }
}
