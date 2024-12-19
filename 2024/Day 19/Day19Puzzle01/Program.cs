string[] input = File.ReadAllLines("input.txt");
string[] towels = input[0].Split(',').Select(t => t.Trim()).OrderByDescending(t => t.Length).ToArray();

Dictionary<string, bool> possibleDictionary = new Dictionary<string, bool>
{
    { string.Empty, true }
};

Console.WriteLine(input.Skip(2).Count(DesignPossible));

bool DesignPossible(string design)
{
    if (possibleDictionary.TryGetValue(design, out bool result))
    {
        return result;
    }
    return possibleDictionary[design] = towels.Any(towel => design.StartsWith(towel) && DesignPossible(design.Substring(towel.Length)));
}