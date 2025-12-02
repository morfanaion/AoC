(string lowBound, string highBound)[] inputs = File.ReadAllText("input.txt").Split(',').Select(s => s.Split('-')).Select<string[], (string lowBound, string highBound)>(p => new(p[0], p[1])).ToArray();
foreach((string lowBound, string highBound) input in inputs)
{
    if(input.lowBound.Length == input.highBound.Length)
    {
        // same length
        for (int groupSize = 1; groupSize < input.lowBound.Length - 1; groupSize++)
        {
            if (input.lowBound.Length % groupSize != 0)
            {
                continue;
            }
            ulong lowerBoundStartNum = ulong.Parse(input.lowBound.Substring(0, groupSize));
            ulong higherBoundStartNum = ulong.Parse(input.highBound.Substring(0, groupSize));
            ulong lowerBoundRemainder = ulong.Parse(input.lowBound.Substring(groupSize));
            ulong higherBoundRemainder = ulong.Parse(input.highBound.Substring(groupSize));
            if (lowerBoundStartNum == higherBoundStartNum)
            {
                // check for numbers within range
                continue;
            }
            // check for numbers from 
        }
    }
}

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

