namespace Day15Puzzle01
{
	internal class Box : Entity
	{
		public static List<Box> AllBoxes { get; } = new List<Box>();

		public Box()
		{
			AllBoxes.Add(this);
		}

		private bool _fixed = false;
		public override bool Fixed => _fixed;

		public override string Symbol => "[]";

		public void SetFixed() => _fixed = true;

		public int GPSCoordinate => Y * 100 + X;
	}
}
