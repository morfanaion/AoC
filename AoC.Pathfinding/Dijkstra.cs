namespace AoC.Pathfinding
{
	public static class Dijkstra
	{
		public static IEnumerable<INode> FindShortestPath(INode start, INode end)
		{
			PriorityQueue<INode, int> queue = new PriorityQueue<INode, int>();
			queue.Enqueue(start, 0);
			INode? currentNode;
			while (queue.TryDequeue(out currentNode, out int traveledCost) && !Equals(currentNode, end))
			{
				if (currentNode.Visited)
				{
					continue;
				}
				currentNode.SetVisited(true);
				foreach ((INode nextNode, int cost) in currentNode.ConnectedNodes)
				{
					if (!nextNode.Visited && (nextNode.TraversalCost == 0 || nextNode.TraversalCost < traveledCost + cost))
					{
						int newPriority = nextNode.TraversalCost = traveledCost + cost;
						queue.Enqueue(nextNode, newPriority);
					}
				}
				currentNode = queue.Dequeue();
			}
			if(Equals(currentNode, end))
			{
				return currentNode.Path;
			}
			return Enumerable.Empty<INode>();
		}
	}
}
