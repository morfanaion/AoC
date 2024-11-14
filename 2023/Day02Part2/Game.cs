namespace Day02Part2
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

        public Draw GetMaximizedDraw() => new Draw()
        {
            Red = Draws?.Max(d => d.Red) ?? 0,
            Green = Draws?.Max(g => g.Green) ?? 0,
            Blue = Draws?.Max(b => b.Blue) ?? 0
        };
    }
}
