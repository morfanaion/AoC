using _2025_08_01;

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
for(int l = 0; l < 1000;  l++)
{
    (JunctionBox box1, JunctionBox box2, _) = allPairs[l];
    box1.Circuit.MergeWith(box2.Circuit);
}
long sizeMultiplication = 1;
foreach(var circuit in junctionBoxes.Select(j => j.Circuit).Distinct().OrderByDescending(c => c.JunctionBoxes.Count).Take(3))
{
    sizeMultiplication *= circuit.JunctionBoxes.Count;
}
Console.WriteLine(sizeMultiplication);