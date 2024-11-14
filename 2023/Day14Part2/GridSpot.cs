namespace Day14Part2
{
    internal class GridSpot
    {
        public GridSpot? North { get; set; }
        public GridSpot? East { get; set; }
        public GridSpot? South { get; set; }
        public GridSpot? West { get; set; }

        public RockType RockType { get; set; }

        public GridSpot(char c)
        {
            switch (c)
            {
                case 'O':
                    RockType = RockType.Round;
                    break;
                case '#':
                    RockType = RockType.Cube;
                    break;
                case '.':
                    RockType = RockType.None;
                    break;
            }
        }

        public void SetNorth(GridSpot north)
        {
            North = north;
            North.South = this;
            if(North.West != null)
            {
                SetWest(North.West.South ?? throw new InvalidOperationException("If North has a West, West must have a South"));
            }
        }

        public void SetWest(GridSpot west)
        {
            West = west;
            west.East = this;
        }
    }
}
