namespace Day19Part2
{
    internal struct WorkingRange
    {
        public static WorkingRange Nil = new WorkingRange(0, 0, 0, 0, 0, 0, 0, 0);
        public static WorkingRange Complete = new WorkingRange(0, 4000, 0, 4000, 0, 4000, 0, 4000);

        public WorkingRange(long xMinimumRange, long xMaximumRange, long mMinimumRange, long mMaximumRange, long aMinimumRange, long aMaximumRange, long sMinimumRange, long sMaximumRange)
        {
            XMinimumRange = xMinimumRange;
            XMaximumRange = xMaximumRange;
            MMinimumRange = mMinimumRange;
            MMaximumRange = mMaximumRange;
            AMinimumRange = aMinimumRange;
            AMaximumRange = aMaximumRange;
            SMinimumRange = sMinimumRange;
            SMaximumRange = sMaximumRange;
        }

        public WorkingRange[] Split(string var, long value)
        {
            return new WorkingRange[2]
            {
                new WorkingRange(XMinimumRange, var == "x" ? value : XMaximumRange, MMinimumRange, var == "m" ? value : MMaximumRange, AMinimumRange, var =="a" ? value : AMaximumRange, SMinimumRange, var == "s" ? value : SMaximumRange),
                new WorkingRange(var == "x" ? value : XMinimumRange, XMaximumRange, var == "m" ? value : MMinimumRange, MMaximumRange, var =="a" ? value : AMinimumRange, AMaximumRange, var == "s" ? value : SMinimumRange, SMaximumRange)
            };
        }

        public long XMinimumRange { get; set; }
        public long XMaximumRange { get; set; }
        public long MMinimumRange { get; set; }
        public long MMaximumRange { get; set; }
        public long AMinimumRange { get; set; }
        public long AMaximumRange { get; set; }
        public long SMinimumRange { get; set; }
        public long SMaximumRange { get; set; }

        public long NumPermutations => (XMaximumRange - XMinimumRange) * (MMaximumRange - MMinimumRange) * (AMaximumRange - AMinimumRange) * (SMaximumRange - SMinimumRange);
    }
}
