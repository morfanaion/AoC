List<List<string>> networks = new List<List<string>>();
List<(string, string)> pairs = File.ReadAllLines("input.txt").Select(PairFromString).OrderBy(p => p.Item1).ThenBy(p => p.Item2).ToList();
List<string> computers = pairs.SelectMany(p => new string[] { p.Item1, p.Item2 }).Distinct().OrderBy(str => str).ToList();

foreach(string computer in computers)
{
	foreach(((string, string) pair1, (string, string) pair2) in GetAllCombinations(pairs.Where(pair => pair.Item1 == computer)))
	{
		if(pair1.Item2 == pair2.Item2)
		{
			continue;
		}
		if (pairs.Any(p => (p.Item1 == pair1.Item2 && p.Item2 == pair2.Item2) || (p.Item1 == pair2.Item2 && p.Item2 == pair1.Item2)))
		{
			networks.Add((new string[] { computer, pair1.Item2, pair2.Item2}).OrderBy(s => s).ToList());
		}
	}
	pairs = pairs.Where(pair => pair.Item1 != computer).ToList();
}

Console.WriteLine(networks.Count(n => n.Any(c => c.StartsWith("t"))));

(string, string) PairFromString(string str)
{
	string[] parts = str.Split('-');
	parts = parts.OrderBy(p => p).ToArray();
	return (parts[0], parts[1]);
}

IEnumerable<(T, T)> GetAllCombinations<T>(IEnumerable<T> enumerable)
{
	int i = 0;
	foreach (T t1 in enumerable)
	{
		i++;
		foreach (T t2 in enumerable.Skip(i))
		{
			yield return (t1, t2);
		}
	}
}