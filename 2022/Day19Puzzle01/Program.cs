// See https://aka.ms/new-console-template for more information

using Day19Puzzle01;

List<Blueprint> blueprints = File.ReadAllLines("Input.txt").Select(line => new Blueprint(line)).ToList();
Console.WriteLine(blueprints.Sum(bp => bp.Value));

