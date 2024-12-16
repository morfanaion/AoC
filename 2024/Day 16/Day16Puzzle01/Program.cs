using Day16Puzzle01;
using Path = Day16Puzzle01.Path;

string[] theMaze = File.ReadAllLines("input.txt");
List<Node> nodes = new List<Node>();
for(int y = 0;  y < theMaze.Length; y++)
{
	for(int x = 0; x < theMaze[y].Length; x++)
	{
		switch (theMaze[y][x])
		{
			case '.':
				if (Neighbours(x, y).Count(c => c != '#') > 2)
				{
					nodes.Add(new Node() { X = x, Y = y });
				}
				break;
			case 'S':
				nodes.Add(new Node() { Y = y, X = x, IsStart = true });
				break;
			case 'E':
				nodes.Add(new Node() { Y = y, X = x, IsEnd = true });
				break;
		}
	}
}

foreach(Node node in nodes)
{
	SetPaths(node);
}
bool eliminatedSomething = true;
while (eliminatedSomething)
{
	eliminatedSomething = false;
	for (int i = nodes.Count - 1; i >= 0; i--)
	{
		Node node = nodes[i];
		if (nodes[i].Eliminate())
		{
			eliminatedSomething = true;
			nodes.RemoveAt(i);
		}
	}
}

Node startNode = nodes.First(n => n.IsStart);
Node endNode = nodes.First(n => n.IsEnd);
Console.WriteLine(GetBestDijkstraCost(startNode, endNode));

long GetBestDijkstraCost(Node startNode, Node endNode)
{
	PriorityQueue<(Node currentNode, Direction directionEntered), long> priorityQueue = new PriorityQueue<(Node currentNode, Direction directionEntered), long>();
	priorityQueue.Enqueue((startNode, Direction.East), 0);
	(Node node, Direction direction) current;
	while(priorityQueue.TryDequeue(out current, out long currentCost))
	{
		if(current.node.IsEnd)
		{
			return currentCost;
		}
		if(current.node.VisitedFrom(current.direction))
		{
			continue;
		}
		current.node.SetVisitedFrom(current.direction);
		foreach ((Path path, long cost) in current.node.ConnectedPathForDirection(current.direction))
		{
			priorityQueue.Enqueue((path.End, path.EndDirection), currentCost + cost);
		}
	}
	return -1;
}

void SetPaths(Node node)
{
	if (theMaze[node.Y - 1][node.X] != '#')
	{
		CreatePath(node, Direction.North);
	}
	if (theMaze[node.Y + 1][node.X] != '#')
	{
		CreatePath(node, Direction.South);
	}
	if (theMaze[node.Y][node.X - 1] != '#')
	{
		CreatePath(node, Direction.West);
	}
	if (theMaze[node.Y][node.X + 1] != '#')
	{
		CreatePath(node, Direction.East);
	}
}

void CreatePath(Node node, Direction direction)
{
	Path path = new Path() { Start = node, StartDirection = direction };
	int currentX = node.X;
	int currentY = node.Y;
	bool createdPath = true;
	bool keepGoing = true;
	void DoChecks((int x, int y) ahead, (int x, int y) left, (int x, int y) right, Direction directionLeft, Direction directionRight)
	{
		if (nodes.FirstOrDefault(n => n.X == currentX && n.Y == currentY) is Node endNode)
		{
			path.End = endNode;
			path.EndDirection = direction;
			keepGoing = false;
			return;
		}
		if (theMaze[ahead.y][ahead.x] == '#')
		{
			// need to change direction
			if (theMaze[left.y][left.x] != '#')
			{
				direction = directionLeft;
				path.Cost += Node.TurnCost;
			}
			else if (theMaze[right.y][right.x] != '#')
			{
				direction = directionRight;
				path.Cost += Node.TurnCost;
			}
			else
			{
				// we haven't reached a node, we can only turn back, we've hit a dead end, this path is useless, scrap it
				createdPath = false;
				keepGoing = false;
			}
		}
	}
	while (keepGoing)
	{
		path.Cost++;
		switch (direction)
		{
			case Direction.North:
				currentY--;
				DoChecks((currentX, currentY - 1), (currentX - 1, currentY), (currentX + 1, currentY), Direction.West, Direction.East);
				break;
				case Direction.South:
				currentY++;
				DoChecks((currentX, currentY + 1), (currentX + 1, currentY), (currentX - 1, currentY), Direction.East, Direction.West);
				break;
			case Direction.West:
				currentX--;
				DoChecks((currentX - 1, currentY), (currentX, currentY + 1), (currentX, currentY - 1), Direction.South, Direction.North);
				break;
			case Direction.East:
				currentX++;
				DoChecks((currentX + 1, currentY), (currentX, currentY - 1), (currentX, currentY + 1), Direction.North, Direction.South);
				break;
		}
	}
	if (createdPath)
	{
		switch (path.StartDirection)
		{
			case Direction.North:
				node.North = path;
				break;
			case Direction.East:
				node.East = path;
				break;
			case Direction.South:
				node.South = path;
				break;
			case Direction.West:
				node.West = path;
				break;
		}
	}
}

IEnumerable<char> Neighbours(int x, int y)
{
	yield return theMaze[y][x - 1];
	yield return theMaze[y][x + 1];
	yield return theMaze[y - 1][x];
	yield return theMaze[y + 1][x];
}