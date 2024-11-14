// See https://aka.ms/new-console-template for more information
using Day16Puzzle01;
using System.Text.RegularExpressions;

Valve ValveFromMatch(Match match) => new Valve()
{
    Name = match.Groups["valveName"].Value,
    FlowRate = int.Parse(match.Groups["Flowrate"].Value),
    ConnectedValveNames = match.Groups["ConnectedValveNames"].Value.Split(", ")
};

Regex regex = new Regex(@"Valve (?<valveName>[A-Z]{2}) has flow rate=(?<Flowrate>\d*); tunnels? leads? to valves? (?<ConnectedValveNames>[A-Z]{2}(, [A-Z]{2})*)");
Valve.AllValves = File.ReadAllLines("Input.txt").Select(line => ValveFromMatch(regex.Match(line))).ToList();

List<Valve> sensibleValves = Valve.AllValves.Where(valve => valve.FlowRate > 0).ToList();
Valve currentValve = Valve.AllValves.Single(valve => valve.Name == "AA");
PriorityQueue<State, int> queue = new PriorityQueue<State, int>();
if (currentValve.FlowRate > 0)
{
    queue.Enqueue(new State(currentValve) 
    { 
        Action = PuzzleAction.Open, 
        MinutesPast = 1, 
        PressureReleased = 0,
        ValvesToOpen = sensibleValves
    }, -(29 * Valve.MaxRelease));
}
foreach(Valve valveToVisit in sensibleValves)
{
    if (valveToVisit.Name != currentValve.Name)
    {
        string path = currentValve.ShortestRouteTo(valveToVisit);
        int timeCost = (path.Length / 2) + 1;
        queue.Enqueue(new State(valveToVisit)
        { 
            Action = PuzzleAction.Open, 
            MinutesPast = timeCost, 
            PressureReleased = 0,
            ValvesToOpen = sensibleValves
        }, -((30 - timeCost) * Valve.MaxRelease));
    }
}
State currentState = new State(currentValve);
while ((currentState = queue.Dequeue()) != null && currentState.MinutesPast < 30)
{
    int flowrate = currentState.ValvesOpened.Sum(valve => valve.FlowRate);
    List<Valve> newOpenedState = new List<Valve>(currentState.ValvesOpened);
    List<Valve> newClosedState = new List<Valve>(currentState.ValvesToOpen);
    string valveOpenedOrder = currentState.ValveOpenOrder;
    if (currentState.Action == PuzzleAction.Open)
    {
        newOpenedState.Add(currentState.CurrentValve);
        int idx = newClosedState.IndexOf(currentState.CurrentValve);
        newClosedState.RemoveAt(idx);
        flowrate += currentState.CurrentValve.FlowRate;
        valveOpenedOrder += currentState.CurrentValve.Name;
    }
    if (flowrate == Valve.MaxRelease)
    {
        queue.Enqueue(
            new State(currentState.CurrentValve)
            {
                Action = PuzzleAction.Wait,
                MinutesPast = 30,
                PressureReleased = currentState.PressureReleased + ((30 - currentState.MinutesPast) * flowrate),
                ValvesOpened = newOpenedState,
                ValvesToOpen = newClosedState,
                ValveOpenOrder = valveOpenedOrder
            },
            -(currentState.PressureReleased + (30 - currentState.MinutesPast) * flowrate));
    }
    else
    {
        bool anyAdded = false;
        foreach (Valve connectedValve in currentState.ValvesToOpen)
        {
            if (currentState.CurrentValve.Name != connectedValve.Name)
            {
                string path = currentState.CurrentValve.ShortestRouteTo(connectedValve);
                int timeCost = (path.Length / 2) + 1;
                if (currentState.MinutesPast + timeCost <= 30)
                {
                    anyAdded = true;
                    State newState = new State(connectedValve)
                    {
                        Action = PuzzleAction.Open,
                        MinutesPast = currentState.MinutesPast + timeCost,
                        PressureReleased = currentState.PressureReleased + (flowrate * timeCost),
                        ValvesOpened = newOpenedState,
                        ValvesToOpen = newClosedState,
                        ValveOpenOrder = valveOpenedOrder
                    };
                    queue.Enqueue(
                        newState,
                        -(currentState.PressureReleased + (flowrate * Math.Min(timeCost, 30 - currentState.MinutesPast)) + ((30 - (currentState.MinutesPast + Math.Min(timeCost, 30 - currentState.MinutesPast))) * Valve.MaxRelease)));
                }
            }
        }
        if(!anyAdded)
        {
            queue.Enqueue(
            new State(currentState.CurrentValve)
            {
                Action = PuzzleAction.Wait,
                MinutesPast = 30,
                PressureReleased = currentState.PressureReleased + ((30 - currentState.MinutesPast) * flowrate),
                ValvesOpened = newOpenedState,
                ValvesToOpen = newClosedState,
                ValveOpenOrder = valveOpenedOrder
            },
            -(currentState.PressureReleased + (30 - currentState.MinutesPast) * flowrate));
        }
    }

}
Console.WriteLine(currentState?.PressureReleased);


