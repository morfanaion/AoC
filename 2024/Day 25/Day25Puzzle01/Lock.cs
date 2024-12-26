namespace Day25Puzzle01
{
	internal class Lock
	{
		public static List<Lock> Locks { get; } = new List<Lock>();
		public static void CreateLock(string[] definition)
		{
			Lock @lock = new Lock();
			for(int y = 1; y < 6; y++)
			{
				for(int x = 0; x < definition[y].Length; x++)
				{
					if (definition[y][x] == '#')
					{
						@lock.PinSizes[x]++;
					}
				}
			}
			Locks.Add(@lock);
		}

		public int[] PinSizes = [0, 0, 0, 0, 0];
	}
}
