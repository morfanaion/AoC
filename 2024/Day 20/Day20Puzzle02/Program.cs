const int cheatSize = 20;
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
List<(int x, int y)> pathPieces = [startPos];
while (theMaze[currentPos.y][currentPos.x].c != 'E')
{
    currentPos = NextPosition(currentPos.x, currentPos.y, numStepstaken);
    theMaze[currentPos.y][currentPos.x].cost = ++numStepstaken;
    pathPieces.Add(currentPos);
}

Dictionary<int, int> timeSaves = new Dictionary<int, int>();
foreach (((int x, int y) start, (int x, int y) end) in GetAllCombinations(pathPieces))
{
    int absoluteDistance = Math.Abs(start.x - end.x) + Math.Abs(start.y - end.y);
    if (absoluteDistance <= cheatSize && theMaze[end.y][end.x].cost > theMaze[start.y][start.x].cost + absoluteDistance)
    {
        int timeSaved = theMaze[end.y][end.x].cost - (theMaze[start.y][start.x].cost + absoluteDistance);
        if (timeSaves.ContainsKey(timeSaved))
        {
            timeSaves[timeSaved]++;
        }
        else
        {
            timeSaves.Add(timeSaved, 1);
        }
    }
}

Console.WriteLine(timeSaves.Where(kvp => kvp.Key >= 100).Sum(kvp => kvp.Value));

IEnumerable<(T, T)> GetAllCombinations<T>(IEnumerable<T> enumerable)
{
    foreach (T t1 in enumerable)
    {
        foreach (T t2 in enumerable)
        {
            yield return (t1, t2);
        }
    }
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
