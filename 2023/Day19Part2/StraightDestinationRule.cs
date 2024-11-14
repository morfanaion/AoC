namespace Day19Part2
{
    internal class StraightDestinationRule : Rule
    {
        public StraightDestinationRule(string destination) : base(destination)
        {
        }

        public override (WorkingRange otherRange, long numAccepted) GetNumAccepted(WorkingRange workingRange)
        {
            switch (Destination)
            {
                case "A":
                    return new(WorkingRange.Nil, workingRange.NumPermutations);
                case "R":
                    return new(WorkingRange.Nil, 0);
                default:
                    return new(WorkingRange.Nil, Workflow.Workflows[Destination].GetNumAccepted(workingRange));
            }
        }
    }
}
