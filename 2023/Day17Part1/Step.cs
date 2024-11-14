namespace Day17Part1
{
    internal struct Step
    {
        public Step() { }
        public CityBlock CurrentBlock { get; set; }
        public CityBlock Last { get; set; }
        public CityBlock Last2nd { get; set; }
        public CityBlock Last3rd { get; set; }
    }
}
