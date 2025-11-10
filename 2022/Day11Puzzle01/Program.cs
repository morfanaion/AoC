// See https://aka.ms/new-console-template for more information

using Day11Puzzle01;

Monkey? currentMonkey = null;
foreach(string line in File.ReadAllLines("Input.txt"))
{
    if (line.StartsWith("Monkey"))
    {
        string numberSection = line.Split(" ")[1];
        currentMonkey = new Monkey() { Id = int.Parse(numberSection.Substring(0, numberSection.Length - 1)) };
        Monkey.MonkeyList.Add(currentMonkey);
    }
    else if(line.Trim().StartsWith("Starting items"))
    {
        if (currentMonkey == null) continue;
        string numbersSection = line.Split(":")[1];
        foreach(string num in numbersSection.Split(","))
        {
            currentMonkey.ItemList.Enqueue(int.Parse(num.Trim()));
        }
    }
    else if(line.Trim().StartsWith("Operation"))
    {
        if (currentMonkey == null) continue;
        string operationString = line.Split(": ")[1];
        Match match = Regex.Match(operationString, "new = old (?<operator>[*+]) (?<parameter>.*)");
        switch(match.Groups["operator"].Value)
        {
            case "*":
                currentMonkey.Operation = Operation.Multiply;
                break;
            case "+":
                currentMonkey.Operation = Operation.Add;
                break;
        }
        if(match.Groups["parameter"].Value == "old")
        {
            currentMonkey.OperationParameter = -1;
        }
        else
        {
            currentMonkey.OperationParameter = int.Parse(match.Groups["parameter"].Value);
        }
    }
    else if(line.Trim().StartsWith( "Test:"))
    {
        if (currentMonkey == null) continue;
        currentMonkey.TestDivisor = int.Parse(line.Trim().Split(" ")[3]);
    }
    else if(line.Trim().StartsWith("If true"))
    {
        if (currentMonkey == null) continue;
        currentMonkey.IdForTrue = int.Parse(line.Trim().Split(" ")[5]);
    }
    else if (line.Trim().StartsWith("If false"))
    {
        if (currentMonkey == null) continue;
        currentMonkey.IdForFalse = int.Parse(line.Trim().Split(" ")[5]);
    }
}
for (int i = 0; i < 20; i++)
{
    PerformRound();
}
int[] twoHighest = Monkey.MonkeyList.OrderByDescending(monkey => monkey.NumInspections).Take(2).Select(monkey => monkey.NumInspections).ToArray();
Console.WriteLine(twoHighest[0] * twoHighest[1]);

void PerformRound()
{
    foreach(Monkey monkey in Monkey.MonkeyList.OrderBy(m => m.Id))
    {
        monkey.HandleTurn();
    }
}