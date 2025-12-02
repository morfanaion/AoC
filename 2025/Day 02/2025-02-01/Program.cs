(string lowBound, string highBound)[] inputs = File.ReadAllText("input.txt").Split(',').Select(s => s.Split('-')).Select<string[], (string lowBound, string highBound)>(p => new (p[0], p[1])).ToArray();
ulong result = 0;
foreach((string lowBound, string highBound) input in inputs)
{
    ulong lowBound = ulong.Parse(input.lowBound);
    ulong highBound = ulong.Parse(input.highBound);
    ulong startNumberLowBound = 0;
    ulong valueForStartLowBound = 0;
    if (input.lowBound.Length % 2 == 0)
    {
        ulong diviser = (ulong)Math.Pow(10, input.lowBound.Length / 2);
        startNumberLowBound = lowBound / diviser;
        valueForStartLowBound = lowBound % diviser;
    }
    else
    {
        startNumberLowBound = (ulong)Math.Pow(10, input.lowBound.Length / 2);
        valueForStartLowBound = 0;
    }
    ulong startNumberHighBound = 0;
    ulong valueForEndHighBound = 0;
    if (input.highBound.Length % 2 == 0)
    {
        ulong diviser = (ulong)Math.Pow(10, input.highBound.Length / 2);
        startNumberHighBound = highBound / diviser;
        valueForEndHighBound = highBound % diviser;
    }
    else
    {
        startNumberHighBound = (ulong)Math.Pow(10, input.highBound.Length / 2) - 1;
        valueForEndHighBound = startNumberHighBound;
    }
    if(startNumberLowBound > startNumberHighBound)
    {
        continue;
    }
    if(startNumberLowBound == startNumberHighBound)
    {
        if(valueForStartLowBound <= startNumberHighBound && valueForEndHighBound >= startNumberHighBound)
        {
            result += ulong.Parse($"{startNumberHighBound}{startNumberHighBound}");
        }
        continue;
    }
    if(valueForStartLowBound <= startNumberLowBound)
    {
        result += ulong.Parse($"{startNumberLowBound}{startNumberLowBound}");
    }
    if(valueForEndHighBound >= startNumberHighBound)
    {
        result += ulong.Parse($"{startNumberHighBound}{startNumberHighBound}");
    }
    for(ulong i = startNumberLowBound + 1; i < startNumberHighBound; i++)
    {
        result += ulong.Parse($"{i}{i}");
    }
}
Console.WriteLine(result);