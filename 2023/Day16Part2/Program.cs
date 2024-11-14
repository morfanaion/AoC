using Day16Part2;

List<List<Tile>> tiles = new List<List<Tile>>();
List<Tile>? previousRow = null;
foreach (string line in File.ReadAllLines("input.txt"))
{
    List<Tile> spotRow = line.Select(Tile.CreateTile).ToList();
    if (previousRow == null)
    {
        foreach ((Tile curr, Tile previous) pair in spotRow.Skip(1).Zip<Tile, Tile, (Tile curr, Tile previous)>(spotRow, (curr, prev) => new(curr, prev)))
        {
            pair.curr.SetWest(pair.previous);
        }
    }
    else
    {
        foreach ((Tile south, Tile north) pair in spotRow.Zip<Tile, Tile, (Tile south, Tile north)>(previousRow, (south, north) => new(south, north)))
        {
            pair.south.SetNorth(pair.north);
        }
    }
    previousRow = spotRow;
    tiles.Add(spotRow);
}

for(int y = 0; y < tiles.Count; y++)
{
    for(int x = 0; x < tiles[y].Count; x++)
    {
        tiles[y][x].X = x;
        tiles[y][x].Y = y;
    }
}
int maximumCount = 0;

for(int i = 0; i < tiles.Count;i++)
{
    Shine(tiles[i][0], Direction.East);
    maximumCount = Math.Max(maximumCount, tiles.SelectMany(row => row).Count(tile => tile.IsEnergized));
    Reset();
    Shine(tiles[i].Last(), Direction.West);
    maximumCount = Math.Max(maximumCount, tiles.SelectMany(row => row).Count(tile => tile.IsEnergized));
    Reset();
}
for (int i = 0; i < tiles[0].Count; i++)
{
    Shine(tiles[0][i], Direction.South);
    maximumCount = Math.Max(maximumCount, tiles.SelectMany(row => row).Count(tile => tile.IsEnergized));
    Reset();
    Shine(tiles.Last()[i], Direction.North);
    maximumCount = Math.Max(maximumCount, tiles.SelectMany(row => row).Count(tile => tile.IsEnergized));
    Reset();
}
Console.WriteLine( maximumCount);

void Reset()
{
    foreach(Tile tile in tiles.SelectMany(row => row))
    {
        tile.IsEnergized = false;
        tile.AlreadyApproachedTo = Direction.None;
    }
}

void Shine(Tile startTile, Direction startDirection)
{
    Queue<(Tile tile, Direction direction)> queue = new Queue<(Tile tile, Direction direction)>();
    queue.Enqueue(new(startTile, startDirection));
    {
        while (queue.Any())
        {
            (Tile tile, Direction direction) currentEntry = queue.Dequeue();
            currentEntry.tile.Visit(currentEntry.direction, (tile, direction) =>
            {
                if (tile != null)
                {
                    queue.Enqueue(new(tile, direction));
                }
            });
        }
    }
}