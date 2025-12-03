ulong result = 0;
const int numBatteriesToTurnOn = 12;
foreach(string line in File.ReadAllLines("input.txt"))
{
    string turnedOnBatteries = string.Empty;
    string lineRemaining = line;
    while(turnedOnBatteries.Length != numBatteriesToTurnOn)
    {
        char highest = lineRemaining.Substring(0, lineRemaining.Length - (numBatteriesToTurnOn - 1 - turnedOnBatteries.Length)).Max();
        int idx = lineRemaining.IndexOf(highest);
        lineRemaining = lineRemaining.Substring(idx + 1);
        turnedOnBatteries += highest;
    }
    result += ulong.Parse(turnedOnBatteries);
}
Console.WriteLine(result);
