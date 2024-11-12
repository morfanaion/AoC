using AoC.Geometry;

namespace AoC.Pathfinding
{
	public interface INode<TVector> where TVector : IVector
	{
		bool Visited { get; }
		void SetVisited(bool visited);
		IEnumerable<(INode<TVector> node, double traversalCost)> ConnectedNodes { get; }
		double TraversalCost { get; set; }
		INode<TVector>? PreviousNode { get; set; }

		IEnumerable<INode<TVector>> Path
		{
			get
			{
				if(PreviousNode != null)
				{
					return PreviousNode.Path.Append(this);
				}
				return Enumerable.Empty<INode<TVector>>().Append(this);
			}
		}

		TVector Position { get; set; }
	}
}
