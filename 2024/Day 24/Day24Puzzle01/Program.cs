using Day24Puzzle01;

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
                "XOR" => new XorGate(),
                "AND" => new AndGate(),
                "OR" => new OrGate(),
                _ => throw new InvalidDataException("Unsupported operator found")
            };
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
foreach(string input in File.ReadAllLines("input.txt"))
{
    processLine(input);
}

ulong result = 0;
foreach(Gate gate in Gate.Gates.Where(g => g.Id.StartsWith("z")).OrderByDescending(g => g.Id))
{
    result <<= 1;
    if(gate.Output)
    {
        result++;
    }
}
Console.WriteLine(result);