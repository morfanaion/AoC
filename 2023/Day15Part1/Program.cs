Console.WriteLine(File.ReadAllText("input.txt").Split(',').Sum(CalculateValue));

int CalculateValue(string arg)
{
    byte result = 0;
    foreach(char c in arg)
    {
        result += (byte)c;
        result *= 17;
    }
    return result;
}