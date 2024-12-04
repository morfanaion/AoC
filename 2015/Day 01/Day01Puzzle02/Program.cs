int currentFloor = 0;
int idx = 0;
foreach(char c in File.ReadAllText("input.txt"))
{
    idx++;
    switch (c)
    {
        case '(':
            currentFloor++;
            break;
        case ')':
            currentFloor--;
            break;
    }
    if (currentFloor < 0)
    {
        break;
    }
}
Console.WriteLine(idx);  