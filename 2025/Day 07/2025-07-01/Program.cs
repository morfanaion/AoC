string[] input = File.ReadAllLines("input.txt");
List<int> beamIds = new List<int>() { input[0].IndexOf('S') };
List<List<int>> splitterIds = new List<List<int>>();
foreach(string line in input.Skip(0))
{
    if(!line.Any(c => c == '^'))
    {
        continue;
    }
    List<int> myIds = new List<int>();
    string processingLine = line;
    int startingIdx = 0;
    while(processingLine.Any(c => c == '^'))
    {
        int newIdx = processingLine.IndexOf('^');
        myIds.Add(newIdx +  startingIdx);
        startingIdx+= newIdx + 1;
        processingLine = processingLine.Substring(newIdx +1);
    }
    splitterIds.Add(myIds);
}
int numSplits = 0;
foreach(List<int> splitterIdsForLevel in splitterIds)
{
    List<int> newBeamIds = new List<int>();
    foreach(int beamId in beamIds)
    {
        if (splitterIdsForLevel.Any(i => i == beamId))
        {
            newBeamIds.Add(beamId - 1);
            newBeamIds.Add(beamId + 1);
            numSplits++;
        }
        else
        {
            newBeamIds.Add(beamId);
        }
    }
    beamIds = newBeamIds.Distinct().ToList();
}
Console.WriteLine(numSplits);