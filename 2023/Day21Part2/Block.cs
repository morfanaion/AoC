namespace Day21Part2
{
    internal class Block(int x, int y, BlockType type, bool isStart)
    {
        public int X { get; } = x;
        public int Y { get; } = y;

        public Block? North { get; set; }
        public Block? East { get; set; }
        public Block? South { get; set; }
        public Block? West { get; set; }

        public BlockType Type { get; set; } = type;

        public bool IsStart { get; set; } = isStart;

        public void SetNorth(Block north)
        {
            north.South.North = this;
            South = north.South;
            north.South = this;
            North = north;
            if (North?.West?.South is Block west)
            {
                if (west.Y == Y)
                {
                    SetWest(west);
                }
            }
        }

        public void SetWest(Block west)
        {
            west.East.West = this;
            East = west.East;
            west.East = this;
            West = west;
        }

        public void UnneighbourWalls()
        {
            if (North?.Type == BlockType.Wall)
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

        internal IEnumerable<(Block, int, int)> GetNeighboursForStep(long step, int x, int y)
        {
            (bool allowed, int newX, int newY) = AllowedStep(North, x, y, step);
            if (allowed)
            {
                yield return (North, newX, newY);
            }
            (allowed, newX, newY) = AllowedStep(East, x, y, step);
            if (allowed)
            {
                yield return (East, newX, newY);
            }
            (allowed, newX, newY) = AllowedStep(South, x, y, step);
            if (allowed)
            {
                yield return (South, newX, newY);
            }
            (allowed, newX, newY) = AllowedStep(West, x, y, step);
            if (allowed)
            {
                yield return (West, newX, newY);
            }
        }

        public long GetNumForSteps(long numSteps)
        {
            if(Type == BlockType.Wall)
            {
                return 0;
            }
            if(Visited.Count == 0)
            {
                return 0;
            }
            long controlNumber = -1;
            if (numSteps < 1000)
            {
                controlNumber = Visited.Count(kvp => kvp.Value <= numSteps && (kvp.Value % 2) == (numSteps % 2));
            }
            long diff = Visited[(-4, -4)] - Visited[(-4, -3)];
            int firstX = ((Visited[(-1, 0)] - Visited[(0, 0)]) == diff) ? 0 : -1;
            int firstY = ((Visited[(0, -1)] - Visited[(0, 0)]) == diff) ? 0 : -1;
            (int x, int y) topleftcoordinates = FindTopleftCoordinate(diff);
            (int x, int y) toprightcoordinates = FindToprightCoordinate(diff);
            (int x, int y) bottomleftcoordinates = FindBottomleftCoordinate(diff);
            (int x, int y) bottomrightcoordinates = FindBottomRightCoordinate(diff);
            if(topleftcoordinates == toprightcoordinates)
            {
                toprightcoordinates.x++;
            }
            if(topleftcoordinates == bottomleftcoordinates)
            {
                bottomleftcoordinates.y++;
            }
            if(toprightcoordinates == bottomrightcoordinates)
            {
                bottomrightcoordinates.y++;
            }
            if(bottomleftcoordinates == bottomrightcoordinates)
            {
                bottomrightcoordinates.x++;
            }

            long result = 0;
            result += GetNumPlotsForQuarter(Visited[(topleftcoordinates.x, topleftcoordinates.y)], diff, numSteps);
            result += GetNumPlotsForQuarter(Visited[(toprightcoordinates.x, toprightcoordinates.y)], diff, numSteps);
            result += GetNumPlotsForQuarter(Visited[(bottomleftcoordinates.x, bottomleftcoordinates.y)], diff, numSteps);
            result += GetNumPlotsForQuarter(Visited[(bottomrightcoordinates.x, bottomrightcoordinates.y)], diff, numSteps);
            bool addCenter = !( new (int, int)[] { topleftcoordinates, toprightcoordinates, bottomleftcoordinates, bottomrightcoordinates }).Contains((0,0));
            if (toprightcoordinates.x - topleftcoordinates.x > 1)
            {
                int y = -1;
                while (Visited[(0, y - 1)] - Visited[(0, y)] != diff)
                {
                    if (Visited[(0, y)] <= numSteps && (Visited[(0, y)] % 2) == (numSteps % 2))
                    {
                        result++;
                    }
                    y--;
                }
                result += GetNumPlotsForSequence(Visited[(0, y)], diff, numSteps);
            }
            if(bottomleftcoordinates.y - topleftcoordinates.y > 1)
            {
                int x = -1;
                while (Visited[(x - 1, 0)] - Visited[(x, 0)] != diff)
                {
                    if (Visited[(x, 0)] <= numSteps && (Visited[(x, 0)] % 2) == (numSteps % 2))
                    {
                        result++;
                    }
                    x--;
                }
                result += GetNumPlotsForSequence(Visited[(x, 0)], diff, numSteps);
            }
            if (bottomrightcoordinates.x - bottomleftcoordinates.x > 1)
            {
                int y = 1;
                while (Visited[(0, y + 1)] - Visited[(0, y)] != diff)
                {
                    if (Visited[(0, y)] <= numSteps && (Visited[(0, y)] % 2) == (numSteps % 2))
                    {
                        result++;
                    }
                    y++;
                }
                result += GetNumPlotsForSequence(Visited[(0, y)], diff, numSteps);

            }
            if (bottomrightcoordinates.y - toprightcoordinates.y > 1)
            {
                int x = 1;
                while (Visited[(x + 1, 0)] - Visited[(x, 0)] != diff)
                {
                    if (Visited[(x, 0)] <= numSteps && (Visited[(x, 0)] % 2) == (numSteps % 2))
                    {
                        result++;
                    }
                    x++;
                }
                result += GetNumPlotsForSequence(Visited[(x, 0)], diff, numSteps);

            }
            if (addCenter)
            {
                if (Visited[(0, 0)] <= numSteps && (Visited[(0, 0)] % 2) == (numSteps % 2))
                {
                    result++;
                }
            }
            if (controlNumber != -1 && controlNumber != result)
            {
                int x = 0;
            }
            return result;
        }

        public string GridRound()
        {
            string result = string.Empty;
            for (int y = -10; y <= 10; y++)
            {
                string line = string.Empty;
                for (int x = -10; x <= 10; x++)
                {
                    line += $" {Visited[(x, y)]}";
                }
                result += line.Trim() + '\n';
            }
            return result;
        }

        private (int x, int y) FindTopleftCoordinate(long diff)
        {
            if (Try(0, 0, -1, -1, -2, -2, diff))
            {
                return (0, 0);
            }
            if (Try(-1, 0, -2, -1, -3, -2, diff))
            {
                return (-1, 0);
            }
            if (Try(0, -1, -1, -2, -2, -3, diff))
            {
                return (0, -1);
            }
            if (Try(-1, -1, -2, -2, -3, -3, diff))
            {
                return (-1, -1);
            }
            throw new Exception("?");
        }
        private (int x, int y) FindToprightCoordinate(long diff)
        {
            if (Try(0, 0, 1, -1, 2, -2, diff))
            {
                return (0, 0);
            }
            if (Try(1, 0, 2, -1, 3, -2, diff))
            {
                return (1, 0);
            }
            if (Try(0, -1, 1, -2, 2, -3, diff))
            {
                return (0, -1);
            }
            if (Try(1, -1, 2, -2, 3, -3, diff))
            {
                return (1, -1);
            }
            throw new Exception("?");
        }
        private (int x, int y) FindBottomleftCoordinate(long diff)
        {
            if (Try(0, 0, -1, 1, -2, 2, diff))
            {
                return (0, 0);
            }
            if (Try(-1, 0, -2, 1, 3, 2, diff))
            {
                return (-1, 0);
            }
            if (Try(0, 1, -1, 2, -2, 3, diff))
            {
                return (0, 1);
            }
            if (Try(-1, 1, -2, 2, -3, 3, diff))
            {
                return (-1, 1);
            }
            throw new Exception("?");
        }
        private (int x, int y) FindBottomRightCoordinate(long diff)
        {
            if (Try(0, 0, 1, 1, 2, 2, diff))
            {
                return (0, 0);
            }
            if (Try(1, 0, 2, 1, 3, 2, diff))
            {
                return (1, 0);
            }
            if (Try(0, 1, 1, 2, 2, 3, diff))
            {
                return (0, 1);
            }
            if (Try(1, 1, 2, 2, 3, 3, diff))
            {
                return (1, 1);
            }
            throw new Exception("?");
        }

        private bool Try(int x, int y, int neighbourX, int neighbourY, int nextneighbourX, int nextNeighbourY, long diff)
        {
            return ((Visited[(neighbourX, y)] - Visited[(x, y)]) == diff) && 
                ((Visited[(x, neighbourY)] - Visited[(x, y)]) == diff) &&
                ((Visited[(nextneighbourX, y)] - Visited[(neighbourX, y)]) == diff) &&
                ((Visited[(x, nextNeighbourY)] - Visited[(x, neighbourY)]) == diff);
        }

        private long GetNumPlotsForSequence(long startNumber, long stepSize, long numSteps)
        {
            if (startNumber > numSteps) { return 0; }
            long numbersInSequence = (numSteps - startNumber) / stepSize + 1;
            if (stepSize % 2 == 0)
            {
                //even stepsize, determine if startNumber and numSteps are both even or odd
                if ((startNumber % 2) != (numSteps % 2))
                {
                    // they differ, we have none
                    return 0;
                }
                else
                {
                    // same, all are good
                    return numbersInSequence;
                }
            }
            if (numbersInSequence % 2 == 0)
            {
                // equal number of odd and even, so just return half the numbers
                return numbersInSequence / 2;
            }
            if ((startNumber % 2) == (numSteps % 2))
            {
                // first number clears, we have an uneven number, return half + 1
                return numbersInSequence / 2 + 1;
            }
            else
            {
                // otherwise integer division by 2
                return numbersInSequence / 2;
            }
        }

        private long GetNumPlotsForQuarter(long startNumber, long stepSize, long numSteps)
        {
            if (startNumber > numSteps) { return 0; }
            long numbersInSequence = (numSteps - startNumber) / stepSize + 1;
            if (stepSize % 2 == 0)
            {
                if ((startNumber % 2) != (numSteps % 2))
                {
                    // one is odd, the other isn't, they will never pair up
                    return 0;
                }
                else
                {
                    // they're the same, they will always be the same, all numbers under maximum are valid
                    return numbersInSequence * (numbersInSequence + 1) / 2;
                }
            }
            // ok, so we've got combinations of odd and evens, let's see what we have. We need to find the position of the highest value in the sequence.
            // If maximum is odd, we want an odd number
            if (numSteps % 2 == 1)
            {
                // now, if start was an odd number, than the first, third, fifth, etc. will be odd
                if (startNumber % 2 == 1)
                {
                    // ok, so we know that the last ok one has to be at an uneven spot
                    if (numbersInSequence % 2 == 0)
                    {
                        // even, so the good number is the one before this, subtract one
                        numbersInSequence--;
                    }
                }
                else
                {
                    // ok, first one was even, so the odd numbers are in 2, 4, 6, 8
                    if (numbersInSequence % 2 == 1)
                    {
                        numbersInSequence--;
                    }
                }
            }
            else
            {
                // now, if start was an odd number, than the first, third, fifth, etc. will be odd
                if (startNumber % 2 == 1)
                {
                    // ok, so we know that the last ok one has to be at an event spot
                    if (numbersInSequence % 2 == 1)
                    {
                        // even, so the good number is the one before this, subtract one
                        numbersInSequence--;
                    }
                }
                else
                {
                    // ok, first one was odd, so the even numbers are in 2, 4, 6, 8
                    if (numbersInSequence % 2 == 0)
                    {
                        numbersInSequence--;
                    }
                }
            }

            return ((long)Math.Pow(numbersInSequence + 1, 2)) / 4;
        }

        private (bool, int, int) AllowedStep(Block? neighbour, int x, int y, long step)
        {
            if (neighbour == null)
            {
                return (false, x, y);
            }
            int diffX = neighbour.X - X;
            if (Math.Abs(diffX) > 1)
            {
                if (diffX > 0)
                {
                    x--;
                }
                else
                {
                    x++;
                }
            }
            int diffY = neighbour.Y - Y;
            if (Math.Abs(diffY) > 1)
            {
                if (diffY > 0)
                {
                    y--;
                }
                else
                {
                    y++;
                }
            }
            if (x > 10 || y > 10 || x < -10 || y < -10)
            {
                return (false, x, y);
            }
            if (neighbour.Visited.ContainsKey((x, y)))
            {
                return (false, x, y);
            }
            neighbour.Visited[(x, y)] = step;
            return (true, x, y);
        }

        public Dictionary<(int x, int y), long> Visited { get; } = new Dictionary<(int x, int y), long>();
    }


}
