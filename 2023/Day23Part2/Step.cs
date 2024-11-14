namespace Day23Part2
{
    internal class Step(Tile currentTile, int numSteps, Step? previous)
    {
        public Tile CurrentTile { get; set; } = currentTile;
        public int NumSteps { get; set; } = numSteps;
        public Step? Previous { get; set; } = previous;

        public IEnumerable<int> PreviousTileIds()
        {
            Step currentStep = this;
            while(currentStep.Previous != null)
            {
                yield return currentStep.Previous.CurrentTile.Id;
                currentStep = currentStep.Previous;
            }
        }

        public void SetTravelledTiles()
        {
            Step currentStep = this;
            while (currentStep.Previous != null)
            {
                currentStep.CurrentTile.Travelled = true;
                currentStep = currentStep.Previous;
            }
        }
    }
}
