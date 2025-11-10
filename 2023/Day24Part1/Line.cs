namespace Day24Part1
{
    internal class Line(Point startPoint, Point direction)
    {
        private static int IdCounter = 0;
        public int Id { get; set; } = IdCounter++;
        public Point StartPoint { get; } = startPoint;
        public Point Direction { get; } = direction;

        public double Slope => Direction.Y / Direction.X;
        public double YIntercept => StartPoint.Y - Slope * StartPoint.X;

        public static Line FromDefinition(string str)
        {
            string[] parts = str.Split("@");
            return new Line(Point.FromDefinition(parts[0]), Point.FromDefinition(parts[1]));
        }

        public Point? FindIntersection(Line line)
        {
            // Calculate x-coordinate of the intersection point
            double x = (line.YIntercept - YIntercept) / (Slope - line.Slope);

            // Calculate y-coordinate of the intersection point
            double y = Slope * (x - StartPoint.X) + StartPoint.Y;

            Point intersection = new Point(x, y, 0);

            // Check if the intersection point lies within the vector's range for both lines
            if (IsPointInVectorRange(intersection, this) && IsPointInVectorRange(intersection, line))
            {
                return intersection;
            }
            else
            {
                // Return null or handle the case where there is no intersection in the vector direction
                return null;
            }
        }

        public static bool IsPointInVectorRange(Point point, Line line)
        {
            // Normalize both vectors
            Point normalizedPoint = (point - line.StartPoint).NormalizedVector;
            Point normalizedVector = line.Direction.NormalizedVector;

            // Check if the normalized vectors are the same
            double tolerance = 0.000001;
            return Math.Abs(normalizedPoint.X - normalizedVector.X) < tolerance
                && Math.Abs(normalizedPoint.Y - normalizedVector.Y) < tolerance;
        }


    }
}
