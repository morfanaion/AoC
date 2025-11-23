namespace Day09Puzzle01
{
    internal class City
    {
        internal string Name { get; set; }
        internal Dictionary<City, int> Distances { get; } = new Dictionary<City, int>();
    }
}
