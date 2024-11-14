// See https://aka.ms/new-console-template for more information
using Day18Puzzle01;

List<CubeEntry> cubeEntries = File.ReadAllLines("Input.txt").Select(line => new CubeEntry(line)).ToList();

Console.WriteLine(cubeEntries.Sum(entry => entry.ExposedSides(cubeEntries)));