
using _2025_12_01;
using System.Text.RegularExpressions;

Regex treeRegex = RegexHelper.TreeRegex();
Regex packageIdentifierRegex = RegexHelper.PackageIdentifierRegex();

List<Tree> trees = new List<Tree>();
Dictionary<int, Package> packages = new Dictionary<int, Package>();
Package package = new Package();
List<string>? currentPackage = null;
int currentIdentifier = 0;
foreach (string str in File.ReadAllLines("input.txt"))
{
    Match packageIdentifierMatch = packageIdentifierRegex.Match(str);
    if(packageIdentifierMatch.Success)
    {
        currentIdentifier = int.Parse(packageIdentifierMatch.Groups["identifier"].Value);
        currentPackage = new List<string>();
        continue;
    }
    Match treeMatch = treeRegex.Match(str);
    if(treeMatch.Success)
    {
        trees.Add(new Tree()
        {
            X = int.Parse(treeMatch.Groups["x"].Value),
            Y = int.Parse(treeMatch.Groups["y"].Value),
            NumPresentsPerType =
            [
                int.Parse(treeMatch.Groups["n1"].Value),
                int.Parse(treeMatch.Groups["n2"].Value),
                int.Parse(treeMatch.Groups["n3"].Value),
                int.Parse(treeMatch.Groups["n4"].Value),
                int.Parse(treeMatch.Groups["n5"].Value),
                int.Parse(treeMatch.Groups["n6"].Value)
            ]
        });
    }

    if(currentPackage is not null)
    {
        currentPackage.Add(str);
        if(currentPackage.Count == 3)
        {
            packages.Add(currentIdentifier, new Package()
            {
                Id = currentIdentifier,
                Grid = currentPackage.Select(line => line.ToCharArray()).ToArray()
            });
        }
    }
}
List<Tree> treesThatAlwaysFit = new List<Tree>();
List<Tree> treesThatNeedPuzzling = new List<Tree>();
List<Tree> treesThatCannotFit = new List<Tree>();
foreach (Tree tree in trees)
{
    if (tree.X / 3 * (tree.Y / 3) >= tree.NumPresentsPerType.Sum())
    {
        treesThatAlwaysFit.Add(tree);
    }
    else if (tree.X * tree.Y < GetMinimumSquaresNeeded(tree))
    {
        treesThatCannotFit.Add(tree);
    }
    else
    {
        treesThatNeedPuzzling.Add(tree);
    }
}

int GetMinimumSquaresNeeded(Tree tree)
{
    int totalRequired = 0;
    for(int i = 0; i < tree.NumPresentsPerType.Length; i++)
    {
        totalRequired += tree.NumPresentsPerType[i] * packages[i].TotalSize;
    }
    return totalRequired;
}

Console.WriteLine($"{treesThatAlwaysFit.Count} will fit no matter what");
Console.WriteLine($"{treesThatCannotFit.Count} will never fit");
Console.WriteLine($"{treesThatNeedPuzzling.Count} will need puzzling");