using Day10Part2;

Pipe.ThePipeGrid = File.ReadAllLines("input.txt").Select(line => line.Select(c => new Pipe() { Type = c }).ToArray()).ToArray();
Pipe startPipe = GetStartPipe(out int x, out int y);
Queue<(Pipe pipe, int x, int y, long distance)> pipeDistanceSetQueue = new Queue<(Pipe, int, int, long)>();
pipeDistanceSetQueue.Enqueue(new(startPipe, x, y, 0));
while (pipeDistanceSetQueue.Count > 0)
{
    (Pipe pipe, int x, int y, long distance) currentEntry = pipeDistanceSetQueue.Dequeue();
    currentEntry.pipe.SetDistance(currentEntry.x, currentEntry.y, (pipe, x, y, distance) => pipeDistanceSetQueue.Enqueue(new(pipe, x, y, distance)), currentEntry.distance);
}
// clear all pipes that aren't part of the system
foreach(Pipe pipe in Pipe.ThePipeGrid.SelectMany(line => line).Where(p => p.MyDistance == long.MaxValue))
{
    pipe.Type = '.';
}
File.WriteAllLines("output1.txt", Pipe.ThePipeGrid.Select(line => string.Join(' ', line.Select(p => p.Type))));
Pipe[][] newGrid = new Pipe[Pipe.ThePipeGrid.Length * 2 + 1][];
newGrid[0] = Enumerable.Range(0, Pipe.ThePipeGrid[y].Length * 2 + 1).Select(n =>  new Pipe() { Type = '.' }).ToArray();
for (y = 0; y < Pipe.ThePipeGrid.Length; y++)
{
    newGrid[y * 2 + 2] = Enumerable.Range(0, Pipe.ThePipeGrid[y].Length * 2 + 1).Select(n => new Pipe() { Type = '.' }).ToArray();
    newGrid[y * 2 + 1] = Enumerable.Range(0, Pipe.ThePipeGrid[y].Length * 2 + 1).Select(n => new Pipe() { Type = '.' }).ToArray();
    for (x = 0; x < Pipe.ThePipeGrid[y].Length; x++)
    {
        newGrid[y * 2 + 1][x * 2 + 1] = Pipe.ThePipeGrid[y][x];
        switch (Pipe.ThePipeGrid[y][x].Type)
        {
            case '|':
                newGrid[y * 2][x * 2 + 1].Type = '|';
                newGrid[y * 2 + 2][x * 2 + 1].Type = '|';
                break;
            case '-':
                newGrid[y * 2 + 1][x * 2].Type = '-';
                newGrid[y * 2 + 1][x * 2 + 2].Type = '-';
                break;
            case 'F':
                newGrid[y * 2 + 1][x * 2 + 2].Type = '-';
                newGrid[y * 2 + 2][x * 2 + 1].Type = '|';
                break;
            case '7':
                newGrid[y * 2 + 1][x * 2].Type = '-';
                newGrid[y * 2 + 2][x * 2 + 1].Type = '|';
                break;
            case 'L':
                newGrid[y * 2 + 1][x * 2 + 2].Type = '-';
                newGrid[y * 2][x * 2 + 1].Type = '|';
                break;
            case 'J':
                newGrid[y * 2 + 1][x * 2].Type = '-';
                newGrid[y * 2][x * 2 + 1].Type = '|';
                break;
        }
    }
}
File.WriteAllLines("output2.txt", newGrid.Select(line => string.Join(' ', line.Select(p => p.Type))));
Queue<(Pipe pipe, int x, int y)> dropWaterQueue = new Queue<(Pipe, int, int)>();
dropWaterQueue.Enqueue(new(newGrid[0][0], 0, 0));
while(dropWaterQueue.Count > 0)
{
    (Pipe pipe, int x, int y) currentItem = dropWaterQueue.Dequeue();
    if(currentItem.pipe.Type != '.')
    {
        continue;
    }
    currentItem.pipe.Type = '0';
    if(currentItem.x > 0)
    {
        dropWaterQueue.Enqueue(new (newGrid[currentItem.y][currentItem.x - 1], currentItem.x - 1, currentItem.y));
    }
    if (currentItem.y > 0)
    {
        dropWaterQueue.Enqueue(new(newGrid[currentItem.y - 1][currentItem.x], currentItem.x, currentItem.y - 1));
    }
    if(currentItem.x < newGrid[y].Length - 1)
    {
        dropWaterQueue.Enqueue(new(newGrid[currentItem.y][currentItem.x + 1], currentItem.x + 1, currentItem.y));
    }
    if (currentItem.y < newGrid.Length - 1)
    {
        dropWaterQueue.Enqueue(new(newGrid[currentItem.y + 1][currentItem.x], currentItem.x, currentItem.y + 1));
    }
}
File.WriteAllLines("output3.txt", newGrid.Select(line => string.Join(' ', line.Select(p => p.Type))));
File.WriteAllLines("output4.txt", Pipe.ThePipeGrid.Select(line => string.Join(' ', line.Select(p => p.Type))));
Console.WriteLine(Pipe.ThePipeGrid.SelectMany(line => line).Count(p => p.Type == '.'));

Pipe GetStartPipe(out int x, out int y)
{
    for (x = 0; x < Pipe.ThePipeGrid[0].Length; x++)
    {
        for (y = 0; y < Pipe.ThePipeGrid.Length; y++)
        {
            if (Pipe.ThePipeGrid[y][x].Type == 'S')
            {
                return Pipe.ThePipeGrid[y][x];
            }
        }
    }
    x = -1;
    y = -1;
    return Pipe.ThePipeGrid[0][0];
}