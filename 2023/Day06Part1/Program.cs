//#define OLD
string[] fileLines = File.ReadAllLines("input.txt");
long[] times = string.Join("", fileLines[0].Skip(11)).Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(s => long.Parse(s)) .ToArray();
long[] distances = string.Join("", fileLines[1].Skip(11)).Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(s => long.Parse(s)).ToArray();
long result = 1;
for(int i = 0; i < times.Length; i++)
{
#if OLD
    long firstWin = 0;
    for(long j = 1; j < times[i]; j++)
    {
        long distanceTravelled = j * (times[i] - j);
        if(distanceTravelled > distances[i])
        {
            firstWin = j;
            break;
        }
    }
#else
    long firstWin = (int)((times[i] - Math.Sqrt(Math.Pow(times[i], 2) - (4 * distances[i]))) / 2);
    if(firstWin * (times[i] - firstWin) < distances[i])
    {
        firstWin++;
    }
#endif
    result *= (times[i] - (2 * firstWin) + 1);
}
Console.WriteLine(result);