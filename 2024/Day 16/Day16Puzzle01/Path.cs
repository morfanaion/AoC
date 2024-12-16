namespace Day16Puzzle01
{
	internal class Path
	{
		public Node Start { get; set; } = new Node();
		public Node End { get; set; } = new Node();
		public Direction StartDirection { get; set; }
		public Direction EndDirection { get; set; }

		public long Cost { get; set; }
	}
}
