using AoC.Geometry;
using AoC.Pathfinding;
using AOC.Maths;
using Day18Puzzle01;

Node?[][] _memory = new Node?[Node.MemorySize][];
(int x, int y)[] allFallingBytes = GetInput();

(int x, int y)[] GetInput()
{
	(int x, int y) GetXAndY(string str)
	{
		string[] parts = str.Split(',');
		return (int.Parse(parts[0]), int.Parse(parts[1]));
	}
	return File.ReadAllLines("input.txt").Select(GetXAndY).ToArray();
}

void ResetMemoryTo(int numFallenBytes)
{
	for (int y = 0; y < Node.MemorySize; y++)
	{
		_memory[y] = new Node[Node.MemorySize];
		for (int x = 0; x < Node.MemorySize; x++)
		{
			_memory[y][x] = new Node() { Position = new Vector2D() { X = x, Y = y } };
			if (x > 0)
			{
				_memory[y][x].West = _memory[y][x - 1];
				_memory[y][x - 1].East = _memory[y][x];
			}
			if (y > 0)
			{
				_memory[y][x].North = _memory[y - 1][x];
				_memory[y - 1][x].South = _memory[y][x];
			}
		}
	}

	for(int i = 0; i < numFallenBytes; i++)
	{
		if (_memory[allFallingBytes[i].y][allFallingBytes[i].x] is Node node)
		{
			node.RemoveConnections();
			_memory[allFallingBytes[i].y][allFallingBytes[i].x] = null;
		}
	}
}

int noPathIndex = allFallingBytes.Length;
int pathIndex = 0;

while(true)
{
	int attempt = AoCMath.Average(pathIndex, noPathIndex);
    if (attempt == pathIndex)
    {
		break;
    }
    ResetMemoryTo(attempt);
	IEnumerable<Node> path = Dijkstra.FindShortestPath<Node, Vector2D>(_memory[0][0], _memory[Node.MemorySize - 1][Node.MemorySize - 1]);
	if(path.Any())
	{
		pathIndex = attempt;
	}
	else
	{
		noPathIndex = attempt;
	}
}


Console.WriteLine($"{allFallingBytes[pathIndex].x},{allFallingBytes[pathIndex].y}");


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