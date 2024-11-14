namespace Day24Part2
{
    internal class Point(long x, long y, long z)
    {
        public long X { get; } = x;
        public long Y { get; } = y;
        public long Z { get; } = z;

        internal static Point FromDefinition(string definition)
        {
            string[] parts = definition.Split(',');
            return new Point(long.Parse(parts[0].Trim()), long.Parse(parts[1].Trim()), long.Parse(parts[2].Trim()));
        }

        public override string ToString() => $"({X}, {Y}, {Z})";

        public static Point operator -(Point p1, Point p2)
        {
            return new Point(p1.X - p2.X, p1.Y - p2.Y, p1.Z - p2.Z);
        }

        public static Point operator *(Point p, long number)
        {
            return new Point(p.X * number, p.Y * number, p.Z * number);
        }

        public static Point operator /(Point p, long number)
        {
            return new Point(p.X / number, p.Y / number, p.Z / number);
        }

        public static Point operator +(Point p1, Point p2)
        {
            return new Point(p1.X + p2.X, p1.Y + p2.Y, p1.Z + p2.Z);
        }

        public static bool operator ==(Point p1, Point p2)
        {
            return p1.X == p2.X && p1.Y == p1.Y && p1.Z == p2.Z;
        }

        public static bool operator !=(Point p1, Point p2)
        {
            return !(p1 == p2);
        }

        public override bool Equals(object? obj)
        {
            if (obj is Point other)
            {
                return this == other;
            }
            return false;
        }

        internal long AddAllDimensions() => X + Y + Z;

        public Point CrossProduct(Point other)
        {
            return new Point(
                Y * other.Z - Z * other.Y,
                Z * other.X - X * other.Z,
                X * other.Y - Y * other.X
            );
        }
    }
}
