// See https://aka.ms/new-console-template for more information
using Day23Puzzle01;

Queue<Direction> directionQueue = new Queue<Direction>();
directionQueue.Enqueue(Direction.North);
directionQueue.Enqueue(Direction.South);
directionQueue.Enqueue(Direction.West);
directionQueue.Enqueue(Direction.East);

Dictionary<Position, List<Elf>> chosenDestinations = new Dictionary<Position, List<Elf>>();

List<Elf> elves = new List<Elf>();
int y = 0;
foreach(string line in File.ReadAllLines("Input.txt"))
{
    for(int x = 0; x < line.Length; x++)
    {
        if (line[x] == '#')
        {
            elves.Add(new Elf() { Position = new Position() { X = x, Y = y } });
        }
    }
    y++;
}

for (int i = 0; i< 10; i++)
{
    Direction direction = directionQueue.Dequeue();
    chosenDestinations.Clear();
    foreach(Elf elf in elves)
    {
        List<Elf> elvesWithinRange = elves.Where(otherElf => otherElf.Id != elf.Id && otherElf.Position.WithinRange(elf.Position, 1)).ToList();
        if (!elvesWithinRange.Any())
        {
            continue;
        }
        
        bool directiontoTravelFound = false;
        Position newPosition = elf.Position;
        for (int j = 0; j < 4; j++)
        {
            switch ((Direction)(((int)direction + j) % 4))
            {
                case Direction.North:
                    if(directiontoTravelFound = GetNorthPositions(elf.Position).All(pos => elvesWithinRange.All(otherElf => !otherElf.Position.Equals(pos))))
                    {
                        newPosition = new Position() { X = elf.Position.X, Y = elf.Position.Y - 1 };
                    }
                    break;
                case Direction.South:
                    if (directiontoTravelFound = GetSouthPositions(elf.Position).All(pos => elvesWithinRange.All(otherElf => !otherElf.Position.Equals(pos))))
                    {
                        newPosition = new Position() { X = elf.Position.X, Y = elf.Position.Y + 1 };
                    }
                    break;
                case Direction.East:
                    if (directiontoTravelFound = GetEastPositions(elf.Position).All(pos => elvesWithinRange.All(otherElf => !otherElf.Position.Equals(pos))))
                    {
                        newPosition = new Position() { X = elf.Position.X + 1, Y = elf.Position.Y };
                    }
                    break;
                case Direction.West:
                    if (directiontoTravelFound = GetWestPositions(elf.Position).All(pos => elvesWithinRange.All(otherElf => !otherElf.Position.Equals(pos))))
                    {
                        newPosition = new Position() { X = elf.Position.X - 1, Y = elf.Position.Y };
                    }
                    break;
            }
            if(directiontoTravelFound)
            {
                if(chosenDestinations.TryGetValue(newPosition, out List<Elf>? elvesForThisDestionation))
                {
                    elvesForThisDestionation.Add(elf);
                }
                else
                {
                    chosenDestinations.Add(newPosition, new List<Elf>() { elf });
                }
                break;
            }
        }

    }
    foreach(KeyValuePair<Position, List<Elf>> result in chosenDestinations.Where(kvp => kvp.Value.Count == 1))
    {
        result.Value[0].Position = result.Key;
    }
    
    directionQueue.Enqueue(direction);
}

int minX = elves.Min(elf => elf.Position.X);
int maxX = elves.Max(elf => elf.Position.X);
int minY = elves.Min(elf => elf.Position.Y);
int maxY = elves.Max(elf => elf.Position.Y);
Console.WriteLine(((maxX - minX + 1) * (maxY - minY + 1)) - elves.Count);

IEnumerable<Position> GetNorthPositions(Position position)
{
    yield return new Position() { X = position.X - 1, Y = position.Y - 1 };
    yield return new Position() { X = position.X, Y = position.Y - 1 };
    yield return new Position() { X = position.X + 1, Y = position.Y - 1 };
}

IEnumerable<Position> GetSouthPositions(Position position)
{
    yield return new Position() { X = position.X - 1, Y = position.Y + 1 };
    yield return new Position() { X = position.X, Y = position.Y + 1 };
    yield return new Position() { X = position.X + 1, Y = position.Y + 1 };
}

IEnumerable<Position> GetWestPositions(Position position)
{
    yield return new Position() { X = position.X - 1, Y = position.Y - 1 };
    yield return new Position() { X = position.X - 1, Y = position.Y };
    yield return new Position() { X = position.X - 1, Y = position.Y + 1 };
}

IEnumerable<Position> GetEastPositions(Position position)
{
    yield return new Position() { X = position.X + 1, Y = position.Y - 1 };
    yield return new Position() { X = position.X + 1, Y = position.Y };
    yield return new Position() { X = position.X + 1, Y = position.Y + 1 };
}