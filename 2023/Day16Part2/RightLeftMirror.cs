namespace Day16Part2
{
    internal class RightLeftMirror : Tile
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
                    AlreadyApproachedTo |= Direction.West | Direction.North;
                    addToQueue(East, Direction.East);
                    break;
                case Direction.South:
                    AlreadyApproachedTo |= Direction.South | Direction.East;
                    addToQueue(West, Direction.West);
                    break;
                case Direction.East:
                    AlreadyApproachedTo |= Direction.East | Direction.South;
                    addToQueue(North, Direction.North);
                    break;
                case Direction.West:
                    AlreadyApproachedTo |= Direction.North | Direction.West;
                    addToQueue(South, Direction.South);
                    break;
            }
        }
    }
}
