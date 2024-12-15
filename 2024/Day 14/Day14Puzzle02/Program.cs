using Day14Puzzle02;

Robot[] robots = File.ReadAllLines("input.txt").Select(Robot.FromString).ToArray();

int iteration = 0;
while (!TreeFound())
{
    iteration++;
    foreach (Robot robot1 in robots)
    {
        robot1.Progress(1);
    }
}

// 6577
PrintState(iteration, robots);

bool TreeFound()
{
    IEnumerable<IGrouping<long, Robot>> robotsByLine = robots.GroupBy(r => r.Y);
    return robotsByLine.Any(g => IsPartOfTreeLogo(g));
}

bool IsPartOfTreeLogo(IGrouping<long, Robot> g)
{
    char[] line = Enumerable.Range(0, Robot.BathroomWidth).Select(i => '.').ToArray();
    foreach(long x in g.Select(r => r.X).Distinct())
    {
        line[x] = '0';
    }
    return string.Concat(line).Contains("000000000000000000");
}

void PrintState(int iteration, Robot[] robots)
{
    char[][] bathroom = Enumerable.Range(0, Robot.BathroomHeight).Select(j => Enumerable.Range(0, Robot.BathroomWidth).Select(i => '.').ToArray()).ToArray();
    foreach (Robot robot in robots)
    {
        bathroom[robot.Y][robot.X] = '0';
    }
    Console.WriteLine(string.Join('\n', bathroom.Select(line => string.Join("", line))));
    Console.WriteLine();
    Console.WriteLine(iteration);
}
