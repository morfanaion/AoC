// See https://aka.ms/new-console-template for more information
using Day24Puzzle01;

List<Tile> allTiles = new List<Tile>();
Tile[]? previousLine = null;
int linecounter = 0;
foreach (string line in File.ReadAllLines("Input.txt"))
{
    Tile[] currentLine = line.Select(c => new Tile() { IsWall = c == '#' }).ToArray();
    allTiles.AddRange(currentLine);
    if (previousLine == null)
    {
        previousLine = currentLine;
    }
    for (int i = 0; i < currentLine.Length; i++)
    {
        if (!currentLine[i].IsWall)
        {
            if (line[i] != '.')
            {
                Blizzard.Blizzards.Add(new Blizzard(currentLine[i]) { Direction = line[i] });
            }
        }
        if (currentLine[i] is Tile current)
        {
            current.X = i;
            current.Y = linecounter;
            if (i > 0 && currentLine[i - 1] is Tile left)
            {
                current.SetLeft(left);
            }
            if (i < previousLine.Length && previousLine[i] is Tile up)
            {
                current.SetUp(up);
            }
        }
    }
    previousLine = currentLine;
    linecounter++;
}
Tile.MaxX = allTiles.Max(t => t.X);
Tile.MaxY = allTiles.Max(t => t.Y) - 1;
Tile startingTile = allTiles.Single(t => t.Y == 0 && !t.IsWall);
startingTile.IsStartingTile = true;
Tile targetTile = allTiles.Single(t => t.Y > Tile.MaxY && !t.IsWall);
targetTile.IsTargetTile = true;
List<Tile> TraversableTiles = new List<Tile>();
TraversableTiles.Add(startingTile);
int minute = 0;
while (!TraversableTiles.Any(t => t.IsTargetTile))
{
    minute++;
    Blizzard.Blizzards.ForEach(b => b.Move());
    TraversableTiles = TraversableTiles.SelectMany(t => t.TraversableTiles).Distinct().Where(t => !t.AnyBlizzardsHere).ToList();
}

Console.WriteLine(minute);