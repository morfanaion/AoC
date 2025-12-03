(string lowBound, string highBound)[] inputs = File.ReadAllText("input.txt").Split(',').Select(s => s.Split('-')).Select<string[], (string lowBound, string highBound)>(p => new(p[0], p[1])).ToArray();
List<ulong> invalidIds = new List<ulong>();
foreach ((string lowBound, string highBound) input in inputs)
{
    List<ulong> InvalidIdsForRange = new List<ulong>();
    for (int stringSize = input.lowBound.Length; stringSize <= input.highBound.Length; stringSize++)
    {
        for (int groupSize = 1; groupSize < stringSize; groupSize++)
        {
            if (stringSize % groupSize != 0)
            {
                continue;
            }
            ulong lowerBoundStartNum = (ulong)Math.Pow(10, groupSize - 1);
            ulong lowerBoundRemainder = 0;
            if (stringSize == input.lowBound.Length)
            {
                string[] lowBoundGroups = SplitStringBySize(input.lowBound, groupSize).ToArray();
                lowerBoundStartNum = ulong.Parse(lowBoundGroups[0]);
                lowerBoundRemainder = lowBoundGroups.Skip(1).Select(ulong.Parse).Max();
            }
            ulong higherBoundStartNum = (ulong)Math.Pow(10, groupSize) - 1;
            ulong higherBoundRemainder = higherBoundStartNum;
            if (stringSize == input.highBound.Length)
            {
                string[] highBoundGroups = SplitStringBySize(input.highBound, groupSize).ToArray();
                higherBoundStartNum = ulong.Parse(highBoundGroups[0]);
                higherBoundRemainder = highBoundGroups.Skip(1).Select(ulong.Parse).Min();
            }
            InvalidIdsForRange.AddRange(Process(lowerBoundStartNum, lowerBoundRemainder, higherBoundStartNum, higherBoundRemainder, stringSize / groupSize));
        }
    }
    invalidIds.AddRange(InvalidIdsForRange.Distinct());
}
ulong result = 0;
foreach(ulong invalidId in invalidIds)
{
       result += invalidId;
}
Console.WriteLine(result);

IEnumerable<string> SplitStringBySize(string input, int size)
{
    if (input.Length % size != 0)
    {
        yield break;
    }
    for (int i = 0; i < input.Length; i += size)
    {
        yield return input.Substring(i, size);
    }
}

IEnumerable<ulong> Process(ulong startNumberLowBound, ulong valueForStartLowBound, ulong startNumberHighBound, ulong valueForEndHighBound, int numGroups)
{
    if (startNumberLowBound > startNumberHighBound)
    {
        yield break;
    }
    if (startNumberLowBound == startNumberHighBound)
    {
        if (valueForStartLowBound <= startNumberHighBound && valueForEndHighBound >= startNumberHighBound)
        {
            yield return ulong.Parse(string.Concat(Enumerable.Repeat(startNumberHighBound.ToString(), numGroups)));
        }
        yield break;
    }
    if (valueForStartLowBound <= startNumberLowBound)
    {
        yield return ulong.Parse(string.Concat(Enumerable.Repeat(startNumberLowBound.ToString(), numGroups)));
    }
    if (valueForEndHighBound >= startNumberHighBound)
    {
        yield return ulong.Parse(string.Concat(Enumerable.Repeat(startNumberHighBound.ToString(), numGroups)));
    }
    for (ulong i = startNumberLowBound + 1; i < startNumberHighBound; i++)
    {
        yield return ulong.Parse(string.Concat(Enumerable.Repeat(i.ToString(), numGroups)));
    }
}