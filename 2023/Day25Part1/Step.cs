namespace Day25Part1
{
    internal class Step(Step? previous, Component currentComponent, int stepNumber)
    {
        public Step? Previous { get; set; } = previous;
        public Component CurrentComponent { get; set; } = currentComponent;
        public int StepNumber { get; set; } = stepNumber;

        internal IEnumerable<Component> GetPath(string fromId)
        {
            if (Previous != null && CurrentComponent.Id != fromId)
            {
                foreach (Component component in Previous.GetPath(fromId))
                {
                    yield return component;
                }
            }
            yield return CurrentComponent;
        }
    }
}
