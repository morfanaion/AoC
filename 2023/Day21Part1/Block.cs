

namespace Day21Part1
{
    internal class Block(BlockType type, bool isStart)
    {
        public Block? North { get; set; }
        public Block? East { get; set; }
        public Block? South { get; set; }
        public Block? West { get; set; }

        public BlockType Type { get; set; } = type;

        public bool IsStart { get; set; } = isStart;

        public void SetNorth(Block value)
        {
            North = value;
            value.South = this;
            if(North?.West?.South is Block west)
            {
                SetWest(west);
            }
        }

        public void SetWest(Block west)
        {
            West = west;
            West.East = this;
        }

        public void UnneighbourWalls()
        {
            if(North?.Type == BlockType.Wall)
            {
                North.South = null;
                North = null;
            }
            if (East?.Type == BlockType.Wall)
            {
                East.West = null;
                East = null;
            }
            if (South?.Type == BlockType.Wall)
            {
                South.North = null;
                South = null;
            }
            if (West?.Type == BlockType.Wall)
            {
                West.East = null;
                West = null;
            }
        }

        internal IEnumerable<Block> GetNeighboursForStep(int step)
        {
            if(AllowedStep(North, step)) 
            {
                North.VisitedInNumSteps.Add(step);
                yield return North; 
            }
            if (AllowedStep(East, step))
            {
                East.VisitedInNumSteps.Add(step);
                yield return East;
            }
            if (AllowedStep(South, step))
            {
                South.VisitedInNumSteps.Add(step);
                yield return South;
            }
            if (AllowedStep(West, step))
            {
                West.VisitedInNumSteps.Add(step);
                yield return West;
            }
        }

        private bool AllowedStep(Block? neighbour, int step) => !(neighbour?.VisitedInNumSteps.Any(s => s == step) ?? true);

        public List<int> VisitedInNumSteps { get; } = new List<int>();
    }


}
