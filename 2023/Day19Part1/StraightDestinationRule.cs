namespace Day19Part1
{
    internal class StraightDestinationRule : Rule
    {
        private Action<Part, Action<Part, string>> SetDestination;

        public StraightDestinationRule(string destination)
        {
            Destination = destination;
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

        public string Destination { get; }

        internal override bool Process(Part part, Action<Part, string> enqueue)
        {
            SetDestination(part, enqueue);
            return true;
        }
    }
}
