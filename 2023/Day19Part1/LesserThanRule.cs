namespace Day19Part1
{
    internal class LesserThanRule : Rule
    {
        private int ComparerNumber;
        private Func<Part, int> ValueGetter;
        private string Destination;
        private Action<Part, Action<Part, string>> SetDestination;

        public LesserThanRule(int comparerNumber, string destination, Func<Part, int> valueGetter)
        {
            ComparerNumber = comparerNumber;
            Destination = destination;
            ValueGetter = valueGetter;
            switch (Destination)
            {
                case "A":
                    SetDestination = (p, q) => p.Accepted = true;
                    break;
                case "R":
                    SetDestination = (p, q) => p.Accepted = false;
                    break;
                default:
                    SetDestination = (p, q) => q(p, destination);
                    break;
            }
        }

        internal override bool Process(Part part, Action<Part, string> enqueue)
        {
            if (ValueGetter(part) < ComparerNumber)
            {
                SetDestination(part, enqueue);
                return true;
            }
            return false;
        }
    }
}