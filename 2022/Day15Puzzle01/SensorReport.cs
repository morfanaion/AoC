namespace Day15Puzzle01
{
    internal class SensorReport
    {
        public int SensorX { get; set; }
        public int SensorY { get; set; }
        public int BeaconX { get; set; }
        public int BeaconY { get; set; }
        public int ManhattanDistance => Math.Abs(SensorX - BeaconX) + Math.Abs(SensorY - BeaconY);
    }
}
