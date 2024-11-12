using AoC.Geometry;
using AoC.Pathfinding;
using TestDijkstra;

Vector2D v1 = new Vector2D() {  X = 12, Y = 9 };
Vector2D v2 = new Vector2D() { X = 9, Y = 13 };
Vector2D v3 = v1 - v2;

List<Node> testNodes = File.ReadAllLines("Graph.txt").Select(Node.FromString).ToList();
foreach(Node node in testNodes)
{
	node.Initialize();
}
var test = Dijkstra.FindShortestPath<Node, Vector2D>(Node.Nodes[8], Node.Nodes[2]);
Console.WriteLine(string.Join(" => ", test.Select(n => n.Id.ToString())));



