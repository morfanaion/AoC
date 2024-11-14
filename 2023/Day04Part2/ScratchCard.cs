namespace Day04Part2
{
    internal class ScratchCard
    {
        public static List<ScratchCard> Cards = new List<ScratchCard>();

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

        private int? _score;
        public int Score => _score ??= HasWins ? 1 + Cards.Skip(Id).Take(WinningNumbers.Intersect(ScratchedNumbers).Count()).Sum(c => c.Score)  : 1;
    }
}
