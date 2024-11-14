char[][] grid = File.ReadAllLines("input.txt").Select(line => line.ToArray()).ToArray();

List<int> emptyRows = new List<int>();
for (int y = 0; y < grid.Length; y++)
{
    if (grid[y].All(c => c == '.'))
    {
        emptyRows.Add(y);
    }
}
List<int> emptyColumns  = new List<int>();
for(int x = 0; x < grid[0].Length; x++)
{
    if(grid.Select(row => row[x]).All(c => c == '.'))
    {
        emptyColumns.Add(x);
    }
}

List<(int x, int y)> galaxies = new List<(int x, int y)>();
for(int x = 0;x < grid[0].Length; x++)
{
    for(int y = 0;y < grid.Length; y++)
    {
        if (grid[y][x] == '#')
        {
            galaxies.Add(new (x + emptyColumns.Count(column => column < x), y + emptyRows.Count(row => row < y)));
        }
    }
}
List<int> distances = new List<int>();
for(int i = 0; i < galaxies.Count; i++)
{
    (int x, int y) currentGalaxy = galaxies[i];
    foreach(var otherGalaxy in galaxies.Skip(i + 1))
    {
        distances.Add(Math.Abs(currentGalaxy.x - otherGalaxy.x) + Math.Abs(currentGalaxy.y - otherGalaxy.y));
    }
}
Console.WriteLine(distances.Sum());