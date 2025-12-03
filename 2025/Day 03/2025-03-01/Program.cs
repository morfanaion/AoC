uint result = 0;
foreach(string line in File.ReadAllLines("input.txt"))
{
    char highest = line.Max();
    int idx = line.IndexOf(highest);
    if (idx == line.Length - 1 )
    {
        result += (((uint)(line.Substring(0, line.Length - 1).Max() - '0')) * 10) + (uint)(highest - '0');
    }
    else
    {
        result += (((uint)(highest - '0')) * 10) + (uint)(line.Substring(idx + 1).Max() - '0');
    }
    Console.WriteLine(result);
}
