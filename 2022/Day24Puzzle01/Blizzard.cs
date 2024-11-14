namespace Day24Puzzle01
{
    internal class Blizzard
    {
        public static List<Blizzard> Blizzards { get; } = new List<Blizzard>();
        public char Direction { get; set; }
        public Tile Tile { get; set; }
        public Tile PreviousTile { get; set; }
        public Blizzard(Tile tile)
        {
            Tile = PreviousTile = tile;
        }

        public void Move()
        {
            PreviousTile = Tile;
            switch(Direction)
            {
                case '<':
                    Tile = Tile.Left;
                    break;
                case '>':
                    Tile = Tile.Right;
                    break;
                case '^':
                    Tile = Tile.Up;
                    break;
                case 'v':
                    Tile = Tile.Down;
                    if(Tile.Down.IsTargetTile)
                    {
                        Tile = Tile.Down;
                    }
                    break;
            }
        }
    }
}
