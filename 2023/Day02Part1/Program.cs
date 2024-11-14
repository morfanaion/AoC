// See https://aka.ms/new-console-template for more information
using Day02Part1;

Console.WriteLine(File.ReadAllLines("input.txt").Select(Game.FromString).Where(g => g.IsGameValid(12, 13, 14)).Sum(g => g.Id));
