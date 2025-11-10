namespace Day14Puzzle01
{
	internal class Robot
	{
		public const int BathroomWidth = 101;
		public const int BathroomHeight = 103;

		public long X {  get; set; }
		public long Y { get; set; }

		public long DX { get; set; }
		public long DY { get; set; }

		internal static Robot FromString(string str)
		{
			Match match = RegexHelper.RobotRegex().Match(str);
			if(!match.Success)
			{
				throw new ArgumentException("input corrupt");
			}
			return new Robot()
			{
				X = long.Parse(match.Groups["x"].Value),
				Y = long.Parse(match.Groups["y"].Value),
				DX = long.Parse(match.Groups["dx"].Value),
				DY = long.Parse(match.Groups["dy"].Value),
			};
		}

		public void Progress(long numSeconds)
		{
			X += DX * numSeconds;
			if (X < 0)
			{
				long divider = X / BathroomWidth - 1;
				X -= divider * BathroomWidth;
			}
			X %= BathroomWidth;
			Y += DY * numSeconds;
			if (Y < 0)
			{
				long divider = Y / BathroomHeight - 1;
				Y -= divider * BathroomHeight;
			}
			Y %= BathroomHeight;
		}
	}
}
