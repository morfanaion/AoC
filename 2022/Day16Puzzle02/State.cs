namespace Day16Puzzle02
{
    internal class State
    {
        public int MinutesPast { get; set; }
        public int PressureReleased { get; set; }
        public List<Valve> ValvesOpened { get; set; } = new List<Valve>();
        public List<Valve> ValvesToOpen { get; set; } = new List<Valve>();
        public PuzzleAction Action { get; set; }
        public Valve CurrentValve { get; set; }
        public int OtherTimeRemaining { get; set; }
        public Valve OtherValve { get; set; }
        public State(Valve currentValve, Valve otherValve)
        {
            CurrentValve = currentValve;
            OtherValve = otherValve;
        }
    }
}
