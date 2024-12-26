using Day24Puzzle02;
using AOC.Maths;
using System.Text.RegularExpressions;

Dictionary<string, List<Action<Gate>>> queuedGateActions = new Dictionary<string, List<Action<Gate>>>();
Action<string> processLine = line =>
{
    if (!string.IsNullOrEmpty(line))
    {
        Match staticMatch = RegExHelper.GetStaticGateRegex().Match(line);
        Gate.Gates.Add(new StaticGate()
        {
            Id = staticMatch.Groups["gateId"].Value,
            Input = staticMatch.Groups["output"].Value == "1",
        });
    }
    else
    {
        processLine = line =>
        {
            Match nonStaticMatch = RegExHelper.GetNonStaticGateRegex().Match(line);
            NonStaticGate gate = nonStaticMatch.Groups["operator"].Value switch
            {
                "XOR" => new NonStaticGate() { CompareFunction = (g1, g2) => g1.Output ^ g2.Output },
                "AND" => new NonStaticGate() { CompareFunction = (g1, g2) => g1.Output && g2.Output },
                "OR" => new NonStaticGate() { CompareFunction = (g1, g2) => g1.Output || g2.Output },
                _ => throw new InvalidDataException("Unsupported operator found")
            };
            gate.Operator = nonStaticMatch.Groups["operator"].Value;
            gate.Id = nonStaticMatch.Groups["gateId"].Value;
            if(Gate.Gates.FirstOrDefault(g => g.Id == nonStaticMatch.Groups["gate1"].Value) is Gate input1Gate)
            {
                gate.Input1 = input1Gate;
            }
            else
            {
                if(queuedGateActions.TryGetValue(nonStaticMatch.Groups["gate1"].Value, out List<Action<Gate>>? setGateList))
                {
                    setGateList.Add(g => gate.Input1 = g);
                }
                else
                {
                    queuedGateActions.Add(nonStaticMatch.Groups["gate1"].Value, new List<Action<Gate>>() { g => gate.Input1 = g });
                }
            }
            if (Gate.Gates.FirstOrDefault(g => g.Id == nonStaticMatch.Groups["gate2"].Value) is Gate input2Gate)
            {
                gate.Input2 = input2Gate;
            }
            else
            {
                if (queuedGateActions.TryGetValue(nonStaticMatch.Groups["gate2"].Value, out List<Action<Gate>>? setGateList))
                {
                    setGateList.Add(g => gate.Input2 = g);
                }
                else
                {
                    queuedGateActions.Add(nonStaticMatch.Groups["gate2"].Value, new List<Action<Gate>>() { g => gate.Input2 = g });
                }
            }
            if(queuedGateActions.TryGetValue(gate.Id, out List<Action<Gate>>? mySetGateList))
            {
                foreach(var setter in mySetGateList)
                {
                    setter(gate);
                }
            }
            Gate.Gates.Add(gate);
        };
    }
};
string[] input = File.ReadAllLines("input.txt");
foreach (string line in input)
{
    processLine(line);
}

List<StaticGate> AllXs = Gate.Gates.OfType<StaticGate>().Where(g => g.IsX).OrderBy(g => g.Index).ToList();
List<StaticGate> AllYs = Gate.Gates.OfType<StaticGate>().Where(g => g.IsY).OrderBy(g => g.Index).ToList();
List<NonStaticGate> AllZs = Gate.Gates.OfType<NonStaticGate>().Where(g => g.IsZ).OrderBy(g => g.Index).ToList();
IEnumerable<NonStaticGate> allFaultedZs = GetAllFaultedZs();
List<(NonStaticGate, NonStaticGate)> swappedGates = new List<(NonStaticGate, NonStaticGate)>();
DateTime start = DateTime.Now;
while (allFaultedZs.Any())
{
    NonStaticGate faultedGate = allFaultedZs.OrderBy(g => g.Index).First();
    List<Gate> fixedGates = AllZs.Where(g => g.Index < faultedGate.Index).SelectMany(g => g.Nodes).Distinct().ToList();
    bool swapDone = false;
    foreach (NonStaticGate swapGate1 in faultedGate.Nodes.Append(faultedGate).Where(g => fixedGates.All(g2 => g2.Id != g.Id)).OfType<NonStaticGate>().Distinct())
    {
        foreach (NonStaticGate swapGate2 in Gate.Gates.OfType<NonStaticGate>().Where(g => fixedGates.All(g2 => g2.Id != g.Id) && g.Nodes.OfType<StaticGate>().All(g => g.Index <= faultedGate.Index)))
        {
            Gate.ValidResult = true;
            SwapGates(swapGate1, swapGate2);
            if (!GetAllFaultedZs().Any(g => g.Index <= faultedGate.Index) && Gate.ValidResult)
            {
                // this swap causes all zs till now to correct themselves, maintain the swap, store it, move on to the next gate
                swappedGates.Add((swapGate1, swapGate2));
                allFaultedZs = GetAllFaultedZs();
                swapDone = true;
                break;
            }
            SwapGates(swapGate1, swapGate2);
        }
        if(swapDone)
        {
            break;
        }
    }
}
Console.WriteLine(string.Join(",", swappedGates.SelectMany(p => new string[] { p.Item1.Id, p.Item2.Id }).Order()));
void SwapGates(NonStaticGate gate1, NonStaticGate gate2)
{
  Func<Gate, Gate, bool> comparerHolder = gate1.CompareFunction;
  Gate input1Holder = gate1.Input1;
  Gate input2Holder = gate1.Input2;
  string op = gate1.Operator;
  gate1.CompareFunction = gate2.CompareFunction;
  gate1.Input1 = gate2.Input1;
  gate1.Input2 = gate2.Input2;
  gate1.Operator = gate2.Operator;
  gate2.CompareFunction = comparerHolder;
  gate2.Input1 = input1Holder;
  gate2.Input2 = input2Holder;
  gate2.Operator = gate1.Operator;
}

long GetXResult() => AllXs.Select(g => g.Value).Combine((v1, v2) => v1 | v2, 0);
long GetYResult() => AllYs.Select(g => g.Value).Combine((v1, v2) => v1 | v2, 0);

IEnumerable<NonStaticGate> GetAllFaultedZs()
{
    long startResultX = GetXResult();
    long startResultY = GetYResult();
    SetInputValuesAndExpectedValue(0b000000000000000000000000000000000000000000000, 0b111111111111111111111111111111111111111111111);
    IEnumerable<NonStaticGate> gatesToBeTrue = AllZs.Where(g => g.ExpectedOutput);
    List<NonStaticGate> result = AllZs.Where(g => !g.ValueAsExpected).ToList();
    SetInputValuesAndExpectedValue(0b111111111111111111111111111111111111111111111, 0b000000000000000000000000000000000000000000000);
    gatesToBeTrue = AllZs.Where(g => g.ExpectedOutput);
    result.AddRange(AllZs.Where(g => !g.ValueAsExpected));
    SetInputValuesAndExpectedValue(0b101010101010101010101010101010101010101010101, 0b010101010101010101010101010101010101010101010);
    gatesToBeTrue = AllZs.Where(g => g.ExpectedOutput);
    result.AddRange(AllZs.Where(g => !g.ValueAsExpected));
    SetInputValuesAndExpectedValue(0b010101010101010101010101010101010101010101010, 0b010101010101010101010101010101010101010101010);
    gatesToBeTrue = AllZs.Where(g => g.ExpectedOutput);
    result.AddRange(AllZs.Where(g => !g.ValueAsExpected));
    SetInputValuesAndExpectedValue(0b101010101010101010101010101010101010101010101, 0b101010101010101010101010101010101010101010101);
    gatesToBeTrue = AllZs.Where(g => g.ExpectedOutput);
    result.AddRange(AllZs.Where(g => !g.ValueAsExpected));
    SetInputValuesAndExpectedValue(0b010101010101010101010101010101010101010101010, 0b101010101010101010101010101010101010101010101);
    gatesToBeTrue = AllZs.Where(g => g.ExpectedOutput);
    result.AddRange(AllZs.Where(g => !g.ValueAsExpected));
    SetInputValuesAndExpectedValue(0b000111000111000111000111000111000111000111000, 0b001110001110001110001110001110001110001110001);
    gatesToBeTrue = AllZs.Where(g => g.ExpectedOutput);
    result.AddRange(AllZs.Where(g => !g.ValueAsExpected));
    SetInputValuesAndExpectedValue(0b001110001110001110001110001110001110001110001, 0b011100011100011100011100011100011100011100011);
    gatesToBeTrue = AllZs.Where(g => g.ExpectedOutput);
    result.AddRange(AllZs.Where(g => !g.ValueAsExpected));
    SetInputValuesAndExpectedValue(0b011100011100011100011100011100011100011100010, 0b111000111000111000111000111000111000111000111);
    gatesToBeTrue = AllZs.Where(g => g.ExpectedOutput);
    result.AddRange(AllZs.Where(g => !g.ValueAsExpected));
    SetInputValuesAndExpectedValue(0b111111111111111111111111111111111111111111111, 0b111111111111111111111111111111111111111111111);
    gatesToBeTrue = AllZs.Where(g => g.ExpectedOutput);
    result.AddRange(AllZs.Where(g => !g.ValueAsExpected));
    SetInputValuesAndExpectedValue(startResultX, startResultY);
    gatesToBeTrue = AllZs.Where(g => g.ExpectedOutput);
    result.AddRange(AllZs.Where(g => !g.ValueAsExpected));
    result = result.Distinct().ToList();
    return result.Distinct();
}

void SetInputValuesAndExpectedValue(long x, long y)
{
    foreach (var gate in AllXs)
    {
        gate.SetInput(x);
    }
    foreach (var gate in AllYs)
    {
        gate.SetInput(y);
    }
    long newX = GetXResult();
    long newY = GetYResult();
    long expectedResult = GetXResult() + GetYResult();
    foreach(var gate in AllZs)
    {
        gate.SetExpectedValue(expectedResult);
    }
}

