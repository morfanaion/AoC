namespace Day18Puzzle02
{
    internal class CubeEntry
    {
        public int X;
        public int Y;
        public int Z;

        internal int ExposedSides(List<CubeEntry> cubeEntries)
        {
            int exposedSides = 0;
            if(!CheckPosition(X + 1, Y, Z, cubeEntries))
            {
                exposedSides++;
            }
            if (!CheckPosition(X - 1, Y, Z, cubeEntries))
            {
                exposedSides++;
            }
            if (!CheckPosition(X, Y + 1, Z, cubeEntries))
            {
                exposedSides++;
            }
            if (!CheckPosition(X, Y -1 , Z, cubeEntries))
            {
                exposedSides++;
            }
            if (!CheckPosition(X, Y, Z + 1, cubeEntries))
            {
                exposedSides++;
            }
            if (!CheckPosition(X, Y, Z - 1, cubeEntries))
            {
                exposedSides++;
            }
            return exposedSides;
        }

        public static bool CheckPosition(int x, int y, int z, List<CubeEntry> cubeEntries) => cubeEntries.Any(entry => entry.X == x && entry.Y == y && entry.Z == z);



        public CubeEntry(string str)
        {
            string[] parts = str.Split(',');
            X = int.Parse(parts[0]) + 2;
            Y = int.Parse(parts[1]) + 2;
            Z = int.Parse(parts[2]) + 2;
        }

    }
}
