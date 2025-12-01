int currentPos = 50;
int numTimesAt0 = 0;
foreach(string line in File.ReadAllLines("input.txt"))
{
    Func<int, int, int> movePosition = line[0] switch
    {
        'L' => MoveLeft,
        'R' => MoveRight,
        _ => throw new InvalidOperationException("Invalid instruction")
    };
    currentPos = movePosition(currentPos, int.Parse(line.Substring(1)));
    if(currentPos == 0)
    {
        numTimesAt0++;
    }
}
Console.WriteLine(numTimesAt0);

int MoveLeft(int position, int numPositions) => TrimRound(position - numPositions);

int MoveRight(int position, int numPositions) => TrimRound(position + numPositions);

int TrimRound(int position)
{
    while(position < 0)
    {
        position += 100;
    }
    while(position >= 100)
    {
        position -= 100;
    }
    return position;
}