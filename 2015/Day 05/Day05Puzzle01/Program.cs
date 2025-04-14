string[] disallowedCombinations = ["ab", "cd", "pq", "xy"];

Console.WriteLine(File.ReadAllLines("Input.txt").Count(IsNiceString));

bool IsNiceString(string arg)
{
	if (!arg.Where("aeoiu".Contains).Skip(2).Any())
	{
		return false;
	}
	if(!arg.Skip(1).Zip(arg).Any(pair => pair.First == pair.Second))
	{
		return false;
	}
	return !disallowedCombinations.Any(arg.Contains);
}