(string lowBound, string highBound)[] inputs = File.ReadAllText("input.txt").Split(',').Select(s => s.Split('-')).Select<string[], (string lowBound, string highBound)>(p => new(p[0], p[1])).ToArray();
List<ulong> invalidIds = new List<ulong>();
foreach ((string lowBound, string highBound) input in inputs)
{
    ulong lowBound = ulong.Parse(input.lowBound);
    ulong highBound = ulong.Parse(input.highBound);
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
            if (stringSize == input.lowBound.Length)
            {
                lowerBoundStartNum = ulong.Parse(input.lowBound.Substring(0, groupSize));
            }
            ulong higherBoundStartNum = (ulong)Math.Pow(10, groupSize) - 1;
            if (stringSize == input.highBound.Length)
            {
                higherBoundStartNum = ulong.Parse(input.highBound.Substring(0, groupSize));
            }
            invalidIds.AddRange(Process(lowerBoundStartNum, higherBoundStartNum, highBound, lowBound, stringSize / groupSize));
        }
    }
}
ulong result = 0;
foreach(ulong invalidId in invalidIds.Distinct())
{
    result += invalidId;
}
Console.WriteLine(result);

IEnumerable<ulong> Process(ulong startNumberLowBound, ulong startNumberHighBound, ulong highBound, ulong lowBound, int numGroups)
{
    for (ulong i = startNumberLowBound; i <= startNumberHighBound; i++)
    {
        ulong numberToCompare = ulong.Parse(string.Concat(Enumerable.Repeat(i.ToString(), numGroups)));
        if (numberToCompare >= lowBound && numberToCompare <= highBound)
        {
            yield return numberToCompare;
        }
    }
}