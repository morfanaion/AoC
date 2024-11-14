using Day23Part2;

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
        case '#': return TileType.Forest;
        default: return TileType.Path;
    }
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
Step fromStart = new Step(startTile, 0, null);
Step trailForStart = GetTrailToNextJunction(startTile.GetNeighbours(fromStart).Single().step);
startTile.Trails.Add(new Trail(trailForStart.CurrentTile, trailForStart.NumSteps));
foreach(Tile tile in tileGrid.SelectMany(row => row).Where(t => t.NumNeighbours > 2))
{
    tile.AddTrails();
}
TrailStep step = new TrailStep(startTile, 0, null);

Console.WriteLine(Calculate(step, (s) => s.CurrentTile.GetTrailNeighbours(s)));

int Calculate(TrailStep start, Func<TrailStep, IEnumerable<(TrailStep s, int weight)>> getNeighbours)
{
    PriorityQueue<TrailStep, int> queue = new PriorityQueue<TrailStep, int>();
    queue.Enqueue(start, 0);
    Dictionary<TrailStep, (TrailStep s, int weight)> distances = new Dictionary<TrailStep, (TrailStep s, int weight)>();
    distances[start] = (start, 0);
    TrailStep lastStepEvaluated = start;
    List<TrailStep> possibleEndSteps = new List<TrailStep>();
    while (queue.Count > 0)
    {
        if (!queue.TryDequeue(out TrailStep step, out int weight))
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
        foreach ((TrailStep step, int weight) subStep in getNeighbours(step))
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
    TrailStep longest = possibleEndSteps.OrderByDescending(s => s.NumSteps).First();
    return longest.NumSteps;
}

Step GetTrailToNextJunction(Step step)
{
    while(step.CurrentTile.NumNeighbours <=2)
    {
        step = step.CurrentTile.GetNeighbours(step).Single().step;
    }
    return step;
}

void DrawGrid(Step step)
{
    step.SetTravelledTiles();
    foreach(List<Tile> row in tileGrid)
    {
        Console.WriteLine(string.Join("", row.Select(t => t.GetChar())));
    }
}