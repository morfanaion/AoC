
using Day10Puzzle01;

Location[][] theGrid = File.ReadAllLines("input.txt").Select(line => line.Select(c => new Location(c - '0')).ToArray()).ToArray();
int result = 0;
List<Location> trailEnds = theGrid.SelectMany(column => column).Where(l => l.Height == 9).ToList();

for(int x = 0; x < theGrid[0].Length; x++)
{
	for(int y = 0; y < theGrid.Length; y++)
	{
		if (theGrid[y][x].Height == 0)
		{
			result += GetScore(x, y);
			foreach (Location l in trailEnds)
			{
				l.Visited = false;
			}
		}
	}
}

Console.WriteLine(result);

int GetScore(int x, int y)
{
	if (theGrid[y][x].Height == 9 && !theGrid[y][x].Visited)
	{
		theGrid[y][x].Visited = true;
		return 1;
	}
	IEnumerable<(int x, int y)> options = GetOptions(x, y);
	return options.Sum(option => GetScore(option.x, option.y));
}

IEnumerable<(int x, int y)> GetOptions(int x, int y)
{
	if(x > 0 && theGrid[y][x - 1].Height - theGrid[y][x].Height == 1)
	{
		yield return (x - 1, y);
	}
	if(y > 0 && theGrid[y - 1][x].Height - theGrid[y][x].Height == 1)
	{
		yield return (x, y - 1);
	}
	if(x < theGrid[0].Length - 1 && theGrid[y][x + 1].Height - theGrid[y][x].Height == 1)
	{
		yield return (x + 1, y);
	}
	if (y < theGrid.Length - 1 && theGrid[y + 1][x].Height - theGrid[y][x].Height == 1)
	{
		yield return (x, y + 1);
	}
}
