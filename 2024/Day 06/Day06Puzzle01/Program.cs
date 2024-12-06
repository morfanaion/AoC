
char[][] theGrid = File.ReadAllLines("input.txt").Select(line => line.ToArray()).ToArray();

int curX = 0;
int curY = 0;
int dx = 0;
int dy = -1;
for(int x =0; x < theGrid[0].Length; x++)
{
    for(int y = 0; y< theGrid.Length; y++)
    {
        if (theGrid[y][x] == '^')
        {
            curX = x;
            curY = y;
        }
    }
}

while (curX >= 0 && curY >= 0 && curX < theGrid[0].Length && curY < theGrid.Length)
{
    theGrid[curY][curX] = 'X';
    if (curX + dx < 0 || curY + dy < 0 || curX + dx >= theGrid[0].Length || curY + dy >= theGrid.Length)
    {
        break;
    }
    if(theGrid[curY + dy][curX + dx] == '#')
    {
        switch((dx, dy))
        {
            case (1, 0):
                dx = 0;
                dy = 1;
                break;
            case (0, 1):
                dx = -1;
                dy = 0;
                break;
            case (-1, 0):
                dx = 0;
                dy = -1;
                break;
            case (0, -1):
                dx = 1;
                dy = 0;
                break;
            default:
                throw new InvalidDataException();
        }
    }
    curX += dx;
    curY += dy;
}

Console.WriteLine(theGrid.SelectMany(y => y).Count(c => c == 'X'));

void PrintGrid(char[][] theGrid, int curX, int curY)
{
    for (int y = 0; y < theGrid.Length; y++)
    {
        if(y == curY)
        {
            Console.WriteLine(string.Concat(theGrid[y].Take(curX).Concat(['^']).Concat(theGrid[y].Skip(curX + 1))));
        }
        else
        {
            Console.WriteLine(string.Concat(theGrid[y]));
        }
    }
    Console.WriteLine();
}