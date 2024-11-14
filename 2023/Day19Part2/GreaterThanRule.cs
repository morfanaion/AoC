namespace Day19Part2
{
    internal class GreaterThanRule : Rule
    {
        private int ComparerNumber;
        private string Var;

        public GreaterThanRule(int comparerNumber, string var, string destination) : base(destination)
        {
            ComparerNumber = comparerNumber;
            Var = var;
        }

        public override (WorkingRange otherRange, long numAccepted) GetNumAccepted(WorkingRange range)
        {
            WorkingRange[] splitRange = range.Split(Var, ComparerNumber);
            switch(Destination)
            {
                case "A":
                    return new(splitRange[0], splitRange[1].NumPermutations);
                case "R":
                    return new(splitRange[0], 0);
                default:
                    return new(splitRange[0], Workflow.Workflows[Destination].GetNumAccepted(splitRange[1]));
            }
        }
    }
}