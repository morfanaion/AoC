namespace Day13Puzzle01
{
	internal class ClawMachine
	{
		public int AChangeX { get; set; }
		public int AChangeY { get; set; }
		public int BChangeX { get; set; }
		public int BChangeY { get; set; }
		public int PrizeX { get; set; }
		public int PrizeY { get; set; }

		public int CoinCost { get; set; } = int.MaxValue;
		public bool Solve()
		{
			bool solvable = false;
			int maxNumPressesA = Math.Min(PrizeX / AChangeX, PrizeY / AChangeY);
			int maxNumPressesB = Math.Min(PrizeX / BChangeX, PrizeY / BChangeY);
			if (maxNumPressesA < maxNumPressesB)
			{
				for(int i =0; i < maxNumPressesA; i++)
				{
					int numPressesB = (PrizeX - AChangeX * i) / BChangeX;
					if(i * AChangeX + numPressesB * BChangeX == PrizeX &&
						i * AChangeY + numPressesB * BChangeY == PrizeY)
					{
						solvable = true;
						CoinCost = Math.Min(CoinCost, i * 3 + numPressesB);
					}
				}
			}
			else
			{
				for (int i = 0; i < maxNumPressesA; i++)
				{
					int numPressesA = (PrizeX - BChangeX * i) / AChangeX;
					if (i * BChangeX + numPressesA * AChangeX == PrizeX &&
						i * BChangeY + numPressesA * AChangeY == PrizeY)
					{
						solvable = true;
						CoinCost = Math.Min(CoinCost, numPressesA * 3 + i);
					}
				}
			}
			return solvable;
		}
	}
}
