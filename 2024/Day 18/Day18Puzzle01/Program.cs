using AoC.Geometry;
using AoC.Pathfinding;
using Day18Puzzle01;

Node?[][] _memory = new Node?[Node.MemorySize][];

for (int y = 0; y < Node.MemorySize; y++)
{
	_memory[y] = new Node[Node.MemorySize];
	for (int x = 0; x < Node.MemorySize; x++)
	{
		_memory[y][x] = new Node() { Position = new Vector2D() { X = x, Y = y} };
		if(x > 0)
		{
			_memory[y][x].West = _memory[y][x - 1];
			_memory[y][x - 1].East = _memory[y][x];
		}
		if(y > 0)
		{
			_memory[y][x].North = _memory[y - 1][x];
			_memory[y - 1][x].South= _memory[y][x];
		}
	}
}

int i = 0;
foreach(string line in File.ReadAllLines("input.txt"))
{
	if(i == 1024)
	{
		break;
	}
	string[] parts = line.Split(',');
	int x = int.Parse(parts[0]);
	int y = int.Parse(parts[1]);
	if (_memory[y][x] is Node node)
	{
		node.RemoveConnections();
		_memory[y][x] = null;
	}
	i++;

}

IEnumerable<Node> path = Dijkstra.FindShortestPath<Node, Vector2D>(_memory[0][0], _memory[70][70]);
PrintMemory(path);
Console.WriteLine(path.Count() - 1);


void PrintMemory(IEnumerable<Node> route)
{
	char[][] output = new char[Node.MemorySize][];
	for(int y =0; y< Node.MemorySize; y++)
	{
		output[y] = new char[Node.MemorySize];
		for (int x = 0; x < Node.MemorySize; x++)
		{
			if (_memory[y][x] == null)
			{
				output[y][x] = '#';
			}
			else
			{
				output[y][x] = '.';
			}
		}
	}
	foreach (Node node in route)
	{
		output[(int)node.Position.Y][(int)node.Position.X] = '0';
	}
	foreach (char[] line in output)
	{
		Console.WriteLine(string.Concat(line));
	}
}