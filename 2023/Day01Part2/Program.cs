// See https://aka.ms/new-console-template for more information
Console.WriteLine(File.ReadLines("input.txt").Select(StringToNumber).Sum());


int StringToNumber(string str)
{
    int result = 0;
    string scannedText = string.Empty;
    foreach (char c in str)
    {
        if (char.IsDigit(c))
        {
            result = (c - '0') * 10;
            break;
        }
        scannedText = c + scannedText;
        if(IsTextualNumber(scannedText, out int n))
        {
            result = n * 10;
            break;
        }
    }
    scannedText = string.Empty;
    foreach (char c in str.Reverse())
    {
        if (char.IsDigit(c))
        {
            result += c - '0';
            break;
        }
        scannedText = c + scannedText;
        if (IsTextualNumber(scannedText, out int n))
        {
            result += n;
            break;
        }
    }
    return result;
}

bool IsTextualNumber(string str, out int number)
{
    switch(str.Substring(0, Math.Min(str.Length, 3)))
    {
        case "one" or "eno":
            number = 1;
            return true;
        case "two" or "owt":
            number = 2;
            return true;
        case "six" or "xis":
            number = 6;
            return true;
        default:
            switch (str.Substring(0, Math.Min(str.Length, 4)))
            {
                case "zero" or "eroz":
                    number = 0;
                    return true;
                case "four" or "ruof":
                    number = 4;
                    return true;
                case "five" or "evif":
                    number = 5;
                    return true;
                case "nine" or "enin":
                    number = 9;
                    return true;
                default:
                    switch (str.Substring(0, Math.Min(str.Length, 5)))
                    {
                        case "three" or "eerht":
                            number = 3;
                            return true;
                        case "seven" or "neves":
                            number = 7;
                            return true;
                        case "eight" or "thgie":
                            number = 8;
                            return true;
                        default:
                            number = 0;
                            return false;
                    }
            }
    }
}