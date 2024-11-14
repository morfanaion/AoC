namespace Day25Part1
{
    internal class Component(string id)
    {
        public List<Component> ConnectedComponents { get; } = new List<Component>();

        public string Id { get; set; } = id;

        public IEnumerable<(string c1, string c2)> GetAllConnection()
        {
            foreach (Component sub in ConnectedComponents)
            {
                string[] names = { sub.Id, Id };
                names = names.OrderBy(name => name).ToArray();
                yield return (names[0], names[1]);
            }
        }

        public IEnumerable<Step> GetSteps(int stepNumber, Step? step) => ConnectedComponents.Select(c => new Step(step, c, stepNumber + 1));
    }
}
