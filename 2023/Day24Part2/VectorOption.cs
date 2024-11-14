namespace Day24Part2
{
    internal class VectorOption
    {
        public VectorOption(Point startPoint, Point vector, long time1, long time2)
        {
            Point travellingVector = (vector - startPoint) / (time2 - time1);
            Line = new Line(startPoint - (travellingVector * time1), travellingVector);
        }

        public Line Line { get; }
    }
}
