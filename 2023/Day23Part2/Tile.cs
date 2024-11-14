namespace Day23Part2
{
    internal class Tile(TileType tileType, int x, int y)
    {
        private static int IdCounter = 0;
        public int Id { get; set; } = IdCounter++;
        public Tile? North { get; set; }
        public Tile? East { get; set; }
        public Tile? South { get; set; }
        public Tile? West { get; set; }
        public TileType TileType { get; set; } = tileType;

        public bool IsStart { get; set; }
        public bool IsEnd { get; set; }

        public int X { get; set; } = x;
        public int Y { get; set; } = y;

        public void SetNorth(Tile value)
        {
            North = value;
            value.South = this;
            if (North?.West?.South is Tile west)
            {
                SetWest(west);
            }
        }

        public void SetWest(Tile west)
        {
            West = west;
            West.East = this;
        }

        public void UnneighbourForests()
        {
            if (North?.TileType == TileType.Forest)
            {
                North.South = null;
                North = null;
            }
            if (East?.TileType == TileType.Forest)
            {
                East.West = null;
                East = null;
            }
            if (South?.TileType == TileType.Forest)
            {
                South.North = null;
                South = null;
            }
            if (West?.TileType == TileType.Forest)
            {
                West.East = null;
                West = null;
            }
        }

        internal IEnumerable<(Step step, int weight)> GetNeighbours(Step s)
        {
            foreach (Tile neighbour in AllNeighbours())
            {
                if (s.PreviousTileIds().Contains(neighbour.Id))
                {
                    continue;
                }
                yield return (new Step(neighbour, s.NumSteps + 1, s), int.MaxValue - (s.NumSteps + 1));
            }
        }

        private IEnumerable<Tile> AllNeighbours()
        {
            if (North != null && (North.TileType == TileType.Path || North.TileType == TileType.SlopeNorth))
            {
                yield return North;
            }
            if (South != null && (South.TileType == TileType.Path || South.TileType == TileType.SlopeSouth))
            {
                yield return South;
            }
            if (East != null && (East.TileType == TileType.Path || East.TileType == TileType.SlopeEast))
            {
                yield return East;
            }
            if (West != null && (West.TileType == TileType.Path || West.TileType == TileType.SlopeWest))
            {
                yield return West;
            }
        }

        public bool Travelled { get; set; } = false;

        public char GetChar()
        {
            if(Travelled)
            {
                return '0';
            }
            switch(TileType)
            {
                case TileType.Path: return '.';
                case TileType.Forest: return '#';
                case TileType.SlopeNorth: return '^';
                case TileType.SlopeEast: return '>';
                case TileType.SlopeWest: return '<';
                case TileType.SlopeSouth: return 'v';
            }
            throw new Exception("?");
        }

        internal void AddTrails()
        {
            Step start = new Step(this, 0, null);
            foreach (Tile neighbour in AllNeighbours())
            {
                Step step = new Step(neighbour, 1, start);
                while (step.CurrentTile.NumNeighbours <= 2 && !step.CurrentTile.IsEnd && !step.CurrentTile.IsStart)
                {
                    step = step.CurrentTile.GetNeighbours(step).Single().step;
                }
                Trails.Add(new Trail(step.CurrentTile, step.NumSteps));
            }
        }

        internal IEnumerable<(TrailStep s, int weight)> GetTrailNeighbours(TrailStep s)
        {
            foreach (Trail neighbour in Trails)
            {
                if (s.PreviousTileIds().Contains(neighbour.EndOfTrail.Id))
                {
                    continue;
                }
                yield return (new TrailStep(neighbour.EndOfTrail, s.NumSteps + neighbour.Weight, s), int.MaxValue - (s.NumSteps + neighbour.Weight));
            }
        }

        public List<Trail> Trails { get; } = new List<Trail>();

        public int NumNeighbours => AllNeighbours().Count();
    }
}
