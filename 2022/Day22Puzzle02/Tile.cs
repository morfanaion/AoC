namespace Day22Puzzle02
{
    internal class Tile
    {
        public static int Counter = 0;
        public int Id = Counter++;
        public TileType TileType { get; set; }
        public Tile Left { get; set; }
        public Tile Right { get; set; }
        public Tile Up { get; set; }
        public Tile Down { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public CubeFace? CubeFace { get; set; }

        public void SetCubeFace(CubeFace cubeFace)
        {
            CubeFace = cubeFace;
            CubeFace.Tiles.Add(this);
        }

        public Tile()
        {
            Left = this;
            Right = this;
            Up = this;
            Down = this;
        }

        public void SetLeft(Tile tile)
        {
            Left = tile;
            Right = tile.Right;
            tile.Right.Left = this;
            tile.Right = this;
        }

        public void SetUp(Tile tile)
        {
            Up = tile;
            Down = tile.Down;
            tile.Down.Up = this;
            tile.Down = this;
        }
    }
}
