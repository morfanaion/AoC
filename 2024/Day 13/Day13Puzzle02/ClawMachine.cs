namespace Day13Puzzle02
{
	internal class ClawMachine
	{
		public long AChangeX { get; set; }
		public long AChangeY { get; set; }
		public long BChangeX { get; set; }
		public long BChangeY { get; set; }
		public long PrizeX { get; set; }
		public long PrizeY { get; set; }

		public long CoinCost { get; set; } = int.MaxValue;
		public bool Solve()
		{
            long numB = (PrizeY * AChangeX - PrizeX * AChangeY) / (BChangeY * AChangeX - BChangeX * AChangeY);
            long numA = (PrizeX - (numB * BChangeX)) / AChangeX;
            if ((numA * AChangeX + numB * BChangeX) == PrizeX && (numA * AChangeY + numB * BChangeY) == PrizeY)
            {
                CoinCost = numA * 3 + numB;
                return true;
            }
            return false;
        }
    }
}
