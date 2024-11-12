using AoC.Geometry;

namespace AoC.Pathfinding
{
	public static class Dijkstra
	{
		public static IEnumerable<TNode> FindShortestPath<TNode, TVector>(TNode start, TNode end)
			where TNode : INode<TVector>
			where TVector : IVector
		{
			PriorityQueue<INode<TVector>, double> queue = new PriorityQueue<INode<TVector>, double>();
			queue.Enqueue(start, 0);
			INode<TVector>? currentNode;
			while (queue.TryDequeue(out currentNode, out double traveledCost) && !Equals(currentNode, end))
			{
				if (currentNode.Visited)
				{
					continue;
				}
				currentNode.SetVisited(true);
				foreach ((INode<TVector> nextNode, double cost) in currentNode.ConnectedNodes)
				{
					if (!nextNode.Visited && (nextNode.TraversalCost == 0 || nextNode.TraversalCost > traveledCost + cost))
					{
						double newPriority = nextNode.TraversalCost = traveledCost + cost;
						nextNode.PreviousNode = currentNode;
						queue.Enqueue(nextNode, newPriority);
					}
				}
			}
			if(Equals(currentNode, end))
			{
				return currentNode.Path.Cast<TNode>();
			}
			return Enumerable.Empty<TNode>();
		}
	}
}
