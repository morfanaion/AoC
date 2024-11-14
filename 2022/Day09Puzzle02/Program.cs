using Day09Puzzle02;

List<Position> tailPositionHistory = new List<Position>();
Rope theRope = new Rope(10, 0, 0);
tailPositionHistory.Add(new Position(theRope.TailPosition));

foreach (string instruction in File.ReadAllLines("Input.txt"))
{
    Action incrementAction = () => { };
    string[] instructionParts = instruction.Split(' ');
    switch (instructionParts[0])
    {
        case "R":
            incrementAction = theRope.MoveRight;
            break;
        case "L":
            incrementAction = theRope.MoveLeft;
            break;
        case "U":
            incrementAction = theRope.MoveUp;
            break;
        case "D":
            incrementAction = theRope.MoveDown;
            break;
    }
    int numTimes = int.Parse(instructionParts[1]);
    for (int i = 0; i < numTimes; i++)
    {
        incrementAction();
        tailPositionHistory.Add(new Position(theRope.TailPosition));
    }
}

Console.WriteLine(tailPositionHistory.Distinct().Count());