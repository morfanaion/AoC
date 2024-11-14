// See https://aka.ms/new-console-template for more information
using Day08Puzzle02;

List<List<Tree>> forest = File.ReadAllLines("Input.txt").Select(line => line.Select(c => new Tree() { Height = int.Parse(c.ToString()) }).ToList()).ToList();

int forestWidth = forest.Count;
int forestHeight = 0;
if (forestWidth != 0)
{
    forestHeight = forest[0].Count;
}

for (int x = 0; x < forest.Count; x++)
{
    for (int y = 0; y < forest[x].Count; y++)
    {
        Tree currentTree = forest[x][y];
        int scoreUp = 0;
        int scoreDown = 0;
        int scoreLeft = 0;
        int scoreRight = 0;
        for (int x2 = x - 1; x2 >= 0; x2--)
        {
            scoreUp++;
            if(forest[x2][y].Height >= currentTree.Height)
            {
                break;
            }
        }
        for (int x2 = x + 1; x2 < forestWidth; x2++)
        {
            scoreDown++;
            if (forest[x2][y].Height >= currentTree.Height)
            {
                break;
            }
        }
        for (int y2 = y - 1; y2 >= 0; y2--)
        {
            scoreLeft++;
            if (forest[x][y2].Height >= currentTree.Height)
            {
                break;
            }
        }
        for (int y2 = y + 1; y2 < forestWidth; y2++)
        {
            scoreRight++;
            if (forest[x][y2].Height >= currentTree.Height)
            {
                break;
            }
        }
        currentTree.Score = scoreUp * scoreDown * scoreLeft * scoreRight;
    }
}
Console.WriteLine(forest.SelectMany(column => column).Max(tree => tree.Score));