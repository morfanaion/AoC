// See https://aka.ms/new-console-template for more information
char[][] textGrid = File.ReadAllLines("input.txt").Select(x => x.ToArray()).ToArray();
Func<int, int, int, bool> containsSymbol = (startX, endX, y) =>
{
    for (int x = startX; x <= endX; x++)
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
    for (int x = 0; x < textGrid[y].Length; x++)
    {
        if (textGrid[y][x] == '*')
        {
            List<int> numbers = new List<int>();
            bool skipSecond = false;
            bool skipThird = false;
            if (y > 0)
            {
                if (x > 0 && char.IsDigit(textGrid[y - 1][x - 1]))
                {
                    int start = x - 1;
                    while (start > 0)
                    {
                        if (!char.IsDigit(textGrid[y - 1][start - 1]))
                        {
                            break;
                        }
                        start -= 1;
                    }
                    int length = 1;
                    string number = textGrid[y - 1][start].ToString();
                    while (start + length <= textGrid[y - 1].Length - 1)
                    {
                        if (!char.IsDigit(textGrid[y - 1][start + length]))
                        {
                            break;
                        }
                        number += textGrid[y - 1][start + length];
                        length++;
                    }
                    numbers.Add(int.Parse(number));
                    skipSecond = start + length >= x;
                    skipThird = start + length >= x + 1;
                }
                if (!skipSecond && char.IsDigit(textGrid[y - 1][x]))
                {
                    int start = x;
                    while (start > 0)
                    {
                        if (!char.IsDigit(textGrid[y - 1][start - 1]))
                        {
                            break;
                        }
                        start -= 1;
                    }
                    int length = 1;
                    string number = textGrid[y - 1][start].ToString();
                    while (start + length <= textGrid[y - 1].Length - 1)
                    {
                        if (!char.IsDigit(textGrid[y - 1][start + length]))
                        {
                            break;
                        }
                        number += textGrid[y - 1][start + length];
                        length++;
                    }
                    numbers.Add(int.Parse(number));
                    skipThird = start + length >= x + 1;
                }
                if (!skipThird && x < textGrid[y - 1].Length - 1 && char.IsDigit(textGrid[y - 1][x + 1]))
                {
                    int start = x + 1;
                    int length = 1;
                    string number = textGrid[y - 1][start].ToString();
                    while (start + length <= textGrid[y - 1].Length - 1)
                    {
                        if (!char.IsDigit(textGrid[y - 1][start + length]))
                        {
                            break;
                        }
                        number += textGrid[y - 1][start + length];
                        length++;
                    }
                    numbers.Add(int.Parse(number));
                }
            }
            if (x > 0 && char.IsDigit(textGrid[y][x - 1]))
            {
                string leftNumber = string.Empty;
                int currentPos = x;
                while(currentPos > 0 && char.IsDigit(textGrid[y][currentPos - 1]))
                {
                    currentPos--;
                    leftNumber = textGrid[y][currentPos] + leftNumber;
                }
                numbers.Add(int.Parse(leftNumber));
            }
            if (x < textGrid[y].Length - 1 && char.IsDigit(textGrid[y][x + 1]))
            {
                string rightNumber = string.Empty;
                int currentPos = x;
                while (currentPos < textGrid[y].Length - 1 && char.IsDigit(textGrid[y][currentPos + 1]))
                {
                    currentPos++;
                    rightNumber = rightNumber + textGrid[y][currentPos];
                }
                numbers.Add(int.Parse(rightNumber));
            }

            skipSecond = false;
            skipThird = false;
            if (y < textGrid.Length - 1)
            {
                if (x > 0 && char.IsDigit(textGrid[y + 1][x - 1]))
                {
                    int start = x - 1;
                    while (start > 0)
                    {
                        if (!char.IsDigit(textGrid[y + 1][start - 1]))
                        {
                            break;
                        }
                        start -= 1;
                    }
                    int length = 1;
                    string number = textGrid[y + 1][start].ToString();
                    while (start + length <= textGrid[y + 1].Length - 1)
                    {
                        if (!char.IsDigit(textGrid[y + 1][start + length]))
                        {
                            break;
                        }
                        number += textGrid[y + 1][start + length];
                        length++;
                    }
                    numbers.Add(int.Parse(number));
                    skipSecond = start + length >= x;
                    skipThird = start + length >= x + 1;
                }
                if (!skipSecond && char.IsDigit(textGrid[y + 1][x]))
                {
                    int start = x;
                    while (start > 0)
                    {
                        if (!char.IsDigit(textGrid[y + 1][start - 1]))
                        {
                            break;
                        }
                        start -= 1;
                    }
                    int length = 1;
                    string number = textGrid[y + 1][start].ToString();
                    while (start + length <= textGrid[y + 1].Length - 1)
                    {
                        if (!char.IsDigit(textGrid[y + 1][start + length]))
                        {
                            break;
                        }
                        number += textGrid[y + 1][start + length];
                        length++;
                    }
                    numbers.Add(int.Parse(number));
                    skipThird = start + length >= x + 1;
                }
                if (!skipThird && x < textGrid[y + 1].Length - 1 && char.IsDigit(textGrid[y + 1][x + 1]))
                {
                    int start = x + 1;
                    while (start > 0)
                    {
                        if (!char.IsDigit(textGrid[y + 1][start - 1]))
                        {
                            break;
                        }
                        start -= 1;
                    }
                    int length = 1;
                    string number = textGrid[y + 1][start].ToString();
                    while (start + length <= textGrid[y + 1].Length - 1)
                    {
                        if (!char.IsDigit(textGrid[y + 1][start + length]))
                        {
                            break;
                        }
                        number += textGrid[y + 1][start + length];
                        length++;
                    }
                    numbers.Add(int.Parse(number));
                }
            }

            if(numbers.Count == 2)
            {
                result += numbers[0] * numbers[1];
            }
        }
    }
}
Console.WriteLine(result);

