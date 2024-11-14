namespace Day17Part2
{
    internal class CityBlock
    {
        private static int IdCounter = 0;
        public CityBlock()
        {
            Id = IdCounter++;
        }

        public int Id { get; set; }
        private char _path = '\0';
        public char Path
        {
            get
            {
                if (_path == '\0')
                {
                    return (char)(HeatLoss + '0');
                }
                return _path;
            }
            set
            {
                _path = value;
            }
        }

        public CityBlock? North { get; set; }
        public CityBlock? East { get; set; }
        public CityBlock? South { get; set; }
        public CityBlock? West { get; set; }

        public int X { get; set; }
        public int Y { get; set; }

        public int HeatLoss { get; set; }
        public bool IsStart { get; set; }
        public bool IsEnd { get; set; }
        
        public void SetNorth(CityBlock north)
        {
            North = north;
            North.South = this;
            if (North.West != null)
            {
                SetWest(North.West.South ?? throw new InvalidOperationException("If North has a West, West must have a South"));
            }
        }

        public void SetWest(CityBlock west)
        {
            West = west;
            west.East = this;
        }

        internal IEnumerable<(Step s, int weight)> GetNeighbours(Step s)
        {
            foreach(CityBlock neighbour in AllNeighbours())
            {
                if (s.Last10th.Id != Id)
                {
                    if (s.Last.Id == neighbour.Id)
                    {
                        continue;
                    }
                    if (s.Last.X != neighbour.X && s.Last.Y != neighbour.Y && s.CurrentBlock.X != s.Last4th.X && s.CurrentBlock.Y != s.Last4th.Y)
                    {
                        continue;
                    }
                    if ((s.Last10th.X == neighbour.X && Math.Abs(s.Last10th.Y - neighbour.Y) > 10) ||
                        (s.Last10th.Y == neighbour.Y && Math.Abs(s.Last10th.X - neighbour.X) > 10))
                    {
                        continue;
                    }
                    if (neighbour.IsEnd && neighbour.X != s.Last3rd.X && neighbour.Y != s.Last3rd.Y)
                    {
                        continue;
                    }
                }
                yield return (s.NextStep(neighbour), neighbour.HeatLoss);
            }
        }

        private IEnumerable<CityBlock> AllNeighbours()
        {
            if(North != null)
            {
                yield return North;
            }
            if (South != null)
            {
                yield return South;
            }
            if (East != null)
            {
                yield return East;
            }
            if (West != null)
            {
                yield return West;
            }
        }
    }
}
