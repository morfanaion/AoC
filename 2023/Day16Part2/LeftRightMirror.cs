namespace Day16Part2
{
    internal class LeftRightMirror : Tile
    {
        public override void Visit(Direction direction, Action<Tile?, Direction> addToQueue)
        {
            if (AlreadyApproachedTo.HasFlag(direction))
            {
                return;
            }
            IsEnergized = true;
            switch (direction)
            {
                case Direction.North:
                    AlreadyApproachedTo |= Direction.East | Direction.North;
                    addToQueue(West, Direction.West);
                    break;
                case Direction.South:
                    AlreadyApproachedTo |= Direction.South | Direction.West;
                    addToQueue(East, Direction.East);
                    break;
                case Direction.East:
                    AlreadyApproachedTo |= Direction.East | Direction.North;
                    addToQueue(South, Direction.South);
                    break;
                case Direction.West:
                    AlreadyApproachedTo |= Direction.South | Direction.West;
                    addToQueue(North, Direction.North);
                    break;
            }
        }
    }
}
