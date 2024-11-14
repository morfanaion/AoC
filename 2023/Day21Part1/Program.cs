using Day21Part1;

List<List<Block>> blockGrid = new List<List<Block>>();
Action<string> handleLine = l =>
{
    blockGrid.Add(l.Select(c => new Block(c == '#' ? BlockType.Wall : BlockType.Plot, c == 'S')).ToList());
    List<Block> previousLine = blockGrid[0];
    for (int i = 1; i < previousLine.Count; i++)
    {
        previousLine[i].SetWest(previousLine[i - 1]);
    }
    handleLine = l =>
    {
        List<Block> currentLine = l.Select(c => new Block(c == '#' ? BlockType.Wall : BlockType.Plot, c == 'S')).ToList();
        blockGrid.Add(currentLine);
        for (int i = 0; i < previousLine.Count; i++)
        {
            currentLine[i].SetNorth(previousLine[i]);
        }
        previousLine = currentLine;
    };
};
foreach(string line in File.ReadAllLines("input.txt"))
{
    handleLine(line);
}
foreach (Block block in blockGrid.SelectMany(row => row))
{
    block.UnneighbourWalls();
}

Block startBlock = blockGrid.SelectMany(row => row).Single(b => b.IsStart);
DijkstraTo(startBlock, 64);

Console.WriteLine(blockGrid.SelectMany(row => row).Count(b => b.VisitedInNumSteps.Contains(64)));

void DijkstraTo(Block startBlock, int maxSteps)
{
    Queue<(Block block, int steps)> queue = new Queue<(Block block, int steps)>();
    queue.Enqueue((startBlock, 0));
    while(queue.Count > 0)
    {
        (Block block, int steps) = queue.Dequeue();
        if(steps == maxSteps)
        {
            continue;
        }
        foreach(Block nextBlock in block.GetNeighboursForStep(steps + 1))
        {
            queue.Enqueue((nextBlock, steps + 1));
        }
    }
}