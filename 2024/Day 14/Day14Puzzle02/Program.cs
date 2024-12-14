using Day14Puzzle02;

Robot[] robots = File.ReadAllLines("input.txt").Select(Robot.FromString).ToArray();

foreach (Robot robot in robots)
{
	robot.Progress(37786);
}
PrintState(robots);

void PrintState(Robot[] robots)
{
	char[][] bathroom = Enumerable.Range(0, Robot.BathroomHeight).Select(j => Enumerable.Range(0, Robot.BathroomWidth).Select(i => '.').ToArray()).ToArray();
	foreach(Robot robot in robots)
	{
		bathroom[robot.Y][robot.X] = '0';
	}
	Console.WriteLine(string.Join('\n', bathroom.Select(line => string.Join("", line))));
    Console.WriteLine();
}