// See https://aka.ms/new-console-template for more information
using Day17Puzzle01;

string input = File.ReadAllText("Input.txt");
List<List<char>> TheBox = new List<List<char>>();
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
TheBox.Add(new List<char>() { '╚', '═', '═', '═', '═', '═', '═', '═', '╝' });
for (int i = 0; i < 7; i++)
{
    TheBox.Add(new List<char>() { '║', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '║' });
}
int cycle = 0;
for (int i = 0; i < 2022; i++)
{
    Shape currentShape = shapeQueue.Dequeue();
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
    shapeQueue.Enqueue(currentShape);
}

for (int i = TheBox.Count - 1; i >= 0; i--)
{
    Console.WriteLine(String.Concat(TheBox[i]));
}
Console.WriteLine(TheBox.FindIndex(l => l.All(c => c != '█' && c != '═')) - 1);

