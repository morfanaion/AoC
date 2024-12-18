using AoC.Geometry;
using AoC.Pathfinding;

namespace Day18Puzzle01
{
	internal class Node : INode<Vector2D>
	{
		public const int MemorySize = 71;
		public bool Visited { get; set; }

		public Node? North { get; set; }
		public Node? East { get; set; }
		public Node? South { get; set; }
		public Node? West { get; set; }

		public IEnumerable<(INode<Vector2D> node, double traversalCost)> ConnectedNodes
		{
			get
			{
				if(North != null)
				{
					yield return (North, 1);
				}
				if (East != null)
				{
					yield return (East, 1);
				}
				if (South != null)
				{
					yield return (South, 1);
				}
				if (West != null)
				{
					yield return (West, 1);
				}
			}
		}

		public void RemoveConnections()
		{
			if (North != null)
			{
				North.South = null;
				North = null;
			}
			if (South != null)
			{
				South.North = null;
				South = null;
			}
			if (East != null)
			{
				East.West = null;
				East = null;
			}
			if (West != null)
			{
				West.East = null;
				West = null;
			}
		}

		public double TraversalCost { get; set; }
		public INode<Vector2D>? PreviousNode { get; set; }
		public Vector2D Position { get; set; }

		public void SetVisited(bool visited) => Visited = true;
	}
}
