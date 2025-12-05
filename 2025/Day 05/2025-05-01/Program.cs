List<ulong> ids = new List<ulong>();
List<(ulong low, ulong high)> freshRanges = new List<(ulong low, ulong high)>();

Action<string> processLine = line =>
{
    if (string.IsNullOrWhiteSpace(line))
    {
        processLine = line =>
        {
            ids.Add(ulong.Parse(line));
        };
        return;
    }
    string[] parts = line.Split('-');
    ulong low = ulong.Parse(parts[0]);
    ulong high = ulong.Parse(parts[1]);
    freshRanges.Add((low, high));
};

foreach(string line in File.ReadAllLines("input.txt"))
{
    processLine(line);
}

Console.WriteLine(ids.Count(IsFresh));

bool IsFresh(ulong id) => freshRanges.Any(range => id >= range.low && id <= range.high);