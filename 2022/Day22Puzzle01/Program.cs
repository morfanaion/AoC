// See https://aka.ms/new-console-template for more information

using Day22Puzzle01;

Tile? currentTile = null;
string instructionLine = string.Empty;
bool instructionComing = false;
Tile?[]? previousLine = null;
int linecounter = 0;
foreach(string line in File.ReadAllLines("Input.txt"))
{
    if(string.IsNullOrEmpty(line))
    {
        instructionComing = true;
        continue;
    }
    if(instructionComing)
    {
        instructionLine = line;
    }
    else
    {
        Tile?[] currentLine = line.Select(c => c == ' ' ? null : new Tile() { TileType = (TileType)c }).ToArray();
        if(previousLine == null)
        {
            previousLine = currentLine;
            currentTile = currentLine.First(t => t != null);
        }
        for(int i = 0; i < currentLine.Length; i++)
        {
            if (currentLine[i] is Tile current)
            {
                current.X = i + 1;
                current.Y = linecounter + 1;
                if (i > 0 && currentLine[i - 1] is Tile left)
                {
                    current.SetLeft(left);
                }
                if (i < previousLine.Length && previousLine[i] is Tile up)
                {
                    current.SetUp(up);
                }
            }
        }
        previousLine = currentLine;
        linecounter++;
    }
}
if(currentTile == null)
{
    return -1;
}

string numberString = string.Empty;
int facing = 0;
Action doMove = () =>
    {
        int numSteps = int.Parse(numberString);
        numberString = string.Empty;
        Func<Tile> nextTile = () => currentTile;
        switch(facing)
        {
            case 0:
                nextTile = () => currentTile.Right;
                break;
            case 1:
                nextTile = () => currentTile.Down;
                break;
            case 2:
                nextTile = () => currentTile.Left;
                break;
            case 3:
                nextTile = () => currentTile.Up;
                break;
        }
        for(int i = 0; i < numSteps; i++)
        {
            Tile next = nextTile();
            if(next.TileType == TileType.Wall)
            {
                break;
            }
            currentTile = next;
        }
    };

foreach(char c in instructionLine)
{
    switch(c)
    {
        case 'R':
            doMove();
            facing = (facing + 1) % 4;
            break;
        case 'L':
            doMove();
            facing = (facing + 3) % 4;
            break;
        default:
            numberString += c;
            break;
    }
}

Console.WriteLine(1000 * currentTile.Y + 4 * currentTile.X + facing);
return 0;