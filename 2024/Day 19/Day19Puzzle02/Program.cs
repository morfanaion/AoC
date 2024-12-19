string[] input = File.ReadAllLines("input.txt");
string[] towels = input[0].Split(',').Select(t => t.Trim()).OrderByDescending(t => t.Length).ToArray();

Dictionary<string, long> possibleDictionary = new Dictionary<string, long>
{
    { string.Empty, 1 }
};
Console.WriteLine(input.Skip(2).Sum(DesignPossible));


long DesignPossible(string design)
{
    if (possibleDictionary.TryGetValue(design, out long result))
    {
        return result;
    }
    return possibleDictionary[design] = towels.Sum(towel => design.StartsWith(towel) ? DesignPossible(design.Substring(towel.Length)) : 0);
}