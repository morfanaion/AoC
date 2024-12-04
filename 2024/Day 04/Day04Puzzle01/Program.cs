
char[][] wordsearch = File.ReadAllLines("input.txt").Select(l => l.ToArray()).ToArray();

int numX = wordsearch[0].Length;
int numY = wordsearch.Length;
int numXmas = 0;
for(int x = 0; x < numX; x++)
{
    for(int y = 0; y < numY; y++)
    {
        if (wordsearch[y][x] == 'X')
        {
            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    if (IsXMas(x, y, dx, dy))
                    {
                        numXmas++;
                    }
                }
            }
        }
    }
}
Console.WriteLine(numXmas);

bool IsXMas(int x, int y, int dx, int dy)
{
    const string XMAS = "XMAS";
    for(int i = 1; i <= 3; i++)
    {
        x += dx;
        y += dy;
        if(x < 0 || x >= numX || y < 0 || y >= numY || wordsearch[y][x] != XMAS[i])
        {
            return false;
        }
    }
    return true;
}