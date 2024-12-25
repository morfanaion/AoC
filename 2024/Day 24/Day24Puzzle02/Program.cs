using Day24Puzzle02;
using AOC.Maths;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

Stack<Gate> _stackOfGates = new Stack<Gate>();

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
Console.WriteLine(string.Concat(Gate.Gates.Where(g => g.Id.StartsWith("x")).OrderByDescending(g => g.Id).Select(g => g.Output ? '1' : '0')));

long resultx = GetXResult();
long resulty = GetYResult();
long expectedResult = resultx + resulty;
Console.WriteLine(Convert.ToString(expectedResult, 2));
foreach(NonStaticGate gate in Gate.Gates.Where(g => g.Id.StartsWith("z")))
{
    gate.SetExpectedValue(expectedResult);
}
long result = GetZResult();
Console.WriteLine(Convert.ToString(result, 2));
List<Gate> correctGates = new List<Gate>();
//List<Gate> swappedGates = new List<Gate>();
List<NonStaticGate> allZs = AllZs().OrderBy(g => g.Id).ToList();
/*foreach (var gate in AllZs().OrderBy(g => g.Id))
{
    Console.WriteLine($"{gate.Id}: {string.Join(",", gate.Nodes.Distinct().Select(g => g.Id))}");
    if(!gate.ValueAsExpected)
    {
        foreach (NonStaticGate gate1 in gate.Nodes.OfType<NonStaticGate>().Where(g => correctGates.All(g2 => g2.Id != g.Id)))
        {
            bool isOk = false;
            foreach(NonStaticGate gate2 in correctGates.OfType<NonStaticGate>().Where(g => g.Id != gate1.Id && correctGates.All(g2 => g2.Id != g.Id)))
            {
                SwapGates(gate1, gate2);
                if(gate.ValueAsExpected)
                { 
                    isOk = true;
                    swappedGates.Add(gate1);
                    swappedGates.Add(gate2);
                    break;
                }
                SwapGates(gate1, gate2);
            }
            if(isOk)
            {
                break;
            }
        }
    }
    correctGates = gate.Nodes.ToList();
}

*/
List<NonStaticGate> swappableGates = AllZs().Where(g => !g.ValueAsExpected).SelectMany(g => g.Nodes).Concat(AllZs()).OfType<NonStaticGate>().Distinct().ToList();

/*
 * Fail by brute force
for (int ig1 = 0; ig1 <= swappableGates.Count - 8; ig1++)
{
    for (int ig2 = ig1 + 1; ig2 < swappableGates.Count; ig2++)
    {
        if (!(OneNotAncestorOfOther(swappableGates[ig1], swappableGates[ig2]) &&
            OneNotAncestorOfOther(swappableGates[ig2], swappableGates[ig1])))
        {
            continue;
        }
        SwapGates(swappableGates[ig1], swappableGates[ig2]);
        if (!(OneNotAncestorOfOther(swappableGates[ig1], swappableGates[ig1]) &&
            OneNotAncestorOfOther(swappableGates[ig2], swappableGates[ig2])))
        {
            SwapGates(swappableGates[ig1], swappableGates[ig2]);
            continue;
        }
        for (int ig3 = ig1 + 1; ig3 <= swappableGates.Count - 6; ig3++)
        {
            if (ig3 == ig2)
            {
                continue;
            }
            for (int ig4 = ig3 + 1; ig4 < swappableGates.Count; ig4++)
            {
                if (ig4 == ig2)
                {
                    continue;
                }
                if (!(OneNotAncestorOfOther(swappableGates[ig3], swappableGates[ig4]) &&
                    OneNotAncestorOfOther(swappableGates[ig3], swappableGates[ig4])))
                {
                    continue;
                }
                SwapGates(swappableGates[ig3], swappableGates[ig4]);
                if (!(OneNotAncestorOfOther(swappableGates[ig3], swappableGates[ig3]) &&
                    OneNotAncestorOfOther(swappableGates[ig4], swappableGates[ig4])))
                {
                    SwapGates(swappableGates[ig3], swappableGates[ig4]);
                    continue;
                }
                for (int ig5 = ig3 + 1; ig5 <= swappableGates.Count - 4; ig5++)
                {
                    if (ig5 == ig2 || ig5 == ig4)
                    {
                        continue;
                    }
                    for (int ig6 = ig5 + 1; ig6 < swappableGates.Count; ig6++)
                    {
                        if (ig6 == ig2 || ig6 == ig4)
                        {
                            continue;
                        }
                        if (!(OneNotAncestorOfOther(swappableGates[ig5], swappableGates[ig6]) &&
                            OneNotAncestorOfOther(swappableGates[ig5], swappableGates[ig6])))
                        {
                            continue;
                        }
                        SwapGates(swappableGates[ig5], swappableGates[ig6]);
                        if (!(OneNotAncestorOfOther(swappableGates[ig5], swappableGates[ig5]) &&
                            OneNotAncestorOfOther(swappableGates[ig6], swappableGates[ig6])))
                        {
                            SwapGates(swappableGates[ig5], swappableGates[ig6]);
                            continue;
                        }
                        for (int ig7 = ig5 + 1; ig7 <= swappableGates.Count - 2; ig7++)
                        {
                            if (ig7 == ig2 || ig7 == ig4 || ig7 == ig6)
                            {
                                continue;
                            }
                            for (int ig8 = ig7 + 1; ig8 < swappableGates.Count; ig8++)
                            {
                                if (ig8 == ig2 || ig8 == ig4 || ig8 == ig6)
                                {
                                    continue;
                                }
                                if (!(OneNotAncestorOfOther(swappableGates[ig7], swappableGates[ig8]) &&
                                    OneNotAncestorOfOther(swappableGates[ig8], swappableGates[ig7])))
                                {
                                    continue;
                                }
                                SwapGates(swappableGates[ig7], swappableGates[ig8]);
                                if (!(OneNotAncestorOfOther(swappableGates[ig7], swappableGates[ig7]) &&
                                    OneNotAncestorOfOther(swappableGates[ig8], swappableGates[ig8])))
                                {
                                    SwapGates(swappableGates[ig7], swappableGates[ig8]);
                                    continue;
                                }

                                if (AllZs().All(g => g.ValueAsExpected))
                                {
                                    Console.WriteLine(string.Join(',', new NonStaticGate[] { swappableGates[ig1], swappableGates[ig2], swappableGates[ig3], swappableGates[ig4], swappableGates[ig5], swappableGates[ig6], swappableGates[ig7], swappableGates[ig8] }.Select(g => g.Id).Order()));
                                    return;
                                }
                                SwapGates(swappableGates[ig7], swappableGates[ig8]);
                            }
                        }
                        SwapGates(swappableGates[ig5], swappableGates[ig6]);
                    }
                }
                SwapGates(swappableGates[ig3], swappableGates[ig4]);
            }
        }
        SwapGates(swappableGates[ig1], swappableGates[ig2]);
    }
}*/

List<NonStaticGate> wrongZs = AllZs().Where(g => !g.ValueAsExpected).ToList();
var faultyGates = wrongZs.SelectMany(AllAncestorGates).GroupBy(g => g.Id).OrderByDescending(group => group.Count());
List<NonStaticGate> rightZs = AllZs().Where(g => g.ValueAsExpected).ToList();
List<NonStaticGate> excludedGatesForSwapping = new List<NonStaticGate>();
List<(NonStaticGate, NonStaticGate)> swappedGates = new List<(NonStaticGate, NonStaticGate)>();
int attempt = 0;
List<PriorityQueue<(NonStaticGate gate1, NonStaticGate gate2), int>> queues = new List<PriorityQueue<(NonStaticGate gate1, NonStaticGate gate2), int>>()
{
  new PriorityQueue<(NonStaticGate gate1, NonStaticGate gate2), int>(),
	new PriorityQueue<(NonStaticGate gate1, NonStaticGate gate2), int>(),
	new PriorityQueue<(NonStaticGate gate1, NonStaticGate gate2), int>(),
	new PriorityQueue<(NonStaticGate gate1, NonStaticGate gate2), int>()
};
while (wrongZs.Any())
{
  foreach (NonStaticGate gate1 in swappableGates)
  {
    foreach (NonStaticGate gate2 in swappableGates.Where(g => g.Id != gate1.Id))
    {
      SwapGates(gate1, gate2);
      Gate.ValidResult = true;
      int newDifference = AllZs().Count(g => g.ValueAsExpected) - rightZs.Count;
      if (newDifference > 0)
      {
        queues[attempt].Enqueue((gate1, gate2), 100 - newDifference);
      }
      SwapGates(gate1, gate2);
    }
  }
  if (queues[attempt].Count > 0)
  {
		(NonStaticGate gate1, NonStaticGate gate2) = queues[attempt].Dequeue();
    SwapGates(gate1, gate2);
    swappedGates.Add((gate1, gate2));
    rightZs = AllZs().Where(g => g.ValueAsExpected).ToList();
    wrongZs = AllZs().Where(g => !g.ValueAsExpected).ToList();
    swappableGates = AllZs().Where(g => !g.ValueAsExpected).SelectMany(g => g.Nodes).Concat(AllZs()).OfType<NonStaticGate>().Distinct().ToList();
    attempt++;
  }
  else
  {
    (NonStaticGate gate1, NonStaticGate gate2) = swappedGates[attempt];
		SwapGates(gate1, gate2);
    swappedGates.RemoveAt(attempt);
	}
}
long zresult = GetZResult();
Console.WriteLine(string.Join(',', swappedGates.Select(g => g.Id).Order()));

//long actualResult = AllZs().Select(g => g.Value).Combine((v1, v2) => v1 | v2, 0);

foreach (string str in Gate.Gates.Select(g => g.GetDefinitionString()).Where(s => input.All(i => i != s)))
{
    Console.WriteLine(str);
}

bool OneNotAncestorOfOther(NonStaticGate gate1, NonStaticGate gate2)
{
    return AllAncestorGates(gate2.Input1).All(g2 => g2.Id != gate1.Id) && AllAncestorGates(gate2.Input2).All(g2 => g2.Id != gate1.Id);
}

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

IEnumerable<(T, T)> AllCombinations<T>(IEnumerable<T> enumerable)
{
    int x = 0;
    foreach(T t1 in enumerable)
    {
        x++;
        foreach(T t2 in enumerable.Skip(x))
        {
            yield return (t1, t2);
        }
    }
}

IEnumerable<NonStaticGate> AllAncestorGates(Gate gate)
{
    if (_stackOfGates.Any(g => g == gate))
    {
        yield break;
    }
    _stackOfGates.Push(gate);
    if (gate is NonStaticGate nonStaticGate)
    {
        yield return nonStaticGate;
        foreach (NonStaticGate ancestor in AllAncestorGates(nonStaticGate.Input1))
        {
            yield return ancestor;
        }
        foreach (NonStaticGate ancestor in AllAncestorGates(nonStaticGate.Input2))
        {
            yield return ancestor;
        }
    }
    _stackOfGates.Pop();
}

long GetZResult() => AllZs().Select(g => g.Value).Combine((v1, v2) => v1 | v2, 0);
long GetXResult() => Gate.Gates.Where(g => g.Id.StartsWith("x")).Select(g => g.Value).Combine((v1, v2) => v1 | v2, 0);
long GetYResult() => Gate.Gates.Where(g => g.Id.StartsWith("y")).Select(g => g.Value).Combine((v1, v2) => v1 | v2, 0);

IEnumerable<NonStaticGate> AllZs() => Gate.Gates.Where(g => g.Id.StartsWith("z")).Cast<NonStaticGate>();