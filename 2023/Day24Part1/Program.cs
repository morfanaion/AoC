// See https://aka.ms/new-console-template for more information
using Day24Part1;
#if TEST
const double Min = 7;
const double Max = 27;
#else
const double Min = 200000000000000;
const double Max = 400000000000000;

#endif

List<Line> hailstones = File.ReadAllLines("input.txt").Select(Line.FromDefinition).ToList();

long collisions = 0;
foreach(Line hailstone in hailstones)
{
    foreach(Line other in hailstones.Where(h => h.Id > hailstone.Id))
    {
        Point? point = hailstone.FindIntersection(other);
        if(point != null)
        {
            Console.Write($"Hailstones {hailstone.Id} and {other.Id} collide at {point}, ");
            if (point.X >= Min && point.X <= Max &&
                point.Y >= Min && point.Y <= Max)
            {
                Console.WriteLine("count it");
                collisions++;
            }
            else
            {
                Console.WriteLine("Don't count it");
            }
        }
        else
        {
            Console.WriteLine($"Hailstones {hailstone.Id} and {other.Id} don't collide");
        }
    }
}
Console.WriteLine(collisions);