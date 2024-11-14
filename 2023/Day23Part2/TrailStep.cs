
namespace Day23Part2
{
    internal class TrailStep(Tile currentTile, int numSteps, TrailStep? previous)
    {
        public Tile CurrentTile { get; set; } = currentTile;
        public int NumSteps { get; set; } = numSteps;
        public TrailStep? Previous { get; set; } = previous;

        internal IEnumerable<int> PreviousTileIds()
        {
            TrailStep currentStep = this;
            while (currentStep.Previous != null)
            {
                yield return currentStep.Previous.CurrentTile.Id;
                currentStep = currentStep.Previous;
            }
        }
    }
}
