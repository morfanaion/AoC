// See https://aka.ms/new-console-template for more information
using Day24Part2;

List<Line> hailstones = File.ReadAllLines("input.txt").Select(Line.FromDefinition).ToList();
IEnumerable<VectorOption> options = GenerateOptions(hailstones[0], hailstones[1]);
foreach (Line hailstone in hailstones.Skip(2))
{
    options = FilterVectorOptions(hailstone, options);
}

VectorOption? theOption = options.FirstOrDefault();
if (theOption == null)
{
    Console.WriteLine("No solution found");
}
else
{
    Console.WriteLine($"Point found: {theOption.Line}");
    Console.WriteLine(theOption.Line.StartPoint.AddAllDimensions());
}

#region Generation and filtering IEnumerable
IEnumerable<VectorOption> GenerateOptions(Line line1, Line line2)
{
    for (int i = -500; i < 500; i++)
    {
        for (int j = -500; j < 500; j++)
        {
            VectorOption? option = FindVectorOptionFor(line1, line2, i, j);
            if (option != null)
            {
                yield return option;
            }
        }
    }
}

IEnumerable<VectorOption> FilterVectorOptions(Line hailstone, IEnumerable<VectorOption> vectorOptions)
{
    foreach (VectorOption option in vectorOptions)
    {
        (long time, Point point)? test = hailstone.Intersect(option.Line);
        if (test.HasValue && test.Value.time >= 0)
        {
            yield return option;
        }
    }
}
#endregion

#region T1 and T2 calculations
VectorOption? FindVectorOptionFor(Line line1, Line line2, int dx3, int dy3)
{
    long min = 0;
    long max = 1000000000000;
    long ycheck = long.MaxValue;
    long xcheck = long.MaxValue;
    long t1 = long.MaxValue;
    long t2 = long.MaxValue;
    while (ycheck != 0 && min != max)
    {
        long x1 = line1.StartPoint.X;
        long dx1 = line1.Direction.X;
        long x2 = line2.StartPoint.X;
        long dx2 = line2.Direction.X;
        long y1 = line1.StartPoint.Y;
        long dy1 = line1.Direction.Y;
        long y2 = line2.StartPoint.Y;
        long dy2 = line2.Direction.Y;
        t1 = ((max - min) / 2) + min;

        // Find t2
        t2 = FindT2(x1, dx1, x2, dx2, dx3, t1);
        ycheck = (y2 + t2 * dy2) - (y1 + t1 * dy1 + (t2 - t1) * dy3);
        if (ycheck == 0)
        {
            xcheck = (x2 + t2 * dx2) - (x1 + t1 * dx1 + (t2 - t1) * dx3);
            if (xcheck == 0)
            {
                break;
            }
        }
        else if (ycheck > 0)
        {
            min = t1;
        }
        else
        {
            max = t1;
        }
        if (max - min == 1)
        {
            t1 = min;
            t2 = FindT2(x1, dx1, x2, dx2, dx3, t1);
            ycheck = (y2 + t2 * dy2) - (y1 + t1 * dy1 + (t2 - t1) * dy3);
            if (ycheck == 0)
            {
                xcheck = (x2 + t2 * dx2) - (x1 + t1 * dx1 + (t2 - t1) * dx3);
                if (xcheck == 0)
                {
                    break;
                }
            }
            t1 = max;
            t2 = FindT2(x1, dx1, x2, dx2, dx3, t1);
            ycheck = (y2 + t2 * dy2) - (y1 + t1 * dy1 + (t2 - t1) * dy3);
            xcheck = (x2 + t2 * dx2) - (x1 + t1 * dx1 + (t2 - t1) * dx3);
            break;
        }
    }
    if (ycheck == 0 && xcheck == 0)
    {
        return new VectorOption(line1.StartPoint + line1.Direction * t1, line2.StartPoint + line2.Direction * t2, t1, t2);
    }
    else
    {
        return null;
    }
}

long FindT2(long x1, long dx1, long x2, long dx2, long dx3, long t1)
{
    long numerator = (x1 - x2) + t1 * dx1 - t1 * dx3;
    long denominator = dx2 - dx3;

    if (denominator == 0)
    {
        return -1;
    }

    long t2 = numerator / denominator;

    return t2;
}
#endregion
