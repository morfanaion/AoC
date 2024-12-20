using AoC.Geometry;
using AoC.Pathfinding;
using Day20Puzzle01;

char[][] theMaze = File.ReadAllLines("input.txt").Select(line => line.ToArray()).ToArray();
Node?[][] nodedMaze = new Node?[theMaze.Length][];
List<(int x, int y, int x2, int y2)> possibleCheats = new List<(int x, int y, int x2, int y2)>();
(int x, int y) startPosition = (0,0);
(int x, int y) endPosition = (0,0);

for (int y = 0; y < theMaze.Length; y++)
{
	for (int x = 0; x < theMaze[y].Length; x++)
	{
		switch (theMaze[y][x])
		{
			case '#':
				if (x > 0 && x + 1 < theMaze[y].Length && y > 0 && y + 1 < theMaze.Length && NeighbourChars(x, y).Count(n => n.c == '#') <= 2)
				{
					foreach ((char c, int x2, int y2) in NeighbourChars(x, y))
					{
						if (c != '#')
						{
							possibleCheats.Add((x, y, x2, y2));
						}
					}
				}
				break;
			case 'S':
				startPosition = (x, y);
				break;
			case 'E':
				endPosition = (x, y);
				break;
		}
	}
}

IEnumerable<(char c, int x, int y)> NeighbourChars(int x, int y)
{
	yield return (theMaze[x - 1][y], x - 1, y);
	yield return (theMaze[x][y - 1], x, y - 1);
	yield return (theMaze[x + 1][y], x + 1, y);
	yield return (theMaze[x][y + 1], x, y + 1);
}

void SetNodedMaze((int x, int y, int x2, int y2) cheat)
{
	nodedMaze = new Node?[theMaze.Length][];
	for (int y = 0; y < theMaze.Length; y++)
	{
		nodedMaze[y] = new Node?[theMaze[y].Length];
		for (int x = 0; x < theMaze[y].Length; x++)
		{
			switch (theMaze[y][x])
			{
				case '#':
					break;
				default:
					nodedMaze[y][x] = new Node() { Position = new Vector2D() { X = x, Y = y } };
					if(x > 0 && nodedMaze[y][x - 1] is Node otherNode)
					{
						nodedMaze[y][x].West = otherNode;
						otherNode.East = nodedMaze[y][x];
					}
					if (y > 0 && nodedMaze[y - 1][x] is Node otherNode2)
					{
						nodedMaze[y][x].North = otherNode2;
						otherNode2.South = nodedMaze[y][x];
					}
					break;
			}
		}
	}
	if (cheat.x != -1 && cheat.y != -1)
	{
		nodedMaze[cheat.y][cheat.x] = new Node() { Position = new Vector2D() { X = cheat.x, Y = cheat.y } };
		Node cheatNode = new Node();
		switch(cheat.x2 - cheat.x)
		{
			case -1:
				nodedMaze[cheat.y][cheat.x].West = cheatNode = nodedMaze[cheat.y2][cheat.x2];
				if (nodedMaze[cheat.y][cheat.x + 1] is Node otherNodeEast)
				{
					otherNodeEast.West = cheatNode;
				}
				if (nodedMaze[cheat.y + 1][cheat.x] is Node otherNodeNorth)
				{
					otherNodeNorth.South = cheatNode;
				}
				if (nodedMaze[cheat.y + 1][cheat.x] is Node otherNodeSouth)
				{
					otherNodeSouth.North = cheatNode;
				}
				break;
			case 1:
				nodedMaze[cheat.y][cheat.x].East = cheatNode = nodedMaze[cheat.y2][cheat.x2];
				if (nodedMaze[cheat.y][cheat.x - 1] is Node otherNodeWest2)
				{
					otherNodeWest2.East = cheatNode;
				}
				if (nodedMaze[cheat.y + 1][cheat.x] is Node otherNodeNorth2)
				{
					otherNodeNorth2.South = cheatNode;
				}
				if (nodedMaze[cheat.y + 1][cheat.x] is Node otherNodeSouth2)
				{
					otherNodeSouth2.North = cheatNode;
				}
				break;
			case 0:
				switch(cheat.y2 - cheat.y)
				{
					case -1:
						nodedMaze[cheat.y][cheat.x].North = cheatNode = nodedMaze[cheat.y2][cheat.x2];
						if (nodedMaze[cheat.y][cheat.x - 1] is Node otherNodeWest3)
						{
							otherNodeWest3.East = cheatNode;
						}
						if (nodedMaze[cheat.y][cheat.x + 1] is Node otherNodeEast3)
						{
							otherNodeEast3.West = cheatNode;
						}
						if (nodedMaze[cheat.y + 1][cheat.x] is Node otherNodeSouth3)
						{
							otherNodeSouth3.North = cheatNode;
						}
						break;
					case 1:
						nodedMaze[cheat.y][cheat.x].South = cheatNode = nodedMaze[cheat.y2][cheat.x2];
						if (nodedMaze[cheat.y][cheat.x - 1] is Node otherNodeWest4)
						{
							otherNodeWest4.East = cheatNode;
						}
						if (nodedMaze[cheat.y][cheat.x + 1] is Node otherNodeEast4)
						{
							otherNodeEast4.West = cheatNode;
						}
						if (nodedMaze[cheat.y + 1][cheat.x] is Node otherNodeNorth4)
						{
							otherNodeNorth4.South = cheatNode;
						}
						break;
					default:
						throw new ArgumentException("Diff x,y can only be a combination of -1 and 0 or 1 and 0");
				}
				break;
			default:
				throw new ArgumentException("Diff x,y can only be a combination of -1 and 0 or 1 and 0");
		}
	}
}

int PicosecondsWhenCheating((int x1, int y1, int x2, int y2) cheat)
{
	if (cheat.x1 == 11 && cheat.y1 == 2 && cheat.x2 == 11 && cheat.y2 == 1)
	{
		int breakpoint = 0;
	}
	SetNodedMaze(cheat);
	return Dijkstra.FindShortestPath<Node, Vector2D>(nodedMaze[startPosition.y][startPosition.x], nodedMaze[endPosition.y][endPosition.x]).Count();
}


SetNodedMaze((-1, -1, -1, -1));
int normalPicoSeconds = Dijkstra.FindShortestPath<Node, Vector2D>(nodedMaze[startPosition.y][startPosition.x], nodedMaze[endPosition.y][endPosition.x]).Count();
foreach(IGrouping<int, ((int x, int y, int x2, int y2) cheat, int)>? group in possibleCheats.Select(cheat => (cheat, normalPicoSeconds - PicosecondsWhenCheating(cheat))).GroupBy(pico => pico.Item2).OrderBy(g => g.Key))
{
	Console.WriteLine($"There are {group.Count()} cheats that save {group.Key} picoseconds");
	foreach(((int x, int y, int x2, int y2) cheat, int pico) in group)
	{
		Console.WriteLine($"{cheat.x}, {cheat.y}, {cheat.x2}, {cheat.y2}");
	}
}

