namespace Day02Part1
{
    internal class Game
    {
        public int Id { get; private set; }
        public Draw[]? Draws { get; private set; }

        public static Game FromString(string str)
        {
            Game game = new Game();
            string[] substrs = str.Split(':');
            game.Id = int.Parse(string.Join("", substrs[0].Skip(5)));
            game.Draws = substrs[1].Split(';').Select(Draw.FromString).ToArray();
            return game;
        }

        public bool IsGameValid(int maxRed, int maxGreen, int maxBlue)
        {
            if(Draws == null) return false;
            return Draws.All(d => d.IsDrawValid(maxRed, maxGreen, maxBlue));
        }
    }
}
