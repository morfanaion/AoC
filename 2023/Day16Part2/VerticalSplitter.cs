namespace Day16Part2
{
    internal class VerticalSplitter : Tile
    {
        public override void Visit(Direction direction, Action<Tile?, Direction> addToQueue)
        {
            if(AlreadyApproachedTo.HasFlag(direction))
            {
                return;
            }
            IsEnergized = true;
            switch (direction)
            {
                case Direction.North:
                    AlreadyApproachedTo |= Direction.East | Direction.West;
                    addToQueue(North, Direction.North);
                    break;
                case Direction.South:
                    AlreadyApproachedTo |= Direction.East | Direction.West;
                    addToQueue(South, Direction.South);
                    break;
                case Direction.East:
                case Direction.West:
                    AlreadyApproachedTo |= Direction.East | Direction.West;
                    addToQueue(North, Direction.North);
                    addToQueue(South, Direction.South);
                    break;
            }
        }
    }
}
