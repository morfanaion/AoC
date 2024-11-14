using Day14Part1;

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
LeverToNorth();
int distance = 1;
int x = ((IEnumerable<List<GridSpot>>)gridSpots).Reverse().Sum(row => (distance++) * row.Count(gridSpot => gridSpot.RockType == RockType.Round));
Console.WriteLine(x);



void LeverToNorth()
{
    GridSpot startSpot = gridSpots[0][0];
    GridSpot? currentNorthSpot = startSpot;
    while(currentNorthSpot != null)
    {
        GridSpot? firstEmptySpot = GetEmptySpotSouth(currentNorthSpot);
        GridSpot? currentSpot = firstEmptySpot?.South;
        while(currentSpot != null)
        {
            switch(currentSpot.RockType)
            {
                case RockType.Round:
#pragma warning disable CS8602 // Impossible to be null here. currentSpot is not null in this instance, but it is taken from firstEmptySpot, false warning
                    firstEmptySpot.RockType = currentSpot.RockType;
#pragma warning restore CS8602 // Impossible to be null here. currentSpot is not null in this instance, but it is taken from firstEmptySpot, false warning
                    currentSpot.RockType = RockType.None;
                    firstEmptySpot = firstEmptySpot.South;
                    break;
                case RockType.Cube:
                    firstEmptySpot = GetEmptySpotSouth(currentSpot);
                    currentSpot = firstEmptySpot?.South;
                    continue;
            }
            currentSpot = currentSpot.South;
        }
        currentNorthSpot = currentNorthSpot.East;
    }
}

GridSpot? GetEmptySpotSouth(GridSpot? currentNorthSpot)
{
    while(currentNorthSpot != null)
    {
        if(currentNorthSpot.RockType == RockType.None)
        {
            return currentNorthSpot;
        }
        currentNorthSpot = currentNorthSpot.South;
    }
    return null;
}