namespace Day17Part1
{
    internal class CityBlock
    {
        private static int IdCounter = 0;
        public CityBlock()
        {
            Id = IdCounter++;
        }

        public int Id { get; set; }

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
                if(s.Last.Id == neighbour.Id)
                {
                    continue;
                }
                if((s.Last3rd.X == neighbour.X && Math.Abs(s.Last3rd.Y - neighbour.Y) > 3) ||
                    (s.Last3rd.Y == neighbour.Y && Math.Abs(s.Last3rd.X - neighbour.X) > 3))
                {
                    continue;
                }
                yield return (new Step() { CurrentBlock = neighbour, Last = s.CurrentBlock, Last2nd = s.Last, Last3rd = s.Last2nd }, neighbour.HeatLoss);
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
