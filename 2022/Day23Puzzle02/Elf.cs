namespace Day23Puzzle02
{
    internal class Elf
    {
        private static int Counter { get; set; } = 0;
        public int Id { get; } = Counter++;
        public Position Position { get; set; }
    }
}
