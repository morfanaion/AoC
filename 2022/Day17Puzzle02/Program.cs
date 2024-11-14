// See https://aka.ms/new-console-template for more information
using Day17Puzzle02;

string input = File.ReadAllText("Input.txt");
List<List<char>> TheBox = new List<List<char>>();
Dictionary<Shape, List<ShapeResult>> testDict = new Dictionary<Shape, List<ShapeResult>>();
Queue<Shape> shapeQueue = new Queue<Shape>();
shapeQueue.Enqueue(new Shape(new char[][]
{
    new char[] { '█', '█', '█', '█' }
}));
shapeQueue.Enqueue(new Shape(new char[][]
{
    new char[] { ' ', '█', ' ' },
    new char[] { '█', '█', '█' },
    new char[] { ' ', '█', ' ' }
})
{ Id = 1});
shapeQueue.Enqueue(new Shape(new char[][]
{
    new char[] { '█', '█', '█' },
    new char[] { ' ', ' ', '█' },
    new char[] { ' ', ' ', '█' }
    
}));
shapeQueue.Enqueue(new Shape(new char[][]
{
    new char[] { '█' },
    new char[] { '█' },
    new char[] { '█' },
    new char[] { '█' }
}));
shapeQueue.Enqueue(new Shape(new char[][]
{
    new char[] { '█', '█' },
    new char[] { '█', '█' }
}));
foreach(Shape s in shapeQueue)
{
    testDict.Add(s, new List<ShapeResult>());
}

TheBox.Add(new List<char>() { '╚', '═', '═', '═', '═', '═', '═', '═', '╝' });
for (int i = 0; i < 7; i++)
{
    TheBox.Add(new List<char>() { '║', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '║' });
}
int cycle = 0;
const long numBlockToThrow = 1000000000000;
long toAddForProsperity = 0;
for (long i = 0; i < numBlockToThrow; i++)
{
    Shape currentShape = shapeQueue.Dequeue();
    int cycleStart = cycle;
    currentShape.PosX = 3;
    currentShape.PosY = TheBox.FindIndex(l => l.All(c => c != '█' && c != '═')) + 3;
    for(int j = TheBox.Count; j <= currentShape.PosY + 4; j++)
    {
        TheBox.Add(new List<char>() { '║', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '║' });
    }
    bool hasMovedDown = true;
    while (hasMovedDown)
    {
        switch(input[cycle])
        {
            case '<':
                if(currentShape.CanMove(TheBox, Direction.Left))
                {
                    currentShape.PosX--;
                }
                break;
            case '>':
                if (currentShape.CanMove(TheBox, Direction.Right))
                {
                    currentShape.PosX++;
                }
                break;
        }
        cycle = (cycle + 1) % input.Length;
        if(currentShape.CanMove(TheBox, Direction.Down))
        {
            currentShape.PosY--;
        }
        else
        {
            hasMovedDown = false;
        }
    }
    currentShape.Place(TheBox);
    testDict[currentShape].Add(new ShapeResult() { PosY = currentShape.PosY, BlockNo = i, CycleId = cycleStart });
    List<ShapeResult> currentShapeResults = testDict[currentShape];
    if(toAddForProsperity == 0 && currentShapeResults.GroupBy(s => s.CycleId).Max(g => g.Count()) > 10)
    {
        List<int> repeatPattern = GetRepeatPattern(currentShapeResults.Select(sr => sr.CycleId));
        int numTimesPatternRepeated = currentShapeResults.Count / repeatPattern.Count;
        List<ShapeResult> compiledResults = Enumerable.Reverse(currentShapeResults).Take(repeatPattern.Count * numTimesPatternRepeated).ToList();
        Dictionary<int, List<int>> posyDictionary = new Dictionary<int, List<int>>();
        Dictionary<int, List<long>> blockIdDictionary = new Dictionary<int, List<long>>();
        for (int idx = 0; idx < repeatPattern.Count; idx++)
        {
            posyDictionary.Add(idx, new List<int>());
            blockIdDictionary.Add(idx, new List<long>());
        }
        for(int p = 0; p < compiledResults.Count; p++)
        {
            ShapeResult compiledResult = compiledResults[p];
            if(compiledResult.CycleId != repeatPattern[p % repeatPattern.Count])
            {
                Console.WriteLine("Eek!!! WHAT?");
            }
            posyDictionary[p % repeatPattern.Count].Add(compiledResult.PosY);
            blockIdDictionary[p % repeatPattern.Count].Add(compiledResult.BlockNo);
        }
        int posYDifferenceForPattern = 0;
        if(posyDictionary.Count != 0)
        {
            List<int> differences = GetDifferences(posyDictionary.First().Value.OrderBy(v => v)).ToList();
            posYDifferenceForPattern = differences.Max();
        }
        long blockIdDifferenceForPattern = 0;
        if(blockIdDictionary.Count != 0)
        {
            List<long> differences = GetDifferencesLong(blockIdDictionary.First().Value.OrderBy(v => v)).ToList();
            blockIdDifferenceForPattern = differences.Max();
        }
        long numPatternsToAdd = (numBlockToThrow - i) / blockIdDifferenceForPattern;
        toAddForProsperity = numPatternsToAdd * posYDifferenceForPattern;
        i += numPatternsToAdd * blockIdDifferenceForPattern;
    }

    shapeQueue.Enqueue(currentShape);
}

Console.WriteLine(toAddForProsperity + ((long)TheBox.FindIndex(l => l.All(c => c != '█' && c != '═'))) - 1);

List<int> GetRepeatPattern(IEnumerable<int> test)
{
    List<int> inverted = test.Reverse().ToList();
    List<int> repeatedPattern = new List<int>();

    while (inverted.Count > 0)
    {
        repeatedPattern.Add(inverted[0]);
        inverted.RemoveAt(0);
        bool patternfailed = false;
        for(int i = 0; i < (inverted.Count - repeatedPattern.Count); i += repeatedPattern.Count)
        {
            for(int j = 0; j < repeatedPattern.Count; j++)
            {
                if(inverted[i + j] != repeatedPattern[j])
                {
                    patternfailed = true;
                }
            }
        }
        if(!patternfailed)
        {
            return repeatedPattern;
        }
    }
    return new List<int>();
}

IEnumerable<int> GetDifferences(IEnumerable<int> test)
{
    int? test3 = null;
    foreach(int test2 in test)
    {
        if(test3.HasValue)
        {
            yield return test2 - test3.Value;
        }
        test3 = test2;
    }
}

IEnumerable<long> GetDifferencesLong(IEnumerable<long> test)
{
    long? test3 = null;
    foreach (long test2 in test)
    {
        if (test3.HasValue)
        {
            yield return test2 - test3.Value;
        }
        test3 = test2;
    }
}