using Day16Part1;

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

Queue<(Tile tile, Direction direction)> queue = new Queue<(Tile tile, Direction direction)>();
queue.Enqueue(new(tiles[0][0], Direction.East));
{
    while(queue.Any())
    {
        (Tile tile, Direction direction) currentEntry = queue.Dequeue();
        currentEntry.tile.Visit(currentEntry.direction, (tile, direction) =>
        {
            if (tile != null)
            {
                queue.Enqueue(new (tile, direction));
            }
        });
    }
}

Console.WriteLine(tiles.SelectMany(row => row).Count(tile => tile.IsEnergized));