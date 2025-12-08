using _2025_08_02;

JunctionBox[] junctionBoxes = File.ReadAllLines("input.txt").Select(JunctionBox.FromString).ToArray();
List<(JunctionBox box1, JunctionBox box2, long squaredDistance)> allPairs = new List<(JunctionBox box1, JunctionBox box2, long squaredDistance)>();
for(int i = 0 ; i < junctionBoxes.Length - 1; i++)
{
    for(int j = i + 1;  j < junctionBoxes.Length; j++)
    {
        JunctionBox box1 = junctionBoxes[i];
        JunctionBox box2 = junctionBoxes[j];
        allPairs.Add((box1, box2, box1.SquaredDistanceTo(box2)));
    }
}
allPairs = allPairs.OrderBy(p => p.squaredDistance).ToList();
bool areMultiple = junctionBoxes.Select(j => j.Circuit).Distinct().Skip(1).Any();
long xMultiplication = 0;
for (int l = 0; l < allPairs.Count; l++)
{
    (JunctionBox box1, JunctionBox box2, _) = allPairs[l];
    box1.Circuit!.MergeWith(box2.Circuit!);
    areMultiple = junctionBoxes.Select(j => j.Circuit).Distinct().Skip(1).Any();
    if(!areMultiple)
    {
        xMultiplication = box1.X * box2.X;
        break;
    }
}
Console.WriteLine(xMultiplication);