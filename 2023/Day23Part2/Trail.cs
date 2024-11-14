namespace Day23Part2
{
    internal class Trail(Tile endOfTrail, int weight)
    {
        public Tile EndOfTrail { get; set; } = endOfTrail;

        public int Weight { get; set; } = weight;
    }
}
