using Day12Puzzle02;

DateTime now = DateTime.Now;
Plot[][] _plots = File.ReadAllLines("input.txt").Select(line => line.Select(c => new Plot() { PlotType = c }).ToArray()).ToArray();
for(int y = 0; y < _plots.Length; y++)
{
    for(int x = 0;  x < _plots[y].Length; x++)
    {
        Plot currentPlot = _plots[y][x];
        currentPlot.X = x;
        currentPlot.Y = y;
        if(x > 0)
        {
            Plot otherPlot = _plots[y][x - 1];
            if(currentPlot.PlotType == otherPlot.PlotType)
            {
                currentPlot.FenceWest = false;
                currentPlot.Perimeter--;
                MergeRegions(otherPlot.RegionId, currentPlot.RegionId);
            }
        }
        if (y > 0)
        {
            Plot otherPlot = _plots[y - 1][x];
            if (currentPlot.PlotType == otherPlot.PlotType)
            {
                currentPlot.FenceNorth = false;
                currentPlot.Perimeter--;
                MergeRegions(otherPlot.RegionId, currentPlot.RegionId);
            }
        }
        if (x + 1 < _plots[y].Length)
        {
            Plot otherPlot = _plots[y][x + 1];
            if (currentPlot.PlotType == otherPlot.PlotType)
            {
                currentPlot.FenceEast = false;
                currentPlot.Perimeter--;
                MergeRegions(currentPlot.RegionId, otherPlot.RegionId);
            }
        }
        if (y + 1 < _plots.Length)
        {
            Plot otherPlot = _plots[y + 1][x];
            if (currentPlot.PlotType == otherPlot.PlotType)
            {
                currentPlot.FenceSouth = false;
                currentPlot.Perimeter--;
                MergeRegions(currentPlot.RegionId, otherPlot.RegionId);
            }
        }
    }
}

void MergeRegions(long region1, long region2)
{
    foreach(Plot plot in _plots.SelectMany(row => row).Where(p => p.RegionId == region2))
    {
        plot.RegionId = region1;
    }
}

int CalculateSides(long regionId, IEnumerable<Plot> plots)
{
    int sides = 0;
    int minX = plots.Min(p => p.X);
    int maxX = plots.Max(p => p.X);
    int minY = plots.Min(p => p.Y);
    int maxY = plots.Max(p => p.Y);
    for(int x = minX; x <= maxX; x++)
    {
        for(int y = minY; y <= maxY; y++)
        {
            Plot currentPlot = _plots[y][x];
            if(currentPlot.RegionId == regionId)
            {
                if(currentPlot.Perimeter == 0) // no perimeter, so nothing to check
                {
                    continue;
                }
                if(currentPlot.FenceNorth)
                {
                    if(x > minX)
                    {
                        Plot otherPlot = _plots[y][x - 1];
                        if (!(otherPlot.RegionId == regionId && otherPlot.FenceNorth))
                        {
                            sides++;
                        }
                    }
                    else
                    {
                        sides++;
                    }
                }
                if(currentPlot.FenceSouth)
                {
                    if (x > minX)
                    {
                        Plot otherPlot = _plots[y][x - 1];
                        if (!(otherPlot.RegionId == regionId && otherPlot.FenceSouth))
                        {
                            sides++;
                        }
                    }
                    else
                    {
                        sides++;
                    }
                }
                if(currentPlot.FenceEast)
                {
                    if(y > minY)
                    {
                        Plot otherPlot = _plots[y - 1][x];
                        if (!(otherPlot.RegionId == regionId && otherPlot.FenceEast))
                        {
                            sides++;
                        }
                    }
                    else
                    {
                        sides++;
                    }
                }
                if (currentPlot.FenceWest)
                {
                    if (y > minY)
                    {
                        Plot otherPlot = _plots[y - 1][x];
                        if (!(otherPlot.RegionId == regionId && otherPlot.FenceWest))
                        {
                            sides++;
                        }
                    }
                    else
                    {
                        sides++;
                    }
                }
            }
        }
    }
    return sides;
}

var groups = _plots.SelectMany(row => row).GroupBy(p => p.RegionId);
foreach (var group in groups) 
{
    Console.WriteLine($"Region with {group.First().PlotType} has area {group.Count()} and sides {CalculateSides(group.Key, group)}");
}
Console.WriteLine(groups.Sum(g => g.Count() * CalculateSides(g.Key, g)));
Console.WriteLine((DateTime.Now - now).TotalMilliseconds);