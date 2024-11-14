// See https://aka.ms/new-console-template for more information
using Day08Puzzle01;

List<List<Tree>> forest = File.ReadAllLines("Input.txt").Select(line => line.Select(c => new Tree() { Height = byte.Parse(c.ToString()) }).ToList()).ToList();

int forestWidth = forest.Count;
int forestHeight = 0;
if(forestWidth != 0)
{
    forestHeight = forest[0].Count;
}

for(int x = 0; x < forest.Count; x++ )
{
    for(int y = 0; y < forest[x].Count; y++)
    {
        Tree currentTree = forest[x][y];
        if(x == 0 || y ==0 || x == (forestWidth - 1) || y == forestHeight - 1)
        {
            currentTree.Visible = true;
            continue;
        }
        bool visible = true;
        for(int x2 = 0; x2 < x; x2++)
        {
            if(forest[x2][y].Height >= currentTree.Height)
            {
                visible = false;
                break;
            }
        }
        if(visible)
        {
            currentTree.Visible = true;
            continue;
        }
        visible = true;
        for (int x2 = x + 1; x2 < forestWidth; x2++)
        {
            if (forest[x2][y].Height >= currentTree.Height)
            {
                visible = false;
                break;
            }
        }
        if (visible)
        {
            currentTree.Visible = true;
            continue;
        }
        visible = true;
        for (int y2 = 0; y2 < y; y2++)
        {
            if (forest[x][y2].Height >= currentTree.Height)
            {
                visible = false;
                break;
            }
        }
        if (visible)
        {
            currentTree.Visible = true;
            continue;
        }
        visible = true;
        for (int y2 = y + 1; y2 < forestHeight; y2++)
        {
            if (forest[x][y2].Height >= currentTree.Height)
            {
                visible = false;
                break;
            }
        }
        if (visible)
        {
            currentTree.Visible = true;
            continue;
        }
        visible = true;

    }
}
Console.WriteLine(forest.SelectMany(column => column).Count(tree => tree.Visible));