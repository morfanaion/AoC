// See https://aka.ms/new-console-template for more information
using Day04Part2;

ScratchCard.Cards = File.ReadAllLines("input.txt").Select(ScratchCard.FromString).ToList();
Console.WriteLine(ScratchCard.Cards.Sum(c => c.Score));