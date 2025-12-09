List<(long x, long y)> redTiles = File.ReadAllLines("input.txt")
    .Select(line => line.Split(','))
    .Select(parts => (x: long.Parse(parts[0]), y: long.Parse(parts[1])))
    .ToList();

Console.WriteLine(GetTilePairs(redTiles).Max(square => square.size));


IEnumerable<((long x, long y), (long x, long y), long size)> GetTilePairs(List<(long x, long y)> tiles)
{
    for (int i = 0; i < tiles.Count; i++)
    {
        for (int j = i + 1; j < tiles.Count; j++)
        {
            long size = (Math.Abs(tiles[i].x - tiles[j].x) + 1) * (Math.Abs(tiles[i].y - tiles[j].y) + 1);
            yield return (tiles[i], tiles[j], size);
        }
    }
}