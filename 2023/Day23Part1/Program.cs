using Day23Part1;

List<List<Tile>> tileGrid = new List<List<Tile>>();
Action<string> handleLine = l =>
{
    int y = 0;
    int x = 0;
    tileGrid.Add(l.Select(c => new Tile(GetTileType(c), x++, y)).ToList());
    List<Tile> previousLine = tileGrid[0];
    for (int i = 1; i < previousLine.Count; i++)
    {
        previousLine[i].SetWest(previousLine[i - 1]);
    }
    handleLine = l =>
    {
        x = 0;
        y++;
        List<Tile> currentLine = l.Select(c => new Tile(GetTileType(c), x++, y)).ToList();
        tileGrid.Add(currentLine);
        for (int i = 0; i < previousLine.Count; i++)
        {
            currentLine[i].SetNorth(previousLine[i]);
        }
        previousLine = currentLine;
    };
};

TileType GetTileType(char c)
{
    switch(c)
    {
        case '.': return TileType.Path;
        case '#': return TileType.Forest;
        case '>': return TileType.SlopeEast;
        case '<': return TileType.SlopeWest;
        case 'v': return TileType.SlopeSouth;
        case '^': return TileType.SlopeNorth;
    }
    throw new Exception("?");
}

foreach (string line in File.ReadAllLines("input.txt"))
{
    handleLine(line);
}
foreach (Tile tile in tileGrid.SelectMany(row => row))
{
    tile.UnneighbourForests();
}

Tile startTile = tileGrid[0].Single(t => t.TileType == TileType.Path);
startTile.IsStart = true;
Tile endTile = tileGrid.Last().Single(t => t.TileType == TileType.Path);
endTile.IsEnd = true;
Step step = new Step(startTile, 0, null);

Console.WriteLine(Calculate(step, (s) => s.CurrentTile.GetNeighbours(s)));

int Calculate(Step start, Func<Step, IEnumerable<(Step s, int weight)>> getNeighbours)
{
    PriorityQueue<Step, int> queue = new PriorityQueue<Step, int>();
    queue.Enqueue(start, 0);
    Dictionary<Step, (Step s, int weight)> distances = new Dictionary<Step, (Step s, int weight)>();
    distances[start] = (start, 0);
    Step lastStepEvaluated = start;
    List<Step> possibleEndSteps = new List<Step>();
    while (queue.Count > 0)
    {
        if (!queue.TryDequeue(out Step step, out int weight))
        {
            throw new InvalidOperationException();
        }
        lastStepEvaluated = step;
        if (distances[step].weight < weight)
        {
            continue;
        }
        if (distances[step].weight > weight)
        {
            throw new InvalidOperationException();
        }
        if (step.CurrentTile.IsEnd)
        {
            possibleEndSteps.Add(step);
        }
        foreach ((Step step, int weight) subStep in getNeighbours(step))
        {
            int nextWeight = weight + subStep.weight;
            if (!distances.TryGetValue(subStep.step, out var entry) || nextWeight < entry.weight)
            {
                distances[subStep.step] = (step, nextWeight);
                queue.Enqueue(subStep.step, nextWeight);
            }
        }
    }
    if (possibleEndSteps.Count == 0)
    {
        return -1;
    }
    Step longest = possibleEndSteps.OrderByDescending(s => s.NumSteps).First();
    return longest.NumSteps;
}

void DrawGrid(Step step)
{
    step.SetTravelledTiles();
    foreach(List<Tile> row in tileGrid)
    {
        Console.WriteLine(string.Join("", row.Select(t => t.GetChar())));
    }
}