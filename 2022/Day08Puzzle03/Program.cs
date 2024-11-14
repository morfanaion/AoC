// See https://aka.ms/new-console-template for more information
using Day08Puzzle03;

List<Tree> trees = new List<Tree>();
Tree[]? previousLine = null;
int lineCounter = 0;
foreach(string line in File.ReadAllLines("Input.txt"))
{
    Tree[] currentLine = line.Select(c => new Tree() { Height = int.Parse(c.ToString()) }).ToArray();
    trees.AddRange(currentLine);
    for(int x = 0; x < currentLine.Length; x++)
    {
        Tree currentTree = currentLine[x];
        currentTree.X = x;
        currentTree.Y = lineCounter;
        if (x > 0)
        {
            Tree west = currentLine[x - 1];
            currentTree.West = west;
            west.East = currentTree;
        }
        if(previousLine != null)
        {
            Tree north = previousLine[x];
            north.South = currentTree;
            currentTree.North = north;
        }
    }
    previousLine = currentLine;
    lineCounter++;
}

Console.WriteLine(trees.Count(t => t.Visible));
Console.WriteLine(trees.Max(t => t.Score));
