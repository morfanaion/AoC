namespace Day16Puzzle01
{
	internal class Node
	{
		public const long TurnCost = 1000;
		public int X { get; set; }
		public int Y { get; set; }

		public bool IsStart { get; set; }
		public bool IsEnd { get; set; }

		public Path? North { get; set; }
		public Path? East { get; set; }
		public Path? South { get; set; }
		public Path? West { get; set; }

		private Dictionary<Direction, bool> _visits =
			new Dictionary<Direction, bool>()
			{
				[Direction.North] = false,
				[Direction.East] = false,
				[Direction.South] = false,
				[Direction.West] = false,
			};
		public bool VisitedFrom(Direction direction) => _visits[direction];

		public bool SetVisitedFrom(Direction direction) => _visits[direction] = true;

		public IEnumerable<Path> AllPaths
		{
			get
			{
				if (North != null)
				{
					yield return North;
				}
				if (East != null)
				{
					yield return East;
				}
				if (South != null)
				{
					yield return South;
				}
				if (West != null)
				{
					yield return West;
				}

			}
		}

		public void EliminatePath(Path path)
		{
			switch (path.EndDirection)
			{
				case Direction.North:
					path.End.South = null;
					break;
				case Direction.South:
					path.End.North = null;
					break;
				case Direction.East:
					path.End.West = null;
					break;
				case Direction.West:
					path.End.East = null;
					break;
			}
		}

		internal bool Eliminate()
		{
			if (IsStart || IsEnd)
			{
				return false;
			}
			Path path = new Path();
			switch(AllPaths.Count())
			{
				case 1:
					if (North != null)
					{
						EliminatePath(North);
					}
					if (South != null)
					{
						EliminatePath(South);
					}
					if (West != null)
					{
						EliminatePath(West);
					}
					if (East != null)
					{
						EliminatePath(East);
					}
					break;
				case 2:
				// we have 2 paths, we're basically a corner, merge our paths and continue
				if(North != null)
				{
					if (East != null)
					{
						MergePaths(North, East, TurnCost);
					}
					else if(West != null)
					{
						MergePaths(North, West, TurnCost);
					}
					else if(South != null)
					{
						MergePaths(North, South, 0);
					}
					else
					{
						throw new InvalidOperationException("We'd only have 1 path, can't be");
					}
				}
				else if (East != null)
				{
					if (South != null)
					{
						MergePaths(East, South, TurnCost);
					}
					else if (West != null)
					{
						MergePaths(East, West, 0);
					}
					else
					{
						throw new InvalidOperationException("We'd only have 1 path or we have a case that should have been handled already");
					}
				}
				else if(South != null)
				{
					if (West != null)
					{
						MergePaths(South, West, TurnCost);
					}
					else
					{
						throw new InvalidOperationException("We'd only have 1 path or we have a case that should have been handled already");
					}
				}
				return true;
			}
			return false;
		}

		private void MergePaths(Path path1, Path path2, long turncost)
		{
			switch(path1.EndDirection)
			{
				case Direction.North:
					MergePath(path1.End.South ?? throw new InvalidOperationException("Cannot be null here"), path2, turncost);
					break;
				case Direction.East:
					MergePath(path1.End.West ?? throw new InvalidOperationException("Cannot be null here"), path2, turncost);
					break;
				case Direction.South:
					MergePath(path1.End.North ?? throw new InvalidOperationException("Cannot be null here"), path2, turncost);
					break;
				case Direction.West:
					MergePath(path1.End.East ?? throw new InvalidOperationException("Cannot be null here"), path2, turncost);
					break;
			}
			switch (path2.EndDirection)
			{
				case Direction.North:
					MergePath(path2.End.South ?? throw new InvalidOperationException("Cannot be null here"), path1, turncost);
					break;
				case Direction.East:
					MergePath(path2.End.West ?? throw new InvalidOperationException("Cannot be null here"), path1, turncost);
					break;
				case Direction.South:
					MergePath(path2.End.North ?? throw new InvalidOperationException("Cannot be null here"), path1, turncost);
					break;
				case Direction.West:
					MergePath(path2.End.East ?? throw new InvalidOperationException("Cannot be null here"), path1, turncost);
					break;
			}
		}

		private void MergePath(Path path1, Path path2, long turncost)
		{
			path1.EndDirection = path2.EndDirection;
			path1.End = path2.End;
			path1.Cost += path2.Cost + turncost;
		}

		public IEnumerable<(Path path, long cost)> ConnectedPathForDirection(Direction direction)
		{
			switch (direction)
			{
				case Direction.North:
					if (North != null)
					{
						yield return (North, North.Cost);
					}
					if (East != null)
					{
						yield return (East, East.Cost + TurnCost);
					}
					if (West != null)
					{
						yield return (West, West.Cost + TurnCost);
					}
					break;
				case Direction.East:
					if (North != null)
					{
						yield return (North, North.Cost + TurnCost);
					}
					if (East != null)
					{
						yield return (East, East.Cost);
					}
					if (South != null)
					{
						yield return (South, South.Cost + TurnCost);
					}
					break;
				case Direction.South:
					if (East != null)
					{
						yield return (East, East.Cost + TurnCost);
					}
					if (South != null)
					{
						yield return (South, South.Cost);
					}
					if (West != null)
					{
						yield return (West, West.Cost + TurnCost);
					}
					break;
				case Direction.West:
					if (North != null)
					{
						yield return (North, North.Cost + TurnCost);
					}
					if (South != null)
					{
						yield return (South, South.Cost + TurnCost);
					}
					if (West != null)
					{
						yield return (West, West.Cost);
					}
					break;
			}
		}
	}
}
