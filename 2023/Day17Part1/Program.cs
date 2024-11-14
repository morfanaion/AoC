using Day17Part1;

List<List<CityBlock>> cityBlocks = new List<List<CityBlock>>();
List<CityBlock>? previousRow = null;
int y = 0;
foreach (string line in File.ReadAllLines("input.txt"))
{
    int x = 0;
    List<CityBlock> currentRow = line.Select(c => new CityBlock() {  HeatLoss = c - '0', X = x++, Y = y}).ToList();
    if (previousRow == null)
    {
        foreach ((CityBlock curr, CityBlock previous) pair in currentRow.Skip(1).Zip<CityBlock, CityBlock, (CityBlock curr, CityBlock previous)>(currentRow, (curr, prev) => new(curr, prev)))
        {
            pair.curr.SetWest(pair.previous);

        }
    }
    else
    {
        foreach ((CityBlock south, CityBlock north) pair in currentRow.Zip<CityBlock, CityBlock, (CityBlock south, CityBlock north)>(previousRow, (south, north) => new(south, north)))
        {
            pair.south.SetNorth(pair.north);
        }
    }
    previousRow = currentRow;
    cityBlocks.Add(currentRow);
    y++;
}

CityBlock start = cityBlocks[0][0];
start.IsStart = true;
cityBlocks.Last().Last().IsEnd = true;
Step startStep = new Step() { CurrentBlock = start, Last = start, Last2nd = start, Last3rd = start };
Console.WriteLine(Calculate(startStep, s => s.CurrentBlock.GetNeighbours(s)));

int Calculate(Step start, Func<Step, IEnumerable<(Step s, int weight)>> getNeighbours)
{
    PriorityQueue<Step, int> queue = new PriorityQueue<Step, int>();
    queue.Enqueue(start, 0);
    Dictionary<Step, (Step s, int weight)> distances = new Dictionary<Step, (Step s, int weight)>();
    distances[start] = (start, 0);
    while (queue.Count > 0)
    {
        if (!queue.TryDequeue(out Step step, out int weight))
        {
            throw new InvalidOperationException();
        }
        if (distances[step].weight < weight)
        {
            continue;
        }
        if (distances[step].weight > weight)
        {
            throw new InvalidOperationException();
        }
        if (step.CurrentBlock.IsEnd)
        {
            return distances[step].weight;
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
    return -1;
}