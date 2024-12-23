
List<List<string>> networks = new List<List<string>>();
List<(string, string)> pairs = File.ReadAllLines("input.txt").Select(PairFromString).OrderBy(p => p.Item1).ThenBy(p => p.Item2).ToList();
List<(string, string)> crossOfPairList = pairs.ToList();
List<string> computers = pairs.SelectMany(p => new string[] { p.Item1, p.Item2 }).Distinct().OrderBy(str => str).ToList();

foreach (string computer in computers)
{
	foreach ((string, string) pair in pairs.Where(p => p.Item1 == computer))
	{
		bool addedToNetwork = false;
		foreach (var network in networks.Where(n => n.Any(c => c == computer)))
		{
			if (network.All(n => pairs.Any(p => p.Item1 == n && p.Item2 == pair.Item2 || p.Item1 == pair.Item2 && p.Item2 == n)))
			{
				addedToNetwork = true;
				network.Add(pair.Item2);
			}
		}
		if (!addedToNetwork)
		{
			networks.Add(new List<string> { pair.Item1, pair.Item2 });
		}
	}
}


Console.WriteLine(string.Join(',', networks.OrderByDescending(n => n.Count).First()));

(string, string) PairFromString(string str)
{
	string[] parts = str.Split('-');
	parts = parts.OrderBy(p => p).ToArray();
	return (parts[0], parts[1]);
}
