// See https://aka.ms/new-console-template for more information
using Day18Puzzle02;

List<CubeEntry> cubeEntries = File.ReadAllLines("Input.txt").Select(line => new CubeEntry(line)).ToList();

int maxX = cubeEntries.Max(e => e.X) + 2;
int maxY = cubeEntries.Max(e => e.Y) + 2;
int maxZ = cubeEntries.Max(e => e.Z) + 2;

char[][][] grid = new char[maxX][][];
for(int x = 0; x < grid.Length; x++)
{
    grid[x] = new char[maxY][];
    for (int y = 0; y < maxY; y++)
    {
        grid[x][y] = new char[maxZ];
        for (int z = 0; z < maxZ; z++)
        {
            if(CubeEntry.CheckPosition(x,y,z, cubeEntries))
            {
                grid[x][y][z] = 'C';
            }
            else
            {
                grid[x][y][z] = ' ';
            }
        }
    }
}
Queue<Position> positionQueue = new Queue<Position>();
positionQueue.Enqueue(new Position() { X = 0, Y = 0, Z = 0 });
grid[0][0][0] = 'A';
while(positionQueue.Count != 0)
{
    Position position = positionQueue.Dequeue();
    if (position.X > 0 && grid[position.X - 1][position.Y][position.Z] == ' ')
    {
        grid[position.X - 1][position.Y][position.Z] = 'A';
        positionQueue.Enqueue(new Position() { X = position.X - 1, Y = position.Y, Z = position.Z });
    }
    if (position.X + 1 < maxX && grid[position.X + 1][position.Y][position.Z] == ' ')
    {
        grid[position.X + 1][position.Y][position.Z] = 'A';
        positionQueue.Enqueue(new Position() { X = position.X + 1, Y = position.Y, Z = position.Z });
    }
    if (position.Y > 0 && grid[position.X][position.Y - 1][position.Z] == ' ')
    {
        grid[position.X][position.Y - 1][position.Z] = 'A';
        positionQueue.Enqueue(new Position() { X = position.X, Y = position.Y - 1, Z = position.Z });
    }
    if (position.Y + 1 < maxY && grid[position.X][position.Y + 1][position.Z] == ' ')
    {
        grid[position.X][position.Y + 1][position.Z] = 'A';
        positionQueue.Enqueue(new Position() { X = position.X, Y = position.Y + 1, Z = position.Z });
    }
    if (position.Z > 0 && grid[position.X][position.Y][position.Z - 1] == ' ')
    {
        grid[position.X][position.Y][position.Z - 1] = 'A';
        positionQueue.Enqueue(new Position() { X = position.X, Y = position.Y, Z = position.Z - 1 });
    }
    if (position.Z + 1 < maxZ && grid[position.X][position.Y][position.Z + 1] == ' ')
    {
        grid[position.X][position.Y][position.Z + 1] = 'A';
        positionQueue.Enqueue(new Position() { X = position.X, Y = position.Y, Z = position.Z + 1 });
    }
}

for (int x = 0; x < grid.Length; x++)
{
    for (int y = 0; y < grid[x].Length; y++)
    {
        for (int z = 0; z < grid[x][y].Length; z++)
        {
            if(grid[x][y][z] == ' ')
            {
                cubeEntries.Add(new CubeEntry($"{x - 2},{y - 2},{z - 2}"));
            }
        }
    }
}

Console.WriteLine(cubeEntries.Sum(entry => entry.ExposedSides(cubeEntries)));