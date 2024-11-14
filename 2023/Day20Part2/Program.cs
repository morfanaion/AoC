using Day20Part2;

List<Module> modules = File.ReadAllLines("input.txt").Select(Module.FromDefinition).ToList();
foreach (var module in modules)
{
    module.Initialize();
}

long tries = 0;

Dictionary<string, long> repetitions = new Dictionary<string, long>();
string recipient = string.Empty;
if(modules.FirstOrDefault(m => m.OutputModules.Contains("rx")) is ConjunctionModule m)
{
    recipient = m.Id;
    foreach (string moduleId in m.InputMemory.Keys)
    {
        repetitions.Add(moduleId, 0);
    }
}

while(repetitions.Values.Any(v => v == 0))
{
    tries++;
    foreach(string moduleId in PressButton(recipient, repetitions.Keys.ToArray()))
    {
        if (repetitions[moduleId] == 0)
        {
            repetitions[moduleId] = tries;
        }
    }
}

Console.WriteLine(findLCM(repetitions.Values.ToList()));

long findGCD(long a, long b)
{
    if (b == 0)
    {
        return a;
    }
    return findGCD(b, a % b);
}

long findLCM(List<long> numbers)
{
    long result = numbers[0];
    for (int i = 1; i < numbers.Count; i++)
    {
        result = numbers[i] * result / findGCD(numbers[i], result);
    }
    return result;
}

List<string> PressButton(string recipient, string[] keys)
{
    List<string> receivedPulse = new List<string>();
    Queue<Pulse> pulseQueue = new Queue<Pulse>();
    pulseQueue.Enqueue(new Pulse("button", "broadcaster", false));
    while(pulseQueue.Count > 0)
    {
        Pulse pulse = pulseQueue.Dequeue();
        if (Module.AllModules.TryGetValue(pulse.Receipient, out var module))
        {
            foreach (Pulse newPulse in module.HandlePulse(pulse))
            {
                pulseQueue.Enqueue(newPulse);
            }
        }
        if(pulse.Receipient == recipient)
        {
            if (pulse.High && keys.Contains(pulse.Sender))
            {
                receivedPulse.Add(pulse.Sender);
            }          
        }
    }
    return receivedPulse;
}