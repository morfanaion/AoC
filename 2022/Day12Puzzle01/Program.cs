// See https://aka.ms/new-console-template for more information
using Day12Puzzle01;

Node[][] grid = File.ReadAllLines("Input.txt").Select(line => line.Select(c => new Node(c)).ToArray()).ToArray();
int sizeX = grid.Length;
int sizeY = 0;
if(sizeX > 0)
{
    sizeY = grid[0].Length;
}
for(int x = 0; x < sizeX; x++)
{
    for(int y = 0; y < sizeY; y++)
    {
        Node currentNode = grid[x][y];
        if (x > 0 && CheckNodes(currentNode, grid[x -1][y]))
        {
            currentNode.ConnectedNodes.Add(grid[x - 1][y]);
        }
        if (x + 1 < sizeX && CheckNodes(currentNode, grid[x + 1][y]))
        {
            currentNode.ConnectedNodes.Add(grid[x + 1][y]);
        }
        if (y > 0 && CheckNodes(currentNode, grid[x][y - 1]))
        {
            currentNode.ConnectedNodes.Add(grid[x][y - 1]);
        }
        if (y + 1 < sizeY && CheckNodes(currentNode, grid[x][y + 1]))
        {
            currentNode.ConnectedNodes.Add(grid[x][y + 1]);
        }
    }
}

PriorityQueue<Node, int> prioQueue = new PriorityQueue<Node, int>();
Node startNode = grid.SelectMany(row => row).Single(node => node.IsStartPoint);
startNode.NumStepsRequired = 0;
prioQueue.Enqueue(startNode, startNode.NumStepsRequired);
while(prioQueue.Count > 0)
{
    Node currentNode = prioQueue.Dequeue();
    foreach(Node connectedNode in currentNode.ConnectedNodes)
    {
        if(connectedNode.NumStepsRequired == -1)
        {
            connectedNode.NumStepsRequired = currentNode.NumStepsRequired + 1;
            prioQueue.Enqueue(connectedNode, connectedNode.NumStepsRequired);
        }
    }
}
Console.WriteLine(grid.SelectMany(row=>row).Any(node => node.NumStepsRequired == -1));
Node endNode = grid.SelectMany(row => row).Single(node => node.IsEndPoint);
Console.WriteLine(endNode.NumStepsRequired);



bool CheckNodes(Node currentNode, Node node) => currentNode.Elevation + 1 >= node.Elevation;