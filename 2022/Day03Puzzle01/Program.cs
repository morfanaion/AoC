// See https://aka.ms/new-console-template for more information
Console.WriteLine(File.ReadAllLines("Input.txt")
    .Select(backpack => backpack.Substring(0, backpack.Length / 2)
                            .Where(item => backpack.Substring(backpack.Length / 2, backpack.Length / 2).Contains(item)).Distinct().Sum(item => PriorityScore(item))).Sum());

int PriorityScore(char item)
{
    switch(item)
    {
        case char c when (c >= 'a' && c <= 'z'):
            return ((int)c) - 96;
        case char c when (c >= 'A' && c <= 'Z'):
            return ((int)c) - 38;
    }
    return 0;
}