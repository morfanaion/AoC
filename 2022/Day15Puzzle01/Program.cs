// See https://aka.ms/new-console-template for more information
using Day15Puzzle01;

Regex regex = new Regex(@"Sensor at x=(?<SX>-?\d*), y=(?<SY>-?\d*): closest beacon is at x=(?<BX>-?\d*), y=(?<BY>-?\d*)");
List<SensorReport> reports = new List<SensorReport>();
foreach(string line in File.ReadAllLines("Input.txt"))
{
    Match match = regex.Match(line);
    reports.Add(new SensorReport() { SensorX = int.Parse(match.Groups["SX"].Value), SensorY = int.Parse(match.Groups["SY"].Value), BeaconX = int.Parse(match.Groups["BX"].Value), BeaconY = int.Parse(match.Groups["BY"].Value) });
}

int maxManhattanDistance = reports.Max(report => report.ManhattanDistance);
int minX = reports.Min(report => report.SensorX) - maxManhattanDistance;
int maxX = reports.Max(report => report.SensorX) + maxManhattanDistance;
int minY = reports.Min(report => report.SensorY) - maxManhattanDistance;
int maxY = reports.Max(report => report.SensorY) + maxManhattanDistance;
int testy = 2000000;
Console.WriteLine(Enumerable.Range(minX, maxX - minX).Count(x => reports.Any(report => !(report.BeaconX == x && report.BeaconY == testy) && WithinDistance(x, testy, report))));

bool WithinDistance(int x, int y, SensorReport report)
{
    return Math.Abs(x - report.SensorX) + Math.Abs(y - report.SensorY) <= report.ManhattanDistance;
}

/*
char[][] grid = new char[(maxY - minY) + 1][];
for (int y = 0; y <= (maxY - minY); y++)
{
    grid[y] = new char[maxX - minX + 1];
    for (int x = 0; x <= maxX - minX; x++)
    {
        grid[y][x] = '.';
    }
}
foreach (SensorReport report in reports)
{
    grid[report.SensorY - minY][report.SensorX - minX] = 'S';
    grid[report.BeaconY - minY][report.BeaconX - minX] = 'B';
    int myManhattanDistance = report.ManhattanDistance;
    for (int x = report.SensorX - myManhattanDistance; x <= report.SensorX + myManhattanDistance; x++)
    {
        for (int y = report.SensorY - myManhattanDistance; y <= report.SensorY + myManhattanDistance; y++)
        {
            char currentGridVal = grid[y - minY][x - minX];
            if (currentGridVal != '.')
            {
                continue;
            }
            if (Math.Abs(x - report.SensorX) + Math.Abs(y - report.SensorY) <= myManhattanDistance)
            {
                grid[y - minY][x - minX] = '#';
            }
        }
    }
}

int numNotPossible = 0;
for(int x = 0; x <= maxX - minX; x++)
{
    char currentChar = grid[2000000 - minY][x];
    if (currentChar != '.' && currentChar != 'B')
    {
        numNotPossible++;
    }
}
Console.WriteLine(string.Join('\n', grid.Select(y => string.Concat(y))));
Console.WriteLine(numNotPossible);*/