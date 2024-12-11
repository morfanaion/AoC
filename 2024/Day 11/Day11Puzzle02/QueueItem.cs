namespace Day11Puzzle02
{
    internal class QueueItem
    {
        public long Number { get; set; }
        public int NumBlinksRemaining { get; set; }

        public long[]? WaitingFor { get; set; }
    }
}
