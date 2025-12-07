string[] input = File.ReadAllLines("input.txt");
Dictionary<int, long> beamIds = new Dictionary<int, long>
{
    { input[0].IndexOf('S'), 1 }
};
List<List<int>> splitterIds = new List<List<int>>();
foreach (string line in input.Skip(0))
{
    if (!line.Any(c => c == '^'))
    {
        continue;
    }
    List<int> myIds = new List<int>();
    string processingLine = line;
    int startingIdx = 0;
    while (processingLine.Any(c => c == '^'))
    {
        int newIdx = processingLine.IndexOf('^');
        myIds.Add(newIdx + startingIdx);
        startingIdx += newIdx + 1;
        processingLine = processingLine.Substring(newIdx + 1);
    }
    splitterIds.Add(myIds);
}
int numSplits = 0;
foreach (List<int> splitterIdsForLevel in splitterIds)
{
    Dictionary<int, long> newBeamIds = new Dictionary<int, long>();
    foreach (KeyValuePair<int, long> beamId in beamIds)
    {
        if (splitterIdsForLevel.Any(i => i == beamId.Key))
        {
            if(newBeamIds.ContainsKey(beamId.Key - 1))
            {
                newBeamIds[beamId.Key - 1] += beamId.Value;
            }
            else
            {
                newBeamIds[beamId.Key - 1] = beamId.Value;
            }
            if (newBeamIds.ContainsKey(beamId.Key + 1))
            {
                newBeamIds[beamId.Key + 1] += beamId.Value;
            }
            else
            {
                newBeamIds[beamId.Key + 1] = beamId.Value;
            }
        }
        else
        {
            if(newBeamIds.ContainsKey(beamId.Key))
            {
                newBeamIds[beamId.Key] += beamId.Value;
            }
            else
            {
                newBeamIds[beamId.Key] = beamId.Value;
            }
        }
    }
    beamIds = newBeamIds;
}
long total = 0;
foreach(long count in beamIds.Values)
{
    total += count;
}
Console.WriteLine(total);