// See https://aka.ms/new-console-template for more information

using Day22Part2;

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

long result = 0;
foreach(Brick brick in Brick.AllBricks)
{
    foreach(Brick initBrick in Brick.AllBricks)
    {
        initBrick.Disintegrated = false;
    }
    brick.Disintegrated = true;
    foreach(Brick otherBrick in Brick.AllBricks.OrderBy(b => b.MinZ))
    {
        if(otherBrick.Disintegrated)
        {
            continue;
        }
        otherBrick.CheckDisintegration();
    }
    result += (Brick.AllBricks.Count(b => b.Disintegrated)) - 1;
}
Console.WriteLine(result);
