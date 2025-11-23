using Day09Puzzle01;
using System.Text.RegularExpressions;
Dictionary<string, City> cities = new Dictionary<string, City>();
Regex regex = RegexHelper.DistancesRegex();
foreach (string line in File.ReadAllLines("input.txt"))
{
    Match match = regex.Match(line);
    if(!match.Success)
    {
        Console.WriteLine("PANIC!!!");
        return;
    }
    string city1Name = match.Groups["city1"].Value;
    string city2Name = match.Groups["city2"].Value;
    int distance = int.Parse(match.Groups["distance"].Value);
    if(!cities.TryGetValue(city1Name, out City? city1))
    {
        city1 = new City() { Name = city1Name };
        cities.Add(city1Name, city1);
    }
    if (!cities.TryGetValue(city2Name, out City? city2))
    {
        city2 = new City() {Name = city2Name};
        cities.Add(city2Name, city2);
    }
    city1.Distances[city2] = distance;
    city2.Distances[city1] = distance;
}

IEnumerable<IEnumerable<City>> permutations = cities.Values.Permute().Where(permutation => permutation.First().Name[0] < permutation.Last().Name[0]);
int currentMinDistance = int.MaxValue;
foreach(IEnumerable<City> permutation in permutations)
{
    int currentDistance = 0;
    foreach((City destination, City origin) in permutation.Skip(1).Zip(permutation))
    {
        currentDistance += origin.Distances[destination];
        if(currentDistance > currentMinDistance)
        {
            break;
        }
    }
    if(currentDistance < currentMinDistance)
    {
        currentMinDistance = currentDistance;
    }
}
Console.WriteLine(currentMinDistance);