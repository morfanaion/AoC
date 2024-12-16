namespace Day16Puzzle02
{
	internal class Path
	{
		public Node Start { get; set; } = new Node();
		public Node End { get; set; } = new Node();
		public Direction StartDirection { get; set; }
		public Direction EndDirection { get; set; }

		public long Cost { get; set; }

		public long NumPathSegments { get; set; }
		public bool HasBeenCounted { get; set; }
	}
}
