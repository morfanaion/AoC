// See https://aka.ms/new-console-template for more information
char[][] textGrid = File.ReadAllLines("input.txt").Select(x => x.ToArray()).ToArray();
string number = string.Empty;
int numberStart = 0;
int numberEnd = 0;
Func<int, int, int, bool> containsSymbol = (startX, endX, y) =>
{
    for(int x = startX; x <= endX; x++)
    {
        if (!char.IsDigit(textGrid[y][x]) && textGrid[y][x] != '.')
        {
            return true;
        }
    }
    return false;
};

int result = 0;
for (int y = 0; y < textGrid.Length; y++)
{
    for(int x = 0; x < textGrid[y].Length; x++)
    {
        if (char.IsDigit(textGrid[y][x]))
        {
            if (number == string.Empty)
            {
                numberStart = x;
            }
            number += textGrid[y][x];
        }
        else
        {
            if(number != string.Empty)
            {
                numberEnd = x - 1;
                if (containsSymbol(Math.Max(0, numberStart - 1), Math.Min(textGrid[y].Length - 1, numberEnd + 1), Math.Max(0, y - 1)) ||
                containsSymbol(Math.Max(0, numberStart - 1), Math.Min(textGrid[y].Length - 1, numberEnd + 1), y) ||
                containsSymbol(Math.Max(0, numberStart - 1), Math.Min(textGrid[y].Length - 1, numberEnd + 1), Math.Min(textGrid.Length - 1, y + 1)))
                {
                    result += int.Parse(number);
                }
            }
            number = string.Empty;
        }
    }
    if (number != string.Empty)
    {
        numberEnd = textGrid[y].Length - 1;
        if (containsSymbol(Math.Max(0, numberStart - 1), Math.Min(textGrid[y].Length - 1, numberEnd + 1), Math.Max(0, y - 1)) ||
        containsSymbol(Math.Max(0, numberStart - 1), Math.Min(textGrid[y].Length - 1, numberEnd + 1), y) ||
        containsSymbol(Math.Max(0, numberStart - 1), Math.Min(textGrid[y].Length - 1, numberEnd + 1), Math.Min(textGrid.Length - 1, y + 1)))
        {
            result += int.Parse(number);
        }
    }
    number = string.Empty;
}
Console.WriteLine(result);
