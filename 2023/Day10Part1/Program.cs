// See https://aka.ms/new-console-template for more information
using Day10Part1;
using System.Net.Http.Headers;

Pipe.ThePipeGrid = File.ReadAllLines("input.txt").Select(line => line.Select(c => new Pipe() { Type = c }).ToArray()).ToArray();
Pipe startPipe = GetStartPipe(out int x, out int y);
Queue<(Pipe pipe, int x, int y, long distance)> pipeDistanceSetQueue = new Queue<(Pipe, int, int, long)>();
pipeDistanceSetQueue.Enqueue(new(startPipe, x, y, 0));
while (pipeDistanceSetQueue.Count > 0)
{
    (Pipe pipe, int x, int y, long distance) currentEntry = pipeDistanceSetQueue.Dequeue();
    currentEntry.pipe.SetDistance(currentEntry.x, currentEntry.y, (pipe, x, y, distance) => pipeDistanceSetQueue.Enqueue(new ( pipe, x, y, distance )), currentEntry.distance);
}
Console.WriteLine(Pipe.ThePipeGrid.SelectMany(line => line).Where(p => p.MyDistance != long.MaxValue).Max(p => p.MyDistance));



Pipe GetStartPipe(out int x, out int y)
{
    for(x = 0; x < Pipe.ThePipeGrid[0].Length; x++)
    {
        for(y = 0; y < Pipe.ThePipeGrid.Length; y++)
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