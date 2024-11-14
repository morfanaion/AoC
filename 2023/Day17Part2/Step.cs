namespace Day17Part2
{
    internal struct Step
    {
        public Step() { }
        public CityBlock CurrentBlock { get; set; }
        public CityBlock Last { get; set; }
        public CityBlock Last2nd { get; set; }
        public CityBlock Last3rd { get; set; }
        public CityBlock Last4th { get; set; }
        public CityBlock Last5th { get; set; }
        public CityBlock Last6th { get; set; }
        public CityBlock Last7th { get; set; }
        public CityBlock Last8th { get; set; }
        public CityBlock Last9th { get; set; }
        public CityBlock Last10th { get; set; }

        public Step NextStep(CityBlock block)
        {
            return new Step()
            {
                CurrentBlock = block,
                Last = CurrentBlock,
                Last2nd = Last,
                Last3rd = Last2nd,
                Last4th = Last3rd,
                Last5th = Last4th,
                Last6th = Last5th,
                Last7th = Last6th,
                Last8th = Last7th,
                Last9th = Last8th,
                Last10th = Last9th
            };
        }
    }
}
