// See https://aka.ms/new-console-template for more information
using Day02Part2;

Console.WriteLine(File.ReadAllLines("input.txt").Select(Game.FromString).Sum(g => g.GetMaximizedDraw().Power));