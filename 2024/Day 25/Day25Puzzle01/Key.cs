namespace Day25Puzzle01
{
	internal class Key
	{
		public static List<Key> Keys { get; } = new List<Key>();
		public static void CreateKey(string[] definition)
		{
			Key key = new Key();
			for (int y = 5; y >0; y--)
			{
				for (int x = 0; x < definition[y].Length; x++)
				{
					if (definition[y][x] == '#')
					{
						key.PinSizes[x]++;
					}
				}
			}
			Keys.Add(key);
		}

		public int[] PinSizes = [0, 0, 0, 0, 0];
	}
}
