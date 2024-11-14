using Day08Part2;

string[] lines = File.ReadAllLines("input.txt");
InstructionIterator iterator = new InstructionIterator(lines[0]);

foreach (string line in lines.Skip(2))
{
    Node.CreateNodeFromString(line);
}
Node.ApplyAllSubNodes();

Node[] startNodes = Node.GetAllNodesEndingWith('A');
Node[] currentNodes = startNodes;
Dictionary<string, long> destinationNodesEntries = new Dictionary<string, long>();
long numSteps = 0;
while (destinationNodesEntries.Count < 6)
{
    Func<Node, Node> nodeGetter = n => throw new Exception("No getter set");
    numSteps++;
    switch (iterator.Next())
    {
        case 'L':
            nodeGetter = n => n.Left ?? throw new Exception("Shit");
            break;
        case 'R':
            nodeGetter = n => n.Right ?? throw new Exception("Shit");
            break;
    }
    currentNodes = currentNodes.Select(n => nodeGetter(n)).ToArray();
    foreach (Node node in currentNodes.Where(n => n.Id.Last() == 'Z'))
    {
        destinationNodesEntries[node.Id] = numSteps;
    }
    currentNodes = currentNodes.Where(n => n.Id.Last() != 'Z').ToArray();
}
List<long> allRecurranceSizes = destinationNodesEntries.Values.OrderBy(l => l).ToList();
Console.WriteLine(findLCM(allRecurranceSizes));

long findGCD(long a, long b)
{
    if (b == 0)
    {
        return a;
    }
    return findGCD(b, a % b);
}

long findLCM(List<long> numbers)
{
    long result = numbers[0];
    for (int i = 1; i < numbers.Count; i++)
    {
        result = numbers[i] * result / findGCD(numbers[i], result);
    }
    return result;
}