using AoC.Pathfinding;

namespace TestDijkstra
{
	internal class Node : INode
	{
		private bool _visited = false;
		public bool Visited => _visited;

		public List<(INode, int traversalCost)> ConnectedNodes { get; set; } = new List<(INode, int traversalCost)>();
		IEnumerable<(INode node, int traversalCost)> INode.ConnectedNodes => ConnectedNodes;

		public int TraversalCost { get; set; }
		public INode? PreviousNode { get; set; }

		public void SetVisited(bool visited)
		{
			_visited = visited;
		}
	}
}
