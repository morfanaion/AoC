using Day08Part1;

string[] lines = File.ReadAllLines("input.txt");
InstructionIterator iterator = new InstructionIterator(lines[0]);

foreach(string line in lines.Skip(2))
{
    Node.CreateNodeFromString(line);
}
Node.ApplyAllSubNodes();

Node startNode = Node.GetNodeByName("AAA");
Node currentNode = startNode;
int numSteps = 0; ;
while(currentNode.Id != "ZZZ")
{
    numSteps ++;
    switch(iterator.Next())
    {
        case 'L':
            currentNode = currentNode.Left ?? throw new Exception("Shit");
            break;
        case 'R':
            currentNode = currentNode.Right ?? throw new Exception("Shit");
            break;
    }
}
Console.WriteLine(numSteps);