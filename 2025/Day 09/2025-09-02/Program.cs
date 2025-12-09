using _2025_09_02;

List<RedTile> redTiles = File.ReadAllLines("input.txt")
    .Select(line => line.Split(','))
    .Select(parts => new RedTile() { ActualX = long.Parse(parts[0]), ActualY = long.Parse(parts[1]) })
    .ToList();

int startX = 1;
long prevX = -1;
long[] allXs = redTiles.Select(redTile => redTile.ActualX).Distinct().OrderBy(x => x).ToArray();
foreach (long realX in allXs)
{
    if(realX - prevX == 1)
    {
        startX--;
    }
    RedTile[] tilesForX = redTiles.Where(redTile => redTile.ActualX == realX).ToArray();
    foreach (RedTile redTile in tilesForX)
    {
        redTile.VirtualX = startX;
    }
    startX += 2;
    prevX = realX;
}
int startY = 1;
long prevY = -1;
long[] allYs = redTiles.Select(redTile => redTile.ActualY).Distinct().OrderBy(y => y).ToArray();
foreach (long realY in allYs)
{
    if(realY - prevY == 1)
    {
        startY--;
    }
    RedTile[] tilesForY = redTiles.Where(redTile => redTile.ActualY == realY).ToArray();
    foreach (RedTile redTile in tilesForY)
    {
        redTile.VirtualY = startY;
    }
    startY += 2;
    prevY = realY;
}

OtherTile otherTile = new OtherTile();
GreenTile greenTile = new GreenTile();
UnknownTile unknownTile = new UnknownTile();
Tile[][] shrunkenFloorPlan = new Tile[startY * 2][];
for(int i = 0; i < startY * 2; i++)
{
    shrunkenFloorPlan[i] = Enumerable.Repeat((Tile)unknownTile, startX * 2).ToArray();
}
RedTile lastTile = redTiles.Last();
foreach (RedTile redTile in redTiles)
{
    shrunkenFloorPlan[redTile.VirtualY * 2][redTile.VirtualX * 2] = redTile;
    if (redTile.VirtualX == lastTile.VirtualX)
    {
        if (redTile.VirtualY > lastTile.VirtualY)
        {
            for (int y = (redTile.VirtualY * 2) - 1; y > lastTile.VirtualY * 2; y--)
            {
                shrunkenFloorPlan[y][redTile.VirtualX * 2] = greenTile;
            }
        }
        else if (redTile.VirtualY < lastTile.VirtualY)
        {
            for (int y = (redTile.VirtualY * 2) + 1; y < lastTile.VirtualY * 2; y++)
            {
                shrunkenFloorPlan[y][redTile.VirtualX * 2] = greenTile;
            }
        }
    }
    else if (redTile.VirtualY == lastTile.VirtualY)
    {
        if (redTile.VirtualX > lastTile.VirtualX)
        {
            for (int x = (redTile.VirtualX * 2) - 1; x > lastTile.VirtualX * 2; x--)
            {
                shrunkenFloorPlan[redTile.VirtualY * 2][x] = greenTile;
            }
        }
        else if (redTile.VirtualX < lastTile.VirtualX)
        {
            for (int x = (redTile.VirtualX * 2) + 1; x < lastTile.VirtualX * 2; x++)
            {
                shrunkenFloorPlan[redTile.VirtualY * 2][x] = greenTile;
            }
        }
    }
    lastTile = redTile;
}

PriorityQueue<(int x, int y), int> squaresToExplore = new();
squaresToExplore.Enqueue((0, 0), 0);
while (squaresToExplore.TryDequeue(out (int x, int y) currentSquare, out int currentPriority))
{
    if (shrunkenFloorPlan[currentSquare.y][currentSquare.x].Color != 'U')
    {
        continue;
    }
    shrunkenFloorPlan[currentSquare.y][currentSquare.x] = otherTile;
    if (currentSquare.x > 0 && shrunkenFloorPlan[currentSquare.y][currentSquare.x - 1].Color == 'U')
    {
        squaresToExplore.Enqueue((currentSquare.x - 1, currentSquare.y), currentSquare.y * 500 + (currentSquare.x - 1));
    }
    if (currentSquare.x < shrunkenFloorPlan[0].Length - 1 && shrunkenFloorPlan[currentSquare.y][currentSquare.x + 1].Color == 'U')
    {
        squaresToExplore.Enqueue((currentSquare.x + 1, currentSquare.y), currentSquare.y * 500 + currentSquare.x + 1);
    }
    if (currentSquare.y > 0 && shrunkenFloorPlan[currentSquare.y - 1][currentSquare.x].Color == 'U')
    {
        squaresToExplore.Enqueue((currentSquare.x, currentSquare.y - 1), (currentSquare.y - 1) * 500 + currentSquare.x);
    }
    if (currentSquare.y < shrunkenFloorPlan.Length - 1 && shrunkenFloorPlan[currentSquare.y + 1][currentSquare.x].Color == 'U')
    {
        squaresToExplore.Enqueue((currentSquare.x, currentSquare.y + 1), (currentSquare.y + 1) * 500 + currentSquare.x);
    }
}

for(int y = 0; y < shrunkenFloorPlan.Length; y++)
{
    for(int x = 0; x < shrunkenFloorPlan[y].Length; x++)
    {
        if(shrunkenFloorPlan[y][x].Color == 'U')
        {
            shrunkenFloorPlan[y][x] = greenTile;
        }
    }
}

Tile[][] tempPlan = new Tile[shrunkenFloorPlan.Length / 2][];
for(int y = 0; y < tempPlan.Length; y++)
{
    tempPlan[y] = new Tile[shrunkenFloorPlan[0].Length / 2];
    for(int x = 0; x < tempPlan[y].Length; x++)
    {
        tempPlan[y][x] = shrunkenFloorPlan[y * 2][x * 2];
    }
}
shrunkenFloorPlan = tempPlan;

Console.WriteLine(GetTilePairs(redTiles).Max(square => square.size));

IEnumerable<(RedTile, RedTile, long size)> GetTilePairs(List<RedTile> tiles)
{
    for (int i = 0; i < tiles.Count; i++)
    {
        for (int j = i + 1; j < tiles.Count; j++)
        {
            int minX = Math.Min(tiles[i].VirtualX, tiles[j].VirtualX);
            int maxX = Math.Max(tiles[i].VirtualX, tiles[j].VirtualX);
            int minY = Math.Min(tiles[i].VirtualY, tiles[j].VirtualY);
            int maxY = Math.Max(tiles[i].VirtualY, tiles[j].VirtualY);
            if (shrunkenFloorPlan.Skip(minY).Take(maxY - minY + 1).SelectMany(row => row.Skip(minX).Take(maxX - minX + 1)).All(tile => tile.Color != '.'))
            {
                long size = (Math.Abs(tiles[i].ActualX - tiles[j].ActualX) + 1) * (Math.Abs(tiles[i].ActualY - tiles[j].ActualY) + 1);
                yield return (tiles[i], tiles[j], size);
            }
        }
    }
}


//Console.WriteLine(GetTilePairs(redTiles).Max(square => square.size));


/*IEnumerable<((long x, long y), (long x, long y), long size)> GetTilePairs(List<(long x, long y)> tiles)
{
    for (int i = 0; i < tiles.Count; i++)
    {
        for (int j = i + 1; j < tiles.Count; j++)
        {
            long size = (Math.Abs(tiles[i].x - tiles[j].x) + 1) * (Math.Abs(tiles[i].y - tiles[j].y) + 1);
            yield return (tiles[i], tiles[j], size);
        }
    }
}*/