// See https://aka.ms/new-console-template for more information
using Day07Puzzle02;

Console.WriteLine(File.ReadAllLines("input.txt").Select(Equation.FromString).Where(e => e.PossiblyTrue).Sum(e => e.Test));
