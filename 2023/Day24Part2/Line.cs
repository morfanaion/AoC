namespace Day24Part2
{
    internal class Line
    {
        private static int IdCounter = 0;
        public int Id { get; set; } = IdCounter++;
        public Point StartPoint { get; }
        public Point Direction { get; }

        public double Slope => Direction.Y / Direction.X;
        public double YIntercept => StartPoint.Y - Slope * StartPoint.X;

        public override string ToString() => $"{StartPoint} @ {Direction}";
        public Line(Point startPoint, Point direction)
        {
            StartPoint = startPoint;
            Direction = direction;
        }

        public static Line FromDefinition(string str)
        {
            string[] parts = str.Split("@");
            return new Line(Point.FromDefinition(parts[0]), Point.FromDefinition(parts[1]));
        }

        public (long time, Point)? Intersect(Line other)
        {
            long time = 0;
            if (other.Direction.X != Direction.X)
            {
                time = (other.StartPoint.X - StartPoint.X) / (Direction.X - other.Direction.X);
            }
            else if (other.Direction.Y != Direction.Y)
            {
                time = (other.StartPoint.Y - StartPoint.Y) / (Direction.Y - other.Direction.Y);
            }
            else if (other.Direction.Z != Direction.Z)
            {
                time = (other.StartPoint.Z - StartPoint.Z) / (Direction.Z - other.Direction.Z);
            }
            else
            {
                if (other.StartPoint == StartPoint)
                {
                    return (0, StartPoint);
                }
                else
                {
                    return null;
                }
            }

            time = Math.Abs(time);
            Point intersect = StartPoint + Direction * time;
            if (intersect.Equals(other.StartPoint + other.Direction * time))
            {
                return ((long)time, intersect);
            }
            else
            {
                return null;
            }
        }
    }
}
