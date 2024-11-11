namespace AoC.Pathfinding
{
	public interface INode
	{
		bool Visited { get; }
		void SetVisited(bool visited);
		IEnumerable<(INode node, int traversalCost)> ConnectedNodes { get; }
		int TraversalCost { get; set; }
		INode? PreviousNode { get; set; }

		IEnumerable<INode> Path
		{
			get
			{
				if(PreviousNode != null)
				{
					return PreviousNode.Path.Append(this);
				}
				return Enumerable.Empty<INode>().Append(this);
			}
		}
	}
}
