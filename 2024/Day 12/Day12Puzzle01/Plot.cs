namespace Day12Puzzle01
{
    internal class Plot
    {
        private static long PlotIdCounter = 0;

        public long RegionId { get; set; } = PlotIdCounter++;
        public char PlotType { get; set; }
        public int Perimeter { get; set; } = 4;
    }
}
