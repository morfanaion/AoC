// See https://aka.ms/new-console-template for more information
Console.WriteLine(File.ReadLines("input.txt").Select(StringToNumber).Sum());


int StringToNumber(string str)
{
    int result = 0;
    foreach (char c in str)
    {
        if (char.IsDigit(c))
        {
            result = (c - '0') * 10;
            break;
        }
    }
    foreach (char c in str.Reverse())
    {
        if (char.IsDigit(c))
        {
            result += c - '0';
            break;
        }
    }
    return result;
}