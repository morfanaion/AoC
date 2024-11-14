// See https://aka.ms/new-console-template for more information

using Day22Puzzle02;

Tile? currentTile = null;
string instructionLine = string.Empty;
bool instructionComing = false;
Tile?[]? previousLine = null;
int linecounter = 0;
CubeFace?[][] cubefaces = new CubeFace?[5][];

for(int x = 0; x < 5; x++)
{
    cubefaces[x] = new CubeFace[5];
    for(int y = 0; y < 5; y++)
    {
        cubefaces[x][y] = null;
    }
}

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
                if(linecounter % CubeFace.FaceSize == 0 && i % CubeFace.FaceSize == 0)
                {
                    cubefaces[i / CubeFace.FaceSize][linecounter / CubeFace.FaceSize] = new CubeFace() { X = i / CubeFace.FaceSize, Y = linecounter / CubeFace.FaceSize };
                }
                current.X = i + 1;
                current.Y = linecounter + 1;
                if (cubefaces[i / CubeFace.FaceSize][linecounter / CubeFace.FaceSize] is CubeFace thisCubeFace)
                {
                    current.SetCubeFace(thisCubeFace);
                }
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

for(int x = 0; x < 4; x++)
{
    for(int y = 0; y < 4; y++)
    {
        if (cubefaces[x][y] is CubeFace currentFace)
        {
            if(currentFace.FaceUp == null)
            {
                SearchFace(x, y, (x, y) => cubefaces[x][y], currentFace.SetFaceUp);
            }
            if(currentFace.FaceToTheRight == null)
            {
                SearchFace(y, (-x) + 4, (x, y) => cubefaces[(-y) + 4][x], currentFace.SetFaceToTheRight);
            }
            if (currentFace.FaceDown == null)
            {
                SearchFace((-x) + 4, (-y) + 4, (x, y) => cubefaces[(-x) + 4][(-y) + 4], currentFace.SetFaceDown);
            }
            if (currentFace.FaceToTheLeft == null)
            {
                SearchFace((-y) + 4, x, (x, y) => cubefaces[y][(-x) + 4], currentFace.SetFaceToTheLeft);
            }
        }
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
        
        for(int i = 0; i < numSteps; i++)
        {
            Tile next;
            switch (facing)
            {
                case 0:
                    next = currentTile.Right;
                    break;
                case 1:
                    next = currentTile.Down;
                    break;
                case 2:
                    next = currentTile.Left;
                    break;
                case 3:
                    next = currentTile.Up;
                    break;
                default:
                    throw new InvalidOperationException();
            }
            if (next.TileType == TileType.Wall)
            {
                break;
            }
            Tile prevTile = currentTile;
            CubeFace? currentFace = currentTile.CubeFace;
            currentTile = next;
            if (next.CubeFace != null && currentFace != null && next.CubeFace != currentFace)
            {
                facing = (facing + currentFace.OrientationChanges[next.CubeFace]) % 4;
            }
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
if(!string.IsNullOrEmpty(numberString))
{
    doMove();
}

Console.WriteLine(1000 * currentTile.Y + 4 * currentTile.X + facing);
return 0;


void SearchFace(int x, int y, Func<int, int, CubeFace?> getCubeface, Action<CubeFace, int> setOtherCubeface)
{
    CubeFace? otherFace;
    if (y > 0 && (otherFace = getCubeface(x, y - 1)) != null)
    {
        setOtherCubeface(otherFace, 0);
        return;
    }
    if (x > 0 && getCubeface(x - 1, y) != null)
    {
        if (y > 0 && (otherFace = getCubeface(x - 1, y - 1)) != null)
        {
            setOtherCubeface(otherFace, 3);
            return;
        }
        if (x > 1 && getCubeface(x - 2, y) != null)
        {
            if ((otherFace = getCubeface(x - 2, y - 1)) != null)
            {
                setOtherCubeface(otherFace, 2);
                return;
            }
            if (x > 2 && getCubeface(x - 3, y) != null && (otherFace = getCubeface(x - 3, y - 1)) != null)
            {
                setOtherCubeface(otherFace, 1);
                return;
            }
            if (y < 4 && x > 3 && getCubeface(x - 2, y + 1) != null && getCubeface(x - 3, y + 1) != null && (otherFace = getCubeface(x - 4, y + 1)) != null)
            {
                setOtherCubeface(otherFace, 0);
                return;
            }
        }
        if (y < 2 && getCubeface(x - 1, y + 1) != null)
        {
            if (x > 2 && getCubeface(x - 2, y + 1) != null)
            {
                if ((otherFace = getCubeface(x - 3, y + 1)) != null)
                {
                    setOtherCubeface(otherFace, 1);
                    return;
                }
                if (y < 3 && getCubeface(x - 2, y + 2) != null && (otherFace = getCubeface(x - 3, y + 2)) != null)
                {
                    setOtherCubeface(otherFace, 0);
                    return;
                }
            }
            if (y < 2 && getCubeface(x - 1, y + 2) != null)
            {
                if (x > 1 && getCubeface(x - 2, y + 2) != null && (otherFace = getCubeface(x - 2, y + 3)) != null)
                {
                    setOtherCubeface(otherFace, 0);
                    return;
                }
                if ((otherFace = getCubeface(x - 1, y + 3)) != null)
                {
                    setOtherCubeface(otherFace, 1);
                    return;
                }
            }
        }
    }
    if (y < 4 && getCubeface(x, y + 1) != null)
    {
        if (x > 1 && getCubeface(x - 1, y + 1) != null)
        {
            if ((otherFace = getCubeface(x - 2, y + 1)) != null)
            {
                setOtherCubeface(otherFace, 2);
                return;
            }
            if (y < 3 && getCubeface(x - 1, y + 2) != null)
            {
                if ((otherFace = getCubeface(x - 2, y + 2)) != null)
                {
                    setOtherCubeface(otherFace, 3);
                    return;
                }
                if (y < 2 && getCubeface(x - 1, y + 3) != null && (otherFace = getCubeface(x - 2, y + 3)) != null)
                {
                    setOtherCubeface(otherFace, 0);
                    return;
                }
            }
        }
        if (y < 2 && getCubeface(x, y + 2) != null)
        {
            if (x > 0 && getCubeface(x - 1, y + 2) != null && (otherFace = getCubeface(x - 1, y + 3)) != null)
            {
                setOtherCubeface(otherFace, 3);
                return;
            }
            if ((otherFace = getCubeface(x, y + 3)) != null)
            {
                setOtherCubeface(otherFace, 0);
                return;
            }
            if (x < 3 && getCubeface(x + 1, y + 2) != null && (otherFace = getCubeface(x + 1, y + 3)) != null)
            {
                setOtherCubeface(otherFace, 1);
                return;
            }
        }
        if (x < 3 && getCubeface(x + 1, y + 1) != null)
        {
            if ((otherFace = getCubeface(x + 2, y + 1)) != null)
            {
                setOtherCubeface(otherFace, 2);
                return;
            }
            if (y < 2 && getCubeface(x + 1, y + 2) != null)
            {
                if ((otherFace = getCubeface(x + 2, y + 2)) != null)
                {
                    setOtherCubeface(otherFace, 1);
                    return;
                }
                if (y < 1 && getCubeface(x + 1, y + 3) != null && (otherFace = getCubeface(x + 2, y + 3)) != null)
                {
                    setOtherCubeface(otherFace, 0);
                    return;
                }
            }
        }
    }
    if (x < 4 && getCubeface(x + 1, y) != null)
    {
        if (y > 0 && (otherFace = getCubeface(x + 1, y - 1)) != null)
        {
            setOtherCubeface(otherFace, 1);
            return;
        }
        if (x < 3 && getCubeface(x + 2, y) != null)
        {
            if ((otherFace = getCubeface(x + 2, y - 1)) != null)
            {
                setOtherCubeface(otherFace, 2);
                return;
            }
            if (x < 2 && getCubeface(x + 3, y) != null && (otherFace = getCubeface(x + 3, y - 1)) != null)
            {
                setOtherCubeface(otherFace, 3);
                return;
            }
            if (y < 4 && x < 1 && getCubeface(x + 2, y + 1) != null && getCubeface(x + 3, y + 1) != null && (otherFace = getCubeface(x + 4, y + 1)) != null)
            {
                setOtherCubeface(otherFace, 0);
                return;
            }
        }
        if (y < 4 && getCubeface(x + 1, y + 1) != null)
        {
            if(x < 2 && getCubeface(x + 2, y + 1) != null)
            {
                if((otherFace = getCubeface(x + 3, y + 1)) != null)
                {
                    setOtherCubeface(otherFace, 3);
                    return;
                }
                if(y < 3 && getCubeface(x + 2, y +2) != null && (otherFace = getCubeface(x + 3, y + 2)) != null)
                {
                    setOtherCubeface(otherFace, 0);
                    return;
                }
            }
            if (y < 2 && getCubeface(x + 1, y + 2) != null)
            {
                if (x < 3 && getCubeface(x + 2, y + 2) != null && (otherFace = getCubeface(x + 2, y + 3)) != null)
                {
                    setOtherCubeface(otherFace, 0);
                    return;
                }
                if ((otherFace = getCubeface(x + 1, y + 3)) != null)
                {
                    setOtherCubeface(otherFace, 3);
                    return;
                }
            }
        }
    }
}