int currentPos = 50;
int numTimesAt0 = 0;
foreach (string line in File.ReadAllLines("input.txt"))
{
    Func<int, int, int> movePosition = line[0] switch
    {
        'L' => MoveLeft,
        'R' => MoveRight,
        _ => throw new InvalidOperationException("Invalid instruction")
    };
    int numClicks = int.Parse(line.Substring(1));
    numTimesAt0 += numClicks / 100;
    numClicks %= 100;
    int newPos = movePosition(currentPos, numClicks);
    if (currentPos != 0)
    {
        if (newPos == 0)
        {
            numTimesAt0++;
        }
        else if (newPos < 0)
        {
            newPos += 100;
            numTimesAt0++;
        }
        else if (newPos >= 100)
        {
            newPos -= 100;
            numTimesAt0++;
        }
    }
    else
    {
        if (newPos < 0)
        {
            newPos += 100;
        }
        else if (newPos >= 100)
        {
            newPos -= 100;
        }
    }
    currentPos = newPos;
}
Console.WriteLine(numTimesAt0);

int MoveLeft(int position, int numPositions) => position - numPositions;

int MoveRight(int position, int numPositions) => position + numPositions;

