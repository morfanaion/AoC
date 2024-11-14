// See https://aka.ms/new-console-template for more information

using Day14Puzzle01;

List<Structure> structures = new List<Structure>();
foreach(string line in File.ReadAllLines("Input.txt"))
{
    Structure structure = new Structure();
    foreach(string pointString in line.Split(" -> "))
    {
        structure.Add(new Point(pointString));
    }
    structures.Add(structure);
}

int maxX = structures.SelectMany(s => s).Select(p => p.X).Max() + 1;
int maxY = structures.SelectMany(s => s).Select(p => p.Y).Max() + 1;

int minX = structures.SelectMany(s => s).Select(p => p.X).Min() - 1;

char[][] grid = new char[maxY][];
for(int y = 0; y < maxY; y++)
{
    grid[y] = new char[maxX - minX];
    for (int x = 0; x < (maxX - minX) ; x++)
    {
        grid[y][x] = '.';
    }
}

foreach(Structure structure in structures)
{
    Point? previousPoint = null;
    foreach(Point point in structure)
    {
        if(previousPoint != null)
        {
            if (previousPoint.X == point.X)
            {
                if(previousPoint.Y < point.Y)
                {
                    for (int y = previousPoint.Y; y < point.Y; y++)
                    {
                        grid[y][point.X - minX] = '#';
                    }
                }
                else
                {
                    for (int y = point.Y; y < previousPoint.Y; y++)
                    {
                        grid[y][point.X - minX] = '#';
                    }
                }
                
            }
            else
            {
                if (previousPoint.X < point.X)
                {
                    for (int x = previousPoint.X; x < point.X; x++)
                    {
                        grid[point.Y][x - minX] = '#';
                    }
                }
                else
                {
                    for (int x = point.X; x < previousPoint.X; x++)
                    {
                        grid[point.Y][x - minX] = '#';
                    }
                }
            }
        }
        grid[point.Y][point.X - minX] = '#';
        previousPoint = point;
    }
}
grid[0][500 - minX] = '+';
int i = 0;
while (PerformDrop())
{
    i++;
}

Console.WriteLine(string.Join('\n', grid.Select(y => string.Concat(y))));
Console.WriteLine(i);

bool PerformDrop()
{
    int x = 500 - minX;
    int y = 0;
    while (true)
    {
        if (grid[y + 1][x] == '.')
        {
            y++;
        }
        else if (grid[y + 1][x - 1] == '.')
        {
            x--;
            y++;
        }
        else if (grid[y + 1][x + 1] == '.')
        {
            x++;
            y++;
        }
        else
        {
            grid[y][x] = 'o';
            return true;
        }

        if (x == 0 || y == maxY - 1 || x == maxX - 1)
        {
            return false;
        }
    }
}

