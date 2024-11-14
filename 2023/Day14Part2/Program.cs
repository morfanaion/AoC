using Day14Part2;

List<List<GridSpot>> gridSpots = new List<List<GridSpot>>();
List<GridSpot>? previousRow = null;
foreach(string line in File.ReadAllLines("input.txt"))
{
    List<GridSpot> spotRow = line.Select(c => new GridSpot(c)).ToList();
    if(previousRow ==  null)
    {
        foreach((GridSpot curr, GridSpot previous) pair in spotRow.Skip(1).Zip<GridSpot, GridSpot, (GridSpot curr, GridSpot previous)>(spotRow, (curr, prev) => new (curr, prev)))
        {
            pair.curr.SetWest(pair.previous);
        }
    }
    else
    {
        foreach ((GridSpot south, GridSpot north) pair in spotRow.Zip<GridSpot, GridSpot, (GridSpot south, GridSpot north)>(previousRow, (south, north) => new(south, north)))
        {
            pair.south.SetNorth(pair.north);
        }
    }
    previousRow = spotRow;
    gridSpots.Add(spotRow);
}

List<string> resultPatterns = new List<string>();

bool repeatPatternFound = false;
for (long i = 0; i < 1000000000; i++)
{
    LeverCycle();
    if (!repeatPatternFound)
    {
        resultPatterns.Add(string.Join("", gridSpots.Select(row => string.Join("", row.Select(g => (char)g.RockType)))));
        if (resultPatterns.Count(pattern => pattern == resultPatterns.Last()) == 3)
        {
            int currentId = resultPatterns.Count - 1;
            int previousId = resultPatterns.FindLastIndex(resultPatterns.Count - 2, str => str == resultPatterns[currentId]);
            int patternRepeatInterval = currentId - previousId;
            repeatPatternFound = true;
            for (int idx = currentId; idx > previousId; idx--)
            {
                if (resultPatterns[idx] != resultPatterns[idx - patternRepeatInterval])
                {
                    repeatPatternFound = false;
                    break;
                }
            }
            if(repeatPatternFound)
            {
                long cyclesToDo = 1000000000 - i;
                long patternRepeatsThatFit = cyclesToDo / patternRepeatInterval;
                i += patternRepeatsThatFit * patternRepeatInterval;
            }
        }
    }
}

int distance = 1;
int x = ((IEnumerable<List<GridSpot>>)gridSpots).Reverse().Sum(row => (distance++) * row.Count(gridSpot => gridSpot.RockType == RockType.Round));
Console.WriteLine(x);

void LeverCycle()
{
    int x = 0;
    LeverToNorth();
    LeverToWest();
    LeverToSouth();
    LeverToEast();
}

void LeverAction(GridSpot startSpot, Func<GridSpot, GridSpot?> getEmptySpot, Func<GridSpot?, GridSpot?> next, Func<GridSpot?, GridSpot?> nextStart)
{
    GridSpot? currentStartSpot = startSpot;
    while (currentStartSpot != null)
    {
        GridSpot? firstEmptySpot = getEmptySpot(currentStartSpot);
        GridSpot? currentSpot = next(firstEmptySpot);
        while (currentSpot != null)
        {
            switch (currentSpot.RockType)
            {
                case RockType.Round:
#pragma warning disable CS8602 // Impossible to be null here. currentSpot is not null in this instance, but it is taken from firstEmptySpot, false warning
                    firstEmptySpot.RockType = currentSpot.RockType;
#pragma warning restore CS8602 // Impossible to be null here. currentSpot is not null in this instance, but it is taken from firstEmptySpot, false warning
                    currentSpot.RockType = RockType.None;
                    firstEmptySpot = next(firstEmptySpot);
                    break;
                case RockType.Cube:
                    firstEmptySpot = getEmptySpot(currentSpot);
                    currentSpot = next(firstEmptySpot);
                    continue;
            }
            currentSpot = next(currentSpot);
        }
        currentStartSpot = nextStart(currentStartSpot);
    }
}


void LeverToNorth() => LeverAction(gridSpots[0][0], GetEmptySpotSouth, g => g?.South, g => g.East);
void LeverToEast() => LeverAction(gridSpots[0].Last(), GetEmptySpotWest, g => g?.West, g => g.South);
void LeverToSouth() => LeverAction(gridSpots.Last()[0], GetEmptySpotNorth, g => g?.North, g => g.East);
void LeverToWest() => LeverAction(gridSpots[0][0], GetEmptySpotEast, g => g?.East, g => g.South);

GridSpot? GetEmptySpot(GridSpot? currentSpot, Func<GridSpot, GridSpot?> next)
{
    while (currentSpot != null)
    {
        if (currentSpot.RockType == RockType.None)
        {
            return currentSpot;
        }
        currentSpot = next(currentSpot);
    }
    return null;
}

GridSpot? GetEmptySpotNorth(GridSpot? currentSouthSpot) => GetEmptySpot(currentSouthSpot, g => g.North);
GridSpot? GetEmptySpotEast(GridSpot? currentWestSpot) => GetEmptySpot(currentWestSpot, g => g.East);
GridSpot? GetEmptySpotSouth(GridSpot? currentNorthSpot) => GetEmptySpot(currentNorthSpot, g => g.South);
GridSpot? GetEmptySpotWest(GridSpot? currentEastSpot) => GetEmptySpot(currentEastSpot, g => g.West);
