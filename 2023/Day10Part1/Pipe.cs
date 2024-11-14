namespace Day10Part1
{
    internal class Pipe
    {
        public static Pipe[][] ThePipeGrid { get; set; } = new Pipe[0][];

        public char Type { get; set; }
        public long MyDistance { get; set; } = long.MaxValue;

        public void SetDistance(int myX, int myY, Action<Pipe, int, int, long> addToQueue, long currentDistance = 0)
        {
            if (MyDistance > currentDistance)
            {
                MyDistance = currentDistance;
                if (myY > 0 && IsPipeType(Type, 'S', '|', 'L', 'J') && IsPipeType(ThePipeGrid[myY - 1][myX].Type, '|', 'F', '7'))
                {
                    addToQueue(ThePipeGrid[myY - 1][myX], myX, myY - 1, currentDistance + 1);
                }
                if (myY < ThePipeGrid.Length - 1 && IsPipeType(Type, 'S', '|', 'F', '7') && IsPipeType(ThePipeGrid[myY + 1][myX].Type, '|', 'L', 'J'))
                {
                    addToQueue(ThePipeGrid[myY + 1][myX], myX, myY + 1, currentDistance + 1);
                }
                if (myX > 0 && IsPipeType(Type, 'S', '-', '7', 'J') && IsPipeType(ThePipeGrid[myY][myX - 1].Type, '-', 'F', 'L'))
                {
                    addToQueue(ThePipeGrid[myY][myX - 1], myX - 1, myY, currentDistance + 1);
                }
                if (myX < ThePipeGrid[myX].Length - 1 && IsPipeType(Type, 'S', '-', 'F', 'L') && IsPipeType(ThePipeGrid[myY][myX + 1].Type, '-', '7', 'J'))
                {
                    addToQueue(ThePipeGrid[myY][myX + 1], myX + 1, myY, currentDistance + 1);
                }
            }
        }

        private bool IsPipeType(char type, params char[] otherTypes) => otherTypes.Contains(type);
    }
}
