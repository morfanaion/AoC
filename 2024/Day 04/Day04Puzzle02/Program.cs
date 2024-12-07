
char[][] wordsearch = File.ReadAllLines("input.txt").Select(l => l.ToArray()).ToArray();

int numX = wordsearch[0].Length;
int numY = wordsearch.Length;
int numXmas = 0;
for(int x = 0; x < numX; x++)
{
    for(int y = 0; y < numY; y++)
    {
        if (wordsearch[y][x] == 'A')
        {
            if (IsXMas(x, y))
            {
                numXmas++;
            }
        }
    }
}
Console.WriteLine(numXmas);

bool IsXMas(int x, int y)
{
    if (x == 0 || y == 0 || x + 1 == numX || y + 1 == numY)
    {
        return false;
    }
    return ((wordsearch[y - 1][x - 1] == 'M' && wordsearch[y + 1][x + 1] == 'S') || (wordsearch[y - 1][x - 1] == 'S' && wordsearch[y + 1][x + 1] == 'M')) &&
        ((wordsearch[y - 1][x + 1] == 'M' && wordsearch[y + 1][x - 1] == 'S') || (wordsearch[y - 1][x + 1] == 'S' && wordsearch[y + 1][x - 1] == 'M'));
}