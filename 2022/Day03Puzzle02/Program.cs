// See https://aka.ms/new-console-template for more information

using Day03Puzzle02;

Console.WriteLine(File.ReadAllLines("Input.txt").CreateChunks(3).Select(chunk => (chunk.First().Intersect(chunk.Skip(1).First()).Intersect(chunk.Last())).Sum(badge => PriorityScore(badge))).Sum());

int PriorityScore(char item)
{
    switch (item)
    {
        case char c when (c >= 'a' && c <= 'z'):
            return ((int)c) - 96;
        case char c when (c >= 'A' && c <= 'Z'):
            return ((int)c) - 38;
    }
    return 0;
}