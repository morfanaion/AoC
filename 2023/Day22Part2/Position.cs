namespace Day22Part2
{
    internal class Position
    {
        public Position(string definition)
        {
            string[] parts = definition.Split(',');
            X = int.Parse(parts[0]);
            Y = int.Parse(parts[1]);
            Z = int.Parse(parts[2]);
        }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
    }
}
