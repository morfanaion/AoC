using Day14Puzzle01;

Robot[] robots = File.ReadAllLines("input.txt").Select(Robot.FromString).ToArray();

foreach (Robot robot in robots)
{
	robot.Progress(100);
}

int numRobotsInQuadrant1 = robots.Count(robot => robot.X >= 0 && robot.X < Robot.BathroomWidth / 2 && robot.Y >= 0 && robot.Y <= Robot.BathroomHeight / 2 - 1);
int numRobotsInQuadrant2 = robots.Count(robot => robot.X >= (Robot.BathroomWidth / 2) + 1 && robot.X <= Robot.BathroomWidth && robot.Y >= 0 && robot.Y <= Robot.BathroomHeight / 2 - 1);
int numRobotsInQuadrant3 = robots.Count(robot => robot.X >= 0 && robot.X < Robot.BathroomWidth / 2 && robot.Y >= (Robot.BathroomHeight / 2) + 1 && robot.Y < Robot.BathroomHeight);
int numRobotsInQuadrant4 = robots.Count(robot => robot.X >= (Robot.BathroomWidth / 2) + 1 && robot.X <= Robot.BathroomWidth && robot.Y >= (Robot.BathroomHeight / 2) + 1 && robot.Y < Robot.BathroomHeight);

Console.WriteLine(numRobotsInQuadrant1 * numRobotsInQuadrant2 * numRobotsInQuadrant3 * numRobotsInQuadrant4);