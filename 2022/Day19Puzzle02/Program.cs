// See https://aka.ms/new-console-template for more information

using Day19Puzzle02;

List<Blueprint> blueprints = File.ReadAllLines("Input.txt").Select(line => new Blueprint(line)).ToList();

int product = 1;
foreach(Blueprint blueprint in blueprints.Take(3))
{
    product *= blueprint.Value;
}
Console.WriteLine(product);