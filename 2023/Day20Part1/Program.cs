using Day20Part1;

List<Module> modules = File.ReadAllLines("input.txt").Select(Module.FromDefinition).ToList();
foreach (var module in modules)
{
    module.Initialize();
}

long totalHigh = 0;
long totalLow = 0;
for (int i = 0; i < 1000; i++)
{
    (long low, long high) result = PressButton();
    totalHigh += result.high;
    totalLow += result.low;
}

Console.WriteLine(totalHigh * totalLow);

(long low, long high) PressButton()
{
    long lowPulses = 0;
    long highPulses = 0;
    Queue<Pulse> pulseQueue = new Queue<Pulse>();
    pulseQueue.Enqueue(new Pulse("button", "broadcaster", false));
    while(pulseQueue.Count > 0)
    {
        Pulse pulse = pulseQueue.Dequeue();
        if(pulse.High)
        {
            highPulses++;
        }
        else
        {
            lowPulses++;
        }
        if (Module.AllModules.TryGetValue(pulse.Receipient, out var module))
        {
            foreach (Pulse newPulse in module.HandlePulse(pulse))
            {
                pulseQueue.Enqueue(newPulse);
            }
        }
    }
    return (lowPulses, highPulses);
}