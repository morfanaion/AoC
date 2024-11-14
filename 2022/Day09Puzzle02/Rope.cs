namespace Day09Puzzle02
{
    internal class Rope
    {
        Position[] partPositions;

        public Rope(int numPositions, int startX, int startY)
        {
            partPositions = new Position[numPositions];
            for(int i = 0; i < numPositions; i++)
            {
                partPositions[i] = new Position() { X = startX, Y = startY };
            }
        }

        public void MoveUp()
        {
            Move(() => partPositions[0].Y++);
        }

        public void Move(Action headMoveAction)
        {
            Position previousPositionPreviousPart = new Position(partPositions[0]);
            headMoveAction();
            for (int i = 1; i < partPositions.Length; i++)
            {
                if(partPositions[i].DistanceFrom(partPositions[i-1]) > 1)
                {
                    if(partPositions[i].X == partPositions[i-1].X)
                    {
                        if (partPositions[i].Y > partPositions[i - 1].Y)
                        {
                            partPositions[i].Y--;
                        }
                        else
                        {
                            partPositions[i].Y++;
                        }
                    }
                    else if (partPositions[i].Y == partPositions[i - 1].Y)
                    {
                        if (partPositions[i].X > partPositions[i - 1].X)
                        {
                            partPositions[i].X--;
                        }
                        else
                        {
                            partPositions[i].X++;
                        }
                    }
                    else
                    {
                        if (partPositions[i].X > partPositions[i - 1].X && partPositions[i].Y > partPositions[i - 1].Y)
                        {
                            partPositions[i].Y--;
                            partPositions[i].X--;
                        }
                        else if (partPositions[i].X > partPositions[i - 1].X && partPositions[i].Y < partPositions[i - 1].Y)
                        {
                            partPositions[i].Y++;
                            partPositions[i].X--;
                        }
                        else if (partPositions[i].X < partPositions[i - 1].X && partPositions[i].Y < partPositions[i - 1].Y)
                        {
                            partPositions[i].Y++;
                            partPositions[i].X++;
                        }
                        else if (partPositions[i].X < partPositions[i - 1].X && partPositions[i].Y > partPositions[i - 1].Y)
                        {
                            partPositions[i].Y--;
                            partPositions[i].X++;
                        }
                    }
                }
                else
                {
                    break;
                }
            }
        }

        public Position TailPosition => partPositions[partPositions.Length - 1];

        public void MoveDown()
        {
            Move(() => partPositions[0].Y--);
        }

        public void MoveLeft()
        {
            Move(() => partPositions[0].X--);
        }

        public void MoveRight()
        {
            Move(() => partPositions[0].X++);
        }
    }
}
