namespace Day16Puzzle01
{
    internal class State
    {
        public int MinutesPast { get; set; }
        public int PressureReleased { get; set; }
        public List<Valve> ValvesOpened { get; set; } = new List<Valve>();
        public List<Valve> ValvesToOpen { get; set; } = new List<Valve>();
        public PuzzleAction Action { get; set; }
        public Valve CurrentValve { get; set; }
        public string ValveOpenOrder { get; set; } = string.Empty;
        public State(Valve currentValve)
        {
            CurrentValve = currentValve;
        }
    }
}
