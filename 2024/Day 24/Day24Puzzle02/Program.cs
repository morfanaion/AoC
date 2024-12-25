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

// first get the numbers that represent x and y
long resultx = GetXResult();
long resulty = GetYResult();
// add them together to get the result we want
long expectedResult = resultx + resulty;
// tell all Zs what the expected result should be and let them determine what output they need to create to get that result
foreach(NonStaticGate gate in Gate.Gates.Where(g => g.Id.StartsWith("z")))
{
    gate.SetExpectedValue(expectedResult);
}
long result = GetZResult();
// determine, given the Zs that are incorrect, which gates are their ancestors and include the Zs themselves
List<NonStaticGate> swappableGates = AllZs().Where(g => !g.ValueAsExpected).SelectMany(g => g.Nodes).Concat(AllZs()).OfType<NonStaticGate>().Distinct().ToList();
// create lists of Zs that were wrong and Zs that were right for checking purposes
List<NonStaticGate> wrongZs = AllZs().Where(g => !g.ValueAsExpected).ToList();
List<NonStaticGate> rightZs = AllZs().Where(g => g.ValueAsExpected).ToList();
// create a list to hold our swaps
List<(NonStaticGate, NonStaticGate)> swappedGates = new List<(NonStaticGate, NonStaticGate)>();
int attempt = 0;
// put a system in place to try different swaps if 1 swap does not least to an answer
List<PriorityQueue<(NonStaticGate gate1, NonStaticGate gate2), int>> queues = new List<PriorityQueue<(NonStaticGate gate1, NonStaticGate gate2), int>>()
{
    new PriorityQueue<(NonStaticGate gate1, NonStaticGate gate2), int>(),
    new PriorityQueue<(NonStaticGate gate1, NonStaticGate gate2), int>(),
    new PriorityQueue<(NonStaticGate gate1, NonStaticGate gate2), int>(),
    new PriorityQueue<(NonStaticGate gate1, NonStaticGate gate2), int>()
};
while (wrongZs.Any())
{
    if (attempt < 4)
    {
        foreach (NonStaticGate gate1 in swappableGates)
        {
            foreach (NonStaticGate gate2 in swappableGates.Where(g => g.Id != gate1.Id))
            {
                SwapGates(gate1, gate2);
                Gate.ValidResult = true;
                int newDifference = AllZs().Count(g => g.ValueAsExpected) - rightZs.Count;
                // if we are getting more correct Zs, add them to the queue for this iteration
                if (newDifference > 0)
                {
                    queues[attempt].Enqueue((gate1, gate2), 100 - newDifference);
                }
                SwapGates(gate1, gate2);
            }
        }
    }
    if (queues[attempt].Count > 0 || attempt >= 4)
    {
        (NonStaticGate gate1, NonStaticGate gate2) = queues[attempt].Dequeue();
        SwapGates(gate1, gate2);
        swappedGates.Add((gate1, gate2));
        rightZs = AllZs().Where(g => g.ValueAsExpected).ToList();
        wrongZs = AllZs().Where(g => !g.ValueAsExpected).ToList();
        swappableGates = AllZs().Where(g => !g.ValueAsExpected).SelectMany(g => g.Nodes).Concat(AllZs()).OfType<NonStaticGate>().Where(g => swappedGates.All(s => s.Item1.Id != g.Id && s.Item2.Id != g.Id)).Distinct().ToList();
        attempt++;
    }
    else
    {
        // our current attempt has no more items in the queue, we need to revert the last swap and try again.
        bool stillHaveAChance = false;
        while (attempt > 0 && !stillHaveAChance)
        {
            attempt--;
            (NonStaticGate gate1, NonStaticGate gate2) = swappedGates[attempt];
            SwapGates(gate1, gate2);
            swappedGates.RemoveAt(attempt);
            if (queues[attempt].TryDequeue(out (NonStaticGate gate1, NonStaticGate gate2) dequeued, out int priority))
            {
                stillHaveAChance = true;
                SwapGates(dequeued.gate1, dequeued.gate2);
                swappedGates.Add((dequeued.gate1, dequeued.gate2));
                rightZs = AllZs().Where(g => g.ValueAsExpected).ToList();
                wrongZs = AllZs().Where(g => !g.ValueAsExpected).ToList();
                swappableGates = AllZs().Where(g => !g.ValueAsExpected).SelectMany(g => g.Nodes).Concat(AllZs()).OfType<NonStaticGate>().Where(g => swappedGates.All(s => s.Item1.Id != g.Id && s.Item2.Id != g.Id)).Distinct().ToList();
                attempt++;
            }
        }
    }
}
Console.WriteLine(string.Join(',', swappedGates.SelectMany(g => new string[] { g.Item1.Id, g.Item2.Id }).Order()));
Console.WriteLine($"Expected output: {expectedResult}");
Console.WriteLine($"Actual output: {GetZResult()}");

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

long GetZResult() => AllZs().Select(g => g.Value).Combine((v1, v2) => v1 | v2, 0);
long GetXResult() => Gate.Gates.Where(g => g.Id.StartsWith("x")).Select(g => g.Value).Combine((v1, v2) => v1 | v2, 0);
long GetYResult() => Gate.Gates.Where(g => g.Id.StartsWith("y")).Select(g => g.Value).Combine((v1, v2) => v1 | v2, 0);

IEnumerable<NonStaticGate> AllZs() => Gate.Gates.Where(g => g.Id.StartsWith("z")).Cast<NonStaticGate>();