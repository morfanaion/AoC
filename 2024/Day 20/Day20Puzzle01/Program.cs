(char c, int cost)[][] theMaze = File.ReadAllLines("input.txt").Select(line => line.Select(c => (c, int.MaxValue)).ToArray()).ToArray();
(int x, int y) startPos = (-1, -1);
for (int y = 0; y < theMaze.Length; y++)
{
    for(int x = 0; x < theMaze[y].Length; x++)
    {
        if (theMaze[y][x].c == 'S')
        {
            startPos = (x, y);
            theMaze[y][x].cost = 0;
        }
    }
}

int numStepstaken = 0;
(int x, int y) currentPos = startPos;
while(theMaze[currentPos.y][currentPos.x].c != 'E')
{
    currentPos = NextPosition(currentPos.x, currentPos.y, numStepstaken);
    theMaze[currentPos.y][currentPos.x].cost = ++numStepstaken;
}

Dictionary<int, int> timeSaves = new Dictionary<int, int>();

for (int y = 0; y < theMaze.Length; y++)
{
    for (int x = 0; x < theMaze[y].Length; x++)
    {
        if (theMaze[y][x].c == '#' && x > 0 && x + 1 < theMaze[y].Length && y > 0 && y + 1 < theMaze.Length && NeighbourChars(x, y).Count(n => n.c == '#') <= 2)
        {
            foreach(((char c, int x, int y) start, (char c, int x, int y) end) in GetAllCombinations(NeighbourChars(x,y).Where(n => n.c != '#')))
            {
                int timeSaved = theMaze[end.y][end.x].cost - (theMaze[start.y][start.x].cost + 2);
                if(timeSaved > 0)
                {
                    if(timeSaves.ContainsKey(timeSaved))
                    {
                        timeSaves[timeSaved]++;
                    }
                    else
                    {
                        timeSaves.Add(timeSaved, 1);
                    }
                }
            }
            foreach ((char c, int x2, int y2) in NeighbourChars(x, y))
            {
                if (c != '#')
                {
                }
            }
        }
    }
}

int totalOver100 = 0;
foreach(var total in timeSaves.OrderBy(kvp => kvp.Key))
{
    Console.WriteLine($"There are {total.Value} cheats that save {total.Key} picoseconds");
    if(total.Key >= 100)
    {
        totalOver100 += total.Value;
    }
}
Console.WriteLine(totalOver100);

IEnumerable<(T, T)> GetAllCombinations<T>(IEnumerable<T> enumerable)
{
    foreach(T t1 in enumerable)
    {
        foreach (T t2 in enumerable)
        {
            yield return (t1, t2);
        }
    }
}

IEnumerable<(char c, int x, int y)> NeighbourChars(int x, int y)
{
    yield return (theMaze[y][x - 1].c, x - 1, y);
    yield return (theMaze[y - 1][x].c, x, y - 1);
    yield return (theMaze[y][x + 1].c, x + 1, y);
    yield return (theMaze[y + 1][x].c, x, y + 1);
}


(int x, int y) NextPosition(int x, int y, int numStepsTaken)
{
    (char c, int cost) other = theMaze[currentPos.y][currentPos.x - 1];
    if (other.c != '#' && other.c != 'S' && other.cost == int.MaxValue)
    {
        return (currentPos.x - 1, currentPos.y);
    }
    other = theMaze[currentPos.y][currentPos.x + 1];
    if (other.c != '#' && other.c != 'S' && other.cost == int.MaxValue)
    {
        return currentPos = (currentPos.x + 1, currentPos.y);
    }
    other = theMaze[currentPos.y - 1][currentPos.x];
    if (other.c != '#' && other.c != 'S' && other.cost == int.MaxValue)
    {
        return currentPos = (currentPos.x, currentPos.y - 1);
    }
    other = theMaze[currentPos.y + 1][currentPos.x];
    if (other.c != '#' && other.c != 'S' && other.cost == int.MaxValue)
    {
        return currentPos = (currentPos.x, currentPos.y + 1);
    }
    throw new InvalidOperationException("Don't call on a dead end");
}

