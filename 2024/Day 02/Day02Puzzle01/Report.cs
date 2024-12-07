namespace Day02Puzzle01
{
    internal class Report
    {
        public static Report FromString(string str)
        {
            Report result = new Report();
            string[] parts = str.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            result.Levels.AddRange(parts.Select(int.Parse));
            return result;
        }

        List<int> Levels { get; } = new List<int>();

        public bool Safe
        {
            get
            {
                bool? ascending = null;
                foreach((int next, int previous) in Levels.Skip(1).Zip(Levels))
                {
                    if(!ascending.HasValue)
                    {
                        ascending = next > previous;
                    }
                    if (ascending.Value)
                    {
                        if (next < previous)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (next > previous)
                        {
                            return false;
                        }
                    }
                    int diff = Math.Abs(next - previous);
                    if(diff < 1 || diff > 3)
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
