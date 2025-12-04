char[][] totalGrid = File.ReadAllLines("input.txt").Select(line => line.ToCharArray()).ToArray();

int totalRemoved = 0;
int numRemovedThisRound = 0;
do
{
    (int x, int y)[] removablePositions = GetRemovablePositions(totalGrid).ToArray();
    numRemovedThisRound = removablePositions.Length;
    foreach((int x, int y) in removablePositions)
    {
        totalGrid[y][x] = '.';
    }
    totalRemoved += numRemovedThisRound;
} while (numRemovedThisRound > 0);
Console.WriteLine(totalRemoved);

IEnumerable<(int x, int y)> GetRemovablePositions(char[][] theGrid)
{
    for (int y = 0; y < theGrid.Length; y++)
    {
        for (int x = 0; x < theGrid[y].Length; x++)
        {
            if (theGrid[y][x] != '@')
            {
                continue;
            }
            int numAdjecent = 0;
            if (x > 0)
            {
                if (y > 0 && theGrid[y - 1][x - 1] == '@')
                {
                    numAdjecent++;
                }
                if (theGrid[y][x - 1] == '@')
                {
                    numAdjecent++;
                }
                if (y < theGrid.Length - 1 && theGrid[y + 1][x - 1] == '@')
                {
                    numAdjecent++;
                }
            }
            if (y > 0 && theGrid[y - 1][x] == '@')
            {
                numAdjecent++;
            }
            if (y < theGrid.Length - 1 && theGrid[y + 1][x] == '@')
            {
                numAdjecent++;
            }
            if (x < theGrid[y].Length - 1)
            {
                if (y > 0 && theGrid[y - 1][x + 1] == '@')
                {
                    numAdjecent++;
                }
                if (theGrid[y][x + 1] == '@')
                {
                    numAdjecent++;
                }
                if (y < theGrid.Length - 1 && theGrid[y + 1][x + 1] == '@')
                {
                    numAdjecent++;
                }
            }
            if (numAdjecent < 4)
            {
                yield return (x, y);
            }
        }
    }

}

