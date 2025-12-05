List<(ulong low, ulong high)> freshRanges = new List<(ulong low, ulong high)>();

foreach(string line in File.ReadAllLines("input.txt"))
{
    if (string.IsNullOrWhiteSpace(line))
    {
        break;
    }
    string[] parts = line.Split('-');
    ulong low = ulong.Parse(parts[0]);
    ulong high = ulong.Parse(parts[1]);
    freshRanges.Add((low, high));
}

List<(ulong low, ulong high)> mergedRanges = new List<(ulong low, ulong high)>();

foreach (var range in freshRanges.OrderBy(r => r.low))
{
    if (mergedRanges.Count == 0)
    {
        mergedRanges.Add(range);
    }
    else
    {
        var last = mergedRanges.Last();
        if (range.low <= last.high + 1)
        {
            mergedRanges[mergedRanges.Count - 1] = (last.low, Math.Max(last.high, range.high));
        }
        else
        {
            mergedRanges.Add(range);
        }
    }
}

Console.WriteLine(mergedRanges.Sum(RangeSize));

long RangeSize((ulong low, ulong high) input) => (long)(input.high - input.low + 1);

