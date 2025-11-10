using Day15Puzzle02;

Regex regex = new Regex(@"Sensor at x=(?<SX>-?\d*), y=(?<SY>-?\d*): closest beacon is at x=(?<BX>-?\d*), y=(?<BY>-?\d*)");
List<SensorReport> reports = new List<SensorReport>();
foreach (string line in File.ReadAllLines("Input.txt"))
{
    Match match = regex.Match(line);
    reports.Add(new SensorReport() { SensorX = int.Parse(match.Groups["SX"].Value), SensorY = int.Parse(match.Groups["SY"].Value), BeaconX = int.Parse(match.Groups["BX"].Value), BeaconY = int.Parse(match.Groups["BY"].Value) });
}

int maxManhattanDistance = reports.Max(report => report.ManhattanDistance);
int minX = reports.Min(report => report.SensorX) - maxManhattanDistance;
int maxX = reports.Max(report => report.SensorX) + maxManhattanDistance;
int minY = reports.Min(report => report.SensorY) - maxManhattanDistance;
int maxY = reports.Max(report => report.SensorY) + maxManhattanDistance;

long x = 0;
long y = 0;
bool found = false;
while(!found)
{
    SensorReport? report = reports.FirstOrDefault(report => WithinDistance(x, y, report));
    if (report != null)
    {
        x = report.SensorX + (report.ManhattanDistance - Math.Abs(report.SensorY - y)) + 1;
        if(x > 4000000)
        {
            x = 0;
            y++;
        }
    }
    else
    {
        found = true;
    }
    
}
Console.WriteLine((x * 4000000) + y);

bool WithinDistance(long x, long y, SensorReport report)
{
    return Math.Abs(x - report.SensorX) + Math.Abs(y - report.SensorY) <= report.ManhattanDistance;
}