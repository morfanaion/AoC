// See https://aka.ms/new-console-template for more information

using Day14Puzzle01;

List<Structure> structures = new List<Structure>();
foreach (string line in File.ReadAllLines("Input.txt"))
{
    Structure structure = new Structure();
    foreach (string pointString in line.Split(" -> "))
    {
        structure.Add(new Point(pointString));
    }
    structures.Add(structure);
}

int maxY = structures.SelectMany(s => s).Select(p => p.Y).Max() + 3;
int maxX = 2 * maxY;
int offset = 500 - maxY;

char[][] grid = new char[maxY][];
for (int y = 0; y < maxY; y++)
{
    grid[y] = new char[maxX];
    for (int x = 0; x < maxX; x++)
    {
        grid[y][x] = '.';
    }
}

foreach (Structure structure in structures)
{
    Point? previousPoint = null;
    foreach (Point point in structure)
    {
        if (previousPoint != null)
        {
            if (previousPoint.X == point.X)
            {
                if (previousPoint.Y < point.Y)
                {
                    for (int y = previousPoint.Y; y < point.Y; y++)
                    {
                        grid[y][point.X - offset] = '#';
                    }
                }
                else
                {
                    for (int y = point.Y; y < previousPoint.Y; y++)
                    {
                        grid[y][point.X - offset] = '#';
                    }
                }

            }
            else
            {
                if (previousPoint.X < point.X)
                {
                    for (int x = previousPoint.X; x < point.X; x++)
                    {
                        grid[point.Y][x - offset] = '#';
                    }
                }
                else
                {
                    for (int x = point.X; x < previousPoint.X; x++)
                    {
                        grid[point.Y][x - offset] = '#';
                    }
                }
            }
        }
        grid[point.Y][point.X - offset] = '#';
        previousPoint = point;
    }
}

for (int x = 0; x < maxX; x++)
{
    grid[maxY - 1][x] = '#';
}
grid[0][500-offset] = '+';
int i = 0;
while (grid[0][500 - offset] != 'o')
{
    PerformDrop();
    i++;
}
File.WriteAllLines("Output.txt", grid.Select(y => string.Concat(y)));
Console.WriteLine(i);

void PerformDrop()
{
    int x = 500 - offset;
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
            return;
        }
    }
}

