using Day16Puzzle02;
using Path = Day16Puzzle02.Path;

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

long numTiles = 1; // start with 1m because S counts
IEnumerable<List<Path>> routes = GetBestDijkstraPaths(startNode, endNode);
foreach (Path path in routes.SelectMany(r => r).Distinct())
{
	if (!path.HasBeenCounted)
	{
		path.HasBeenCounted = true;
		numTiles += path.NumPathSegments;
		if (!path.End.HasBeenCounted)
		{
			{
				path.End.HasBeenCounted = true;
				numTiles++;
			}
		}
	}
}
Console.WriteLine(numTiles);

IEnumerable<List<Path>> GetBestDijkstraPaths(Node startNode, Node endNode)
{
	PriorityQueue<(Node currentNode, Direction directionEntered, IEnumerable<Path> pathTillNow), long> priorityQueue = new PriorityQueue<(Node currentNode, Direction directionEntered, IEnumerable<Path> pathTillNow), long>();
	priorityQueue.Enqueue((startNode, Direction.East, Enumerable.Empty<Path>()), 0);
	(Node node, Direction direction, IEnumerable<Path> pathTillNow) current;
	List<List<Path>> paths = new List<List<Path>>();
	long lowestCostTillEnd = long.MaxValue;
	while(priorityQueue.TryDequeue(out current, out long currentCost))
	{
		if(currentCost > lowestCostTillEnd)
		{
			break;
		}
		if(current.node.IsEnd)
		{
			lowestCostTillEnd = currentCost;
			paths.Add(current.pathTillNow.ToList());
		}
		if(current.node.VisitedFrom(current.direction, currentCost))
		{
			continue;
		}
		current.node.SetVisitedFrom(current.direction, currentCost);
		foreach ((Path path, long cost) in current.node.ConnectedPathForDirection(current.direction))
		{
			priorityQueue.Enqueue((path.End, path.EndDirection, current.pathTillNow.Append(path)), currentCost + cost);
		}
	}
	return paths;
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
			path.NumPathSegments--;
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
		path.NumPathSegments++;
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