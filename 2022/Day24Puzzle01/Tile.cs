namespace Day24Puzzle01
{
    internal class Tile
    {
        public static int MaxX { get; set; }
        public static int MaxY { get; set; }

        public bool IsWall { get; set; }
        public bool IsStartingTile { get; set; }
        public bool IsTargetTile { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public Tile Left { get; set; }
        public Tile Right { get; set; }
        public Tile Up { get; set; }
        public Tile Down { get; set; }

        public Tile()
        {
            Left = this;
            Right = this;
            Up = this;
            Down = this;
        }

        public void SetLeft(Tile tile)
        {
            if (IsWall) return;
            if (tile.IsWall) return;
            Left = tile;
            Right = tile.Right;
            tile.Right.Left = this;
            tile.Right = this;
        }

        public void SetUp(Tile tile)
        {
            if (IsWall) return;
            if (tile.IsWall) return;
            Up = tile;
            Down = tile.Down;
            tile.Down.Up = this;
            tile.Down = this;
        }

        public bool AnyBlizzardsHere => Blizzard.Blizzards.Any(b => b.Tile.Equals(this));
        public IEnumerable<Blizzard> Blizzards => Blizzard.Blizzards.Where(b => b.Tile.Equals(this));

        public IEnumerable<Tile> TraversableTiles
        {
            get
            {
                if (Left.X < X)
                {
                    yield return Left;
                }
                if (Right.X > X)
                {
                    yield return Right;
                }
                if (Up.Y < Y)
                {
                    yield return Up;
                }
                if (Down.Y > Y)
                {
                    yield return Down;
                }
                yield return this;
            }
        }
    }
}
