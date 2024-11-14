const long expansionFactor = 1000000;

char[][] grid = File.ReadAllLines("input.txt").Select(line => line.ToArray()).ToArray();

List<long> emptyRows = new List<long>();
for (long y = 0; y < grid.Length; y++)
{
    if (grid[y].All(c => c == '.'))
    {
        emptyRows.Add(y);
    }
}
List<long> emptyColumns  = new List<long>();
for(long x = 0; x < grid[0].Length; x++)
{
    if(grid.Select(row => row[x]).All(c => c == '.'))
    {
        emptyColumns.Add(x);
    }
}

List<(long x, long y)> galaxies = new List<(long x, long y)>();
for(long x = 0;x < grid[0].Length; x++)
{
    for(long y = 0;y < grid.Length; y++)
    {
        if (grid[y][x] == '#')
        {
            galaxies.Add(new(x + emptyColumns.Where(column => column < x).Select(column => expansionFactor - 1).Sum(), y + emptyRows.Where(row => row < y).Select(row => expansionFactor - 1).Sum()));
        }
    }
}
List<long> distances = new List<long>();
for(int i = 0; i < galaxies.Count; i++)
{
    (long x, long y) currentGalaxy = galaxies[i];
    foreach(var otherGalaxy in galaxies.Skip(i + 1))
    {
        distances.Add(Math.Abs(currentGalaxy.x - otherGalaxy.x) + Math.Abs(currentGalaxy.y - otherGalaxy.y));
    }
}
Console.WriteLine(distances.Sum());