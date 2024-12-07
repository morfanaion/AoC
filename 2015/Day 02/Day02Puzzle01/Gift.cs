namespace Day02Puzzle01
{
    internal class Gift
    {
        public static Gift FromString(string str)
        {
            string[] parts = str.Split('x', StringSplitOptions.RemoveEmptyEntries);
            return new Gift()
            {
                Length = Convert.ToInt32(parts[0]),
                Width = Convert.ToInt32(parts[1]),
                Height = Convert.ToInt32(parts[2])
            };
        }

        public int Width;
        public int Height;
        public int Length;

        private int[]? _sideSizes;
        public int[] SideSizes => _sideSizes ??= [Width * Length, Width * Height, Length * Height];

        public int WrapSize => SideSizes.Sum(s => s * 2) + SideSizes.Min();
    }
}
