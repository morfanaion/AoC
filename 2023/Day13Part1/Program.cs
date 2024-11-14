// See https://aka.ms/new-console-template for more information
using Day13Part1;

List<string> pattern = new List<string>();
List<Pattern> patterns = new List<Pattern>();
foreach(string line in File.ReadAllLines("input.txt"))
{
    if(string.IsNullOrEmpty(line))
    {
        patterns.Add(new Pattern(pattern));
        pattern.Clear();
    }
    else
    {
        pattern.Add(line);
    }
}
if(pattern.Any())
{
    patterns.Add(new Pattern(pattern));
}

List<int> Values = patterns.Select(p => p.GetReflectionValue()).ToList();
Console.WriteLine(Values.Sum());