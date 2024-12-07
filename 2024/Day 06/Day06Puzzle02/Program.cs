using System.Collections;

char[][] theGrid = File.ReadAllLines("input.txt").Select(line => line.ToArray()).ToArray();
char[][] originalGrid = theGrid.Select(line => line.ToArray()).ToArray();
int startX = 0;
int startY = 0;
int dx = 0;
int dy = -1;
for(int x = 0; x < theGrid[0].Length; x++)
{
    for(int y = 0; y< theGrid.Length; y++)
    {
        if (theGrid[y][x] == '^')
        {
            startX = x;
            startY = y;
        }
    }
}
int curX = startX;
int curY = startY;


while (curX >= 0 && curY >= 0 && curX < theGrid[0].Length && curY < theGrid.Length)
{
    theGrid[curY][curX] = 'X';
    if (curX + dx < 0 || curY + dy < 0 || curX + dx >= theGrid[0].Length || curY + dy >= theGrid.Length)
    {
        break;
    }
    while(theGrid[curY + dy][curX + dx] == '#')
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
theGrid[startY][startX] = '.';

int numOptions = 0;
for(int x = 0; x < theGrid[0].Length; x++)
{
    for(int y = 0; y < theGrid.Length; y++)
    {
        //if(x != startX && y != startY)
        if (theGrid[y][x] == 'X')
        {
            if (CausesLoop(originalGrid, x, y, startX, startY))
            {
                numOptions++;
            }
        }
    }
}
Console.WriteLine(numOptions);

bool CausesLoop(char[][] originalGrid, int x, int y, int startX, int startY)
{
    int curX = startX;
    int curY = startY;
    char[][] alteredGrid = originalGrid.Select(line => line.ToArray()).ToArray();
    alteredGrid[y][x] = '#';
    int dx = 0;
    int dy = -1;
    Hashtable hashtable = new Hashtable();
    while (curX >= 0 && curY >= 0 && curX < alteredGrid[0].Length && curY < alteredGrid.Length)
    {
        if(hashtable.ContainsKey((curX, curY, dx, dy)))
        {
            return true;
        }
        hashtable.Add((curX, curY, dx, dy), 0);
        if (curX + dx < 0 || curY + dy < 0 || curX + dx >= alteredGrid[0].Length || curY + dy >= alteredGrid.Length)
        {
            return false;
        }
        
        while (alteredGrid[curY + dy][curX + dx] == '#')
        {
            switch ((dx, dy))
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
    return false;
}