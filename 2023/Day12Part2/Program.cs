/*using Day12Part1;

List<SpringCollection> groups = File.ReadAllLines("input.txt").Select(l => new SpringCollection(l)).ToList();
//groups[28].FindSolves();
ThreadPool.SetMaxThreads(1000, 1000);
Task[] tasks = groups.Select(g => Task.Run(() => g.FindSolves())).ToArray();
Task.WaitAll(tasks);
Console.WriteLine(groups.Sum(g => g.NumSolves));
*/
internal class Program
{
    public static readonly char[] nonPeriod = { '?', '#' };
    public static Dictionary<string, long> Calculations = new Dictionary<string, long>();

    private static void Main(string[] args)
    {
        Console.WriteLine(File.ReadAllLines("input.txt").Select(CreateTuple).Sum(r => Calculate(r.springs, r.groups)));

        long Calculate(string springs, List<int> groups)
        {
            string key = $"{springs}|{string.Join('|', groups)}";
            if(Calculations.TryGetValue(key, out long value))
            {
                return value;
            }
            while (true)
            {
                if (!groups.Any() || groups[0] == 0)
                {
                    if (springs.IndexOf('#') != -1)
                    {
                        return 0;
                    }
                    else
                    {
                        return 1;
                    }
                }
                int nonPeriodIdx = springs.IndexOfAny(nonPeriod);
                springs = springs.Trim('.');
                if(springs.Length == 0)
                {
                    return 0; // getting here means we still have groups, but only periods left
                }
                if (springs.First() == '?')
                {
                    return Calculations[key] = Calculate(string.Concat(".", springs.AsSpan(1)), groups) + Calculate("#" + springs.Substring(1), groups);
                }
                // right now, cannot be questionmark, cannot be ., has to be #
                if (springs.Take(groups[0]).Any(c => c == '.'))
                {
                    return 0; // within groupsize we have a ., cannot fit, so wrong
                }
                if (springs.Skip(groups[0]).FirstOrDefault() == '#')
                {
                    return 0; // after groupsize, we cannot have a #, group too big, go away
                }
                // if we're getting here, than we have the potential of the right group here (either through questionmarks or an exact set of #). Skip the group and the next one
                if (groups[0] == springs.Length)
                {
                    springs = string.Empty;
                }
                else
                {
                    if (groups[0] > springs.Length)
                    {
                        return 0; // sneakysneaky, pretending to be a good group at the end... gotcha, you're not!
                    }
                    springs = springs[(groups[0] + 1)..];
                }
                groups = groups.Skip(1).ToList();
            }
        }

        (string springs, List<int> groups) CreateTuple(string str)
        {
            string[] parts = str.Split(' ');
            string springs = string.Join('?', Enumerable.Repeat(parts[0], 5));
            List<int> groups = Enumerable.Repeat(parts[1].Split(',').Select(int.Parse), 5).SelectMany(l => l).ToList();
            return new(springs, groups) { springs = springs, groups = groups };
        }
    }
}