namespace Day19Part2
{
    internal class LesserThanRule : Rule
    {
        private int ComparerNumber;
        private string Var;

        public LesserThanRule(int comparerNumber, string var, string destination) : base(destination)
        {
            ComparerNumber = comparerNumber;
            Var = var;
        }

        public override (WorkingRange otherRange, long numAccepted) GetNumAccepted(WorkingRange range)
        {
            WorkingRange[] splitRange = range.Split(Var, ComparerNumber - 1);
            switch (Destination)
            {
                case "A":
                    return new(splitRange[1], splitRange[0].NumPermutations);
                case "R":
                    return new(splitRange[1], 0);
                default:
                    return new(splitRange[1], Workflow.Workflows[Destination].GetNumAccepted(splitRange[0]));
            }
        }
    }
}