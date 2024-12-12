namespace Day12Puzzle02
{
    internal class Plot
    {
        private static long PlotIdCounter = 0;

        public int X { get; set; }
        public int Y { get; set; }
        public long RegionId { get; set; } = PlotIdCounter++;
        public char PlotType { get; set; }
        public int Perimeter { get; set; } = 4;
        public bool FenceNorth { get; set; } = true;
        public bool FenceSouth { get; set; } = true;
        public bool FenceEast { get; set; } = true;
        public bool FenceWest { get; set; } = true;
    }
}
