// See https://aka.ms/new-console-template for more information
using Day04Part1;

Console.WriteLine(File.ReadAllLines("input.txt").Select(ScratchCard.FromString).Sum(sc => sc.Score));


