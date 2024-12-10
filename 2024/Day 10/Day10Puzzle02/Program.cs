int [][] theGrid = File.ReadAllLines("input.txt").Select(line => line.Select(c => c - '0').ToArray()).ToArray();
int result = 0;

for(int x = 0; x < theGrid[0].Length; x++)
{
	for(int y = 0; y < theGrid.Length; y++)
	{
		if (theGrid[y][x] == 0)
		{
			result += GetScore(x, y);
		}
	}
}

Console.WriteLine(result);

int GetScore(int x, int y)
{
	if (theGrid[y][x] == 9)
	{
		return 1;
	}
	IEnumerable<(int x, int y)> options = GetOptions(x, y);
	return options.Sum(option => GetScore(option.x, option.y));
}

IEnumerable<(int x, int y)> GetOptions(int x, int y)
{
	if(x > 0 && theGrid[y][x - 1] - theGrid[y][x] == 1)
	{
		yield return (x - 1, y);
	}
	if(y > 0 && theGrid[y - 1][x] - theGrid[y][x] == 1)
	{
		yield return (x, y - 1);
	}
	if(x < theGrid[0].Length - 1 && theGrid[y][x + 1] - theGrid[y][x] == 1)
	{
		yield return (x + 1, y);
	}
	if (y < theGrid.Length - 1 && theGrid[y + 1][x] - theGrid[y][x] == 1)
	{
		yield return (x, y + 1);
	}
}
