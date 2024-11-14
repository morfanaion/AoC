namespace Day24Part2
{
    internal class DoubleVector(double x, double y, double z)
    {
        public double X { get; set; } = x;
        public double Y { get; set; } = y;
        public double Z { get; set; } = z;

        public override bool Equals(object? obj)
        {
            if(obj is DoubleVector other)
            {
                double tolerance = 0.000001;
                return Math.Abs(other.X - X) < tolerance
                    && Math.Abs(other.Y - Y) < tolerance
                    && Math.Abs(other.Z - Z) < tolerance;
            }
            return false;
        }

        public bool IsOpposite(DoubleVector other)
        {
            double tolerance = 0.000001;
            return Math.Abs(other.X + X) < tolerance
                && Math.Abs(other.Y + Y) < tolerance
                && Math.Abs(other.Z + Z) < tolerance;
        }

        // Calculate the dot product of two vectors
        public static double Dot(DoubleVector v1, DoubleVector v2) => v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;

        // Calculate the cross product of two vectors
        public static DoubleVector Cross(DoubleVector v1, DoubleVector v2)
        {
            double x = v1.Y * v2.Z - v1.Z * v2.Y;
            double y = v1.Z * v2.X - v1.X * v2.Z;
            double z = v1.X * v2.Y - v1.Y * v2.X;

            return new DoubleVector(x, y, z);
        }

        // Calculate the squared length of the vector
        public double LengthSquared() => X * X + Y * Y + Z * Z;

        // Normalize the vector
        public DoubleVector Normalize()
        {
            double length = Math.Sqrt(LengthSquared());
            if (length > 0)
            {
                return new DoubleVector(X / length, Y / length, Z / length);
            }
            else
            {
                // Handle zero vector case
                return new DoubleVector(0, 0, 0);
            }
        }

        public bool EitherOppositeOrEquals(DoubleVector other) => IsOpposite(other) || Equals(other);

        public override int GetHashCode() => base.GetHashCode();
    }
}
