using Day21Part2;
const long NumSteps = 1000000;
List<List<Block>> blockGrid = new List<List<Block>>();
Action<string> handleLine = l =>
{
    int x = 0;
    int y = 0;
    blockGrid.Add(l.Select(c => new Block(x++, y, c == '#' ? BlockType.Wall : BlockType.Plot, c == 'S')).ToList());
    List<Block> previousLine = blockGrid[0];
    Block northeast = previousLine[0];
    northeast.North = northeast;
    northeast.South = northeast;
    northeast.West = northeast;
    northeast.East = northeast;
    for (int i = 1; i < previousLine.Count; i++)
    {
        Block block = previousLine[i];
        block.SetWest(previousLine[i - 1]);
        block.South = block;
        block.North = block;
    }
    handleLine = l =>
    {
        x = 0;
        y++;
        List<Block> currentLine = l.Select(c => new Block(x++, y, c == '#' ? BlockType.Wall : BlockType.Plot, c == 'S')).ToList();
        blockGrid.Add(currentLine);
        for (int i = 0; i < previousLine.Count; i++)
        {
            Block newBlock = currentLine[i];
            if (i == 0)
            {
                newBlock.West = newBlock;
                newBlock.East = newBlock;
            }
            newBlock.SetNorth(previousLine[i]);
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
DijkstraTo(startBlock, NumSteps);

Console.WriteLine(blockGrid.SelectMany(row => row).Sum(b => b.GetNumForSteps(26501365)));


void DijkstraTo(Block startBlock, long maxSteps)
{
    Queue<(Block block, int newX, int newY, long steps)> queue = new Queue<(Block block, int newX, int newY, long steps)>();
    queue.Enqueue((startBlock, 0, 0, 0));
    while(queue.Count > 0)
    {
        (Block block, int x, int y, long steps) = queue.Dequeue();
        if(steps == maxSteps)
        {
            continue;
        }
        foreach((Block newBlock, int newX, int newY) in block.GetNeighboursForStep(steps + 1, x, y))
        {
            queue.Enqueue((newBlock, newX, newY, steps + 1));
        }
    }
}