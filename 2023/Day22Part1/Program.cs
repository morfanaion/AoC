// See https://aka.ms/new-console-template for more information

using Day22Part1;

Brick.AllBricks.AddRange(File.ReadAllLines("input.txt").Select(l => new Brick(l)));
foreach(Brick currentBrick in Brick.AllBricks.OrderBy(b => b.MinZ))
{
    foreach (Brick otherBrick in Brick.AllBricks.Where(brick => brick.Settled).OrderByDescending(brick => brick.MaxZ))
    {
        if(otherBrick.WillSupport(currentBrick))
        {
            currentBrick.SettleAtZ(otherBrick.MaxZ + 1);
            break;
        }
    }
    if(!currentBrick.Settled)
    {
        currentBrick.SettleAtZ(1);
    }
}
foreach(Brick brick in Brick.AllBricks)
{
    brick.DetermineSupports();
}

Console.WriteLine(Brick.AllBricks.Count(b => !Brick.AllBricks.Any(o => o.SupportedBy.Count == 1 && o.SupportedBy[0] == b)));
