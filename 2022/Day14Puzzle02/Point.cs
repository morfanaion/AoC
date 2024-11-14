namespace Day14Puzzle01
{
    internal class Point
    {
        public int X;
        public int Y;

        public Point(string pointString)
        {
            string[] parts = pointString.Split(',');
            X = int.Parse(parts[0]);
            Y = int.Parse(parts[1]);
        }
    }
}
