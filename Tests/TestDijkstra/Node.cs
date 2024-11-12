using AoC.Geometry;
using AoC.Pathfinding;
using System.Text.RegularExpressions;

namespace TestDijkstra
{
	internal class Node : INode<Vector2D>
	{
		public static Dictionary<int, Node> Nodes { get; } = new Dictionary<int, Node>();
		public static Node FromString(string text)
		{
			Match match = RegexHelper.NodeRegex().Match(text);
			if (!match.Success)
			{
				throw new ArgumentException("Invalid string");
			}
			Node node = new Node();
			node.Position = new Vector2D() { X = int.Parse(match.Groups["x"].Value), Y = int.Parse(match.Groups["y"].Value) };
			node.Id = int.Parse(match.Groups["id"].Value);
			node._connectedNodeIds = match.Groups["connected"].Value.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(s => int.Parse(s)).ToList();
			Nodes.Add(node.Id, node);
			return node;
		}

		public void Initialize()
		{
			foreach (int connectedNodeId in _connectedNodeIds)
			{
				Node otherNode = Nodes[connectedNodeId];
				ConnectedNodes.Add((otherNode, (Position - otherNode.Position).Length));
			}
		}

		private List<int> _connectedNodeIds = new List<int>();

		public int Id { get; private set; }

		private bool _visited = false;
		public bool Visited => _visited;

		public List<(Node node, double traversalCost)> ConnectedNodes { get; set; } = new List<(Node, double traversalCost)>();
		IEnumerable<(INode<Vector2D> node, double traversalCost)> INode<Vector2D>.ConnectedNodes => ConnectedNodes.Select<(Node node, double traversalCost), (INode<Vector2D>, double)>(cn => (cn.node, cn.traversalCost));

		public double TraversalCost { get; set; }
		public INode<Vector2D>? PreviousNode { get; set; }
		public Vector2D Position { get; set; }

		public void SetVisited(bool visited)
		{
			_visited = visited;
		}

		public override string ToString()
		{
			return $"Id = {Id} X={Position.X} Y={Position.Y}";
		}
	}
}
