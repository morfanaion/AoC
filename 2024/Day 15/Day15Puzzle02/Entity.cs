namespace Day15Puzzle01
{
	internal abstract class Entity
	{
		public int X { get; set; }
		public int Y { get; set; }

		public abstract bool Fixed { get; }

		public abstract string Symbol { get; }
	}
}
