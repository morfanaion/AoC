// See https://aka.ms/new-console-template for more information
using Day12Part1;

List<SpringCollection> groups = File.ReadAllLines("input.txt").Select(l => new SpringCollection(l)).ToList();
Task[] tasks = groups.Select(g => Task.Run(() => g.FindSolves())).ToArray();
Task.WaitAll(tasks);
Console.WriteLine(groups.Sum(g => g.NumSolves));
