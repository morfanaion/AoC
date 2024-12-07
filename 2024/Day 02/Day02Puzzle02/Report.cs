namespace Day02Puzzle02
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
                return Permutations.Any(DetermineSafety);
            }
        }

        public IEnumerable<IEnumerable<int>> Permutations
        {
            get
            {
                yield return Levels;
                for (int i = 0; i < Levels.Count; i++)
                {
                    yield return Levels.Take(i).Concat(Levels.Skip(i + 1));
                }
            }
        }


        public static bool DetermineSafety(IEnumerable<int> items)
        {
            bool? ascending = null;
            foreach ((int next, int previous) in items.Skip(1).Zip(items))
            {
                if (!ascending.HasValue)
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
                if (diff < 1 || diff > 3)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
