namespace _2025_08_01
{
    internal class JunctionBox
    {
        public static JunctionBox FromString(string text)
        {
            string[] parts = text.Split(',');
            JunctionBox newBox = new JunctionBox() {  X = long.Parse(parts[0]), Y = long.Parse(parts[1]), Z = long.Parse(parts[2]) };
            newBox.Circuit = new Circuit(newBox);
            return newBox;
        }

        public long X { get; set; }
        public long Y { get; set; }
        public long Z { get; set; }
        public Circuit Circuit { get; internal set; }

        public long SquaredDistanceTo(JunctionBox other)
        {
            return (long)(Math.Pow(X - other.X, 2) + Math.Pow(Y - other.Y, 2) + Math.Pow(Z - other.Z, 2));
        }

        public override string ToString()
        {
            return $"{X}, {Y}, {Z}";
        }
    }
}
