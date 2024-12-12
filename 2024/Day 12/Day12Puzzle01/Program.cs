using Day12Puzzle01;

Plot[][] plots = File.ReadAllLines("input.txt").Select(line => line.Select(c => new Plot() { PlotType = c }).ToArray()).ToArray();
for(int y = 0; y < plots.Length; y++)
{
    for(int x = 0;  x < plots[y].Length; x++)
    {
        Plot currentPlot = plots[y][x];
        if(x > 0)
        {
            Plot otherPlot = plots[y][x - 1];
            if(currentPlot.PlotType == otherPlot.PlotType)
            {
                currentPlot.Perimeter--;
                MergeRegions(otherPlot.RegionId, currentPlot.RegionId);
            }
        }
        if (y > 0)
        {
            Plot otherPlot = plots[y - 1][x];
            if (currentPlot.PlotType == otherPlot.PlotType)
            {
                currentPlot.Perimeter--;
                MergeRegions(otherPlot.RegionId, currentPlot.RegionId);
            }
        }
        if (x + 1 < plots[y].Length)
        {
            Plot otherPlot = plots[y][x + 1];
            if (currentPlot.PlotType == otherPlot.PlotType)
            {
                currentPlot.Perimeter--;
                MergeRegions(currentPlot.RegionId, otherPlot.RegionId);
            }
        }
        if (y + 1 < plots.Length)
        {
            Plot otherPlot = plots[y + 1][x];
            if (currentPlot.PlotType == otherPlot.PlotType)
            {
                currentPlot.Perimeter--;
                MergeRegions(currentPlot.RegionId, otherPlot.RegionId);
            }
        }
    }
}

void MergeRegions(long region1, long region2)
{
    foreach(Plot plot in plots.SelectMany(row => row).Where(p => p.RegionId == region2))
    {
        plot.RegionId = region1;
    }
}

var groups = plots.SelectMany(row => row).GroupBy(p => p.RegionId);
foreach (var group in groups) 
{
    Console.WriteLine($"Region with {group.First().PlotType} has area {group.Count()} and perimiter {group.Sum(p => p.Perimeter)}");
}
Console.WriteLine(groups.Sum(g => g.Count() * g.Sum(p => p.Perimeter)));