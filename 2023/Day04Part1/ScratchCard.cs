namespace Day04Part1
{
    internal class ScratchCard
    {
        public int Id { get; private set; }
        public int[] WinningNumbers { get; private set; } = new int[0];
        public int[] ScratchedNumbers { get; private set; } = new int[0];

        public static ScratchCard FromString(string s)
        {
            ScratchCard card = new ScratchCard();
            string[] parts = s.Split(':');
            card.Id = int.Parse(string.Join("", parts[0].Skip(5)));
            parts = parts[1].Split('|');
            card.WinningNumbers = parts[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(s => int.Parse(s.Trim())).ToArray();
            card.ScratchedNumbers = parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(s => int.Parse(s.Trim())).ToArray();
            return card;
        }

        public bool HasWins => WinningNumbers.Intersect(ScratchedNumbers).Any();

        public int Score => HasWins ? (int)Math.Pow(2, WinningNumbers.Intersect(ScratchedNumbers).Count() - 1) : 0;
    }
}
