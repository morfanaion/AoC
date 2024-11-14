namespace Day16Part1
{
    internal class HorizontalSplitter : Tile
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
                case Direction.South:
                    AlreadyApproachedTo |= Direction.North | Direction.South | Direction.East | Direction.West;
                    addToQueue(East, Direction.East);
                    addToQueue(West, Direction.West);
                    break;
                case Direction.East:
                    AlreadyApproachedTo |= Direction.East | Direction.West;
                    addToQueue(East, Direction.East);
                    break;
                case Direction.West:
                    AlreadyApproachedTo |= Direction.East | Direction.West;
                    addToQueue(West, Direction.West);
                    break;
            }
        }
    }
}
