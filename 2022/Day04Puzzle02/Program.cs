// See https://aka.ms/new-console-template for more information
Console.WriteLine(File.ReadAllLines("Input.txt").Select(line => line.Split(',').Select(elf => elf.Split('-').Select(num => int.Parse(num)).ToArray()).ToArray()).Count(pair => HasOverlap(pair)));

bool HasOverlap(int[][] pair)
{
    int maxLowerBound = pair.Select(elf => elf[0]).Max();
    int minHigherBound = pair.Select(elf => elf[1]).Min();
    return minHigherBound >= maxLowerBound;
}