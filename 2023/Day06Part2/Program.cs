#define OLD
string[] fileLines = File.ReadAllLines("input.txt");
long time = long.Parse(string.Join("", fileLines[0].Skip(11).Where(c => c != ' ')).Trim());
long distance = long.Parse(string.Join("", fileLines[1].Skip(11).Where(c => c != ' ')).Trim());
long result = 1;
#if OLD
long firstWin = 0;
for (long j = 1; j < time; j++)
{
    long distanceTravelled = j * (time - j);
    if (distanceTravelled > distance)
    {
        firstWin = j;
        break;
    }
}
#else
long firstWin = (int)((time - Math.Sqrt(Math.Pow(time, 2) - (4 * distance))) / 2);
if (firstWin * (time - firstWin) < distance)
{
    firstWin++;
}
#endif

result = (time - (2 * firstWin) + 1);
Console.WriteLine(result);