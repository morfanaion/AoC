using Day05Part2;

string[] lines = File.ReadAllLines("input.txt");

string[] seedNumbers = string.Join("", lines[0].Skip(7)).Split(" ", StringSplitOptions.RemoveEmptyEntries).ToArray();

List<string> mapLines = new List<string>();
Action<IEnumerable<string>> registerMap = list => { };

foreach (string line in lines.Skip(2))
{
    if (!char.IsDigit(line.FirstOrDefault()))
    {
        if (!string.IsNullOrEmpty(line?.Trim()))
        {
            switch (line.Trim())
            {
                case "seed-to-soil map:":
                    registerMap = list => MapHelper.RegisterMapHelper(new MapHelper<Soil, Seed>((id) => new Soil() { Id = id }, list));
                    break;
                case "soil-to-fertilizer map:":
                    registerMap = list => MapHelper.RegisterMapHelper(new MapHelper<Fertilizer, Soil>((id) => new Fertilizer() { Id = id }, list));
                    break;
                case "fertilizer-to-water map:":
                    registerMap = list => MapHelper.RegisterMapHelper(new MapHelper<Water, Fertilizer>((id) => new Water() { Id = id }, list));
                    break;
                case "water-to-light map:":
                    registerMap = list => MapHelper.RegisterMapHelper(new MapHelper<Light, Water>((id) => new Light() { Id = id }, list));
                    break;
                case "light-to-temperature map:":
                    registerMap = list => MapHelper.RegisterMapHelper(new MapHelper<Temperature, Light>((id) => new Temperature() { Id = id }, list));
                    break;
                case "temperature-to-humidity map:":
                    registerMap = list => MapHelper.RegisterMapHelper(new MapHelper<Humidity, Temperature>((id) => new Humidity() { Id = id }, list));
                    break;
                case "humidity-to-location map:":
                    registerMap = list => MapHelper.RegisterMapHelper(new MapHelper<Location, Humidity>((id) => new Location() { Id = id }, list));
                    break;
            }
        }
        else
        {
            registerMap(mapLines);
            mapLines.Clear();
        }
    }
    else
    {
        mapLines.Add(line);
    }
}
if (mapLines.Any())
{
    registerMap(mapLines);
    mapLines.Clear();
}

long minLocationId = long.MaxValue;
for (int i = 0; i < seedNumbers.Length; i += 2)
{
    long seedRangeStart = long.Parse(seedNumbers[i]);
    long seedRangeCount = long.Parse(seedNumbers[i + 1]);
    for (long j = seedRangeStart; j < seedRangeStart + seedRangeCount; j++)
    {
        minLocationId = Math.Min(minLocationId, MapHelper.GetLocationForSeedId(j));
    }
}


Console.WriteLine(minLocationId);