
namespace Day24Part1
{
    internal class Point(double x, double y, double z)
    {
        public double X { get; } = x;
        public double Y { get; } = y;
        public double Z { get; } = z;

        internal static Point FromDefinition(string definition)
        {
            string[] parts = definition.Split(',');
            return new Point(double.Parse(parts[0].Trim()), double.Parse(parts[1].Trim()), double.Parse(parts[2].Trim()));
        }

        public override string ToString() => $"({X}, {Y}";

        public Point NormalizedVector
        {
            get
            {
                // Calculate the magnitude of the vector
                double magnitude = Math.Sqrt(X * X + Y * Y);

                // Normalize the vector
                double normalizedX = X / magnitude;
                double normalizedY = Y / magnitude;

                return new Point(normalizedX, normalizedY, 0);
            }
        }

        public static Point operator -(Point p1, Point p2)
        {
            return new Point(p1.X - p2.X, p1.Y - p2.Y, p1.Z - p2.Z);
        }
    }
}
