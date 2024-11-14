// See https://aka.ms/new-console-template for more information
Console.WriteLine(File.ReadAllLines("Input.txt").Select(line => line.Split(',').Select(elf => elf.Split('-').Select(num => int.Parse(num)).ToArray()).ToArray()).Count(pair => OneContainsOther(pair)));

bool OneContainsOther(int[][] pair)
{
    int maxLowerBound = pair.Select(elf => elf[0]).Max();
    int minHigherBound = pair.Select(elf => elf[1]).Min();
    return pair.Any(elf => elf[0] == maxLowerBound && elf[1] == minHigherBound);
}