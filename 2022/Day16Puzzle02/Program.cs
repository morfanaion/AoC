// See https://aka.ms/new-console-template for more information
using Day16Puzzle02;
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
for (int i = 0; i < sensibleValves.Count; i++)
{
    for(int j = i + 1; j < sensibleValves.Count; j++)
    {
        List<Valve> valvesToOpen = new List<Valve>(sensibleValves);
        Valve valve1 = sensibleValves[i];
        Valve valve2 = sensibleValves[j];
        int timeCostValve1 = (currentValve.ShortestRouteTo(valve1).Length / 2) + 1;
        int timeCostValve2 = (currentValve.ShortestRouteTo(valve2).Length / 2) + 1;
        int timeCost = Math.Min(timeCostValve1, timeCostValve2);
        int timeRemainingForOther = Math.Max(timeCostValve1, timeCostValve2) - timeCost;
        valvesToOpen.Remove(valve1);
        valvesToOpen.Remove(valve2);
        queue.Enqueue(new State(timeCost == timeCostValve1 ? valve1 : valve2, timeCost == timeCostValve1 ? valve2 : valve1)
        {
            Action = PuzzleAction.Open,
            MinutesPast = timeCost,
            OtherTimeRemaining = timeRemainingForOther,
            PressureReleased = 0,
            ValvesToOpen = valvesToOpen
        }, -(26 - timeCost) * Valve.MaxRelease); ;
    }
}

State currentState = new State(currentValve, currentValve);
while ((currentState = queue.Dequeue()) != null && currentState.MinutesPast < 26)
{
    int flowrate = currentState.ValvesOpened.Sum(valve => valve.FlowRate);
    List<Valve> newOpenedState = new List<Valve>(currentState.ValvesOpened);
    if (currentState.Action == PuzzleAction.Open)
    {
        newOpenedState.Add(currentState.CurrentValve);
        flowrate += currentState.CurrentValve.FlowRate;
    }
    if (!(currentState.ValvesToOpen.Any()))
    {
        if(currentState.OtherValve.Name != "DONE")
        {
            State newState = new State(currentState.OtherValve, new Valve() {  Name = "DONE"})
            {
                Action = PuzzleAction.Open,
                MinutesPast = currentState.MinutesPast + currentState.OtherTimeRemaining,
                PressureReleased = currentState.PressureReleased + (flowrate * currentState.OtherTimeRemaining),
                ValvesOpened = newOpenedState,
                OtherTimeRemaining = 26 - (currentState.MinutesPast + currentState.OtherTimeRemaining)
            };
            queue.Enqueue(
                newState,
                -(currentState.PressureReleased + (flowrate * Math.Min(currentState.OtherTimeRemaining, 26 - currentState.MinutesPast)) + ((26 - (currentState.MinutesPast + Math.Min(currentState.OtherTimeRemaining, 26 - currentState.MinutesPast))) * Valve.MaxRelease)));
        }
        else
        {
            queue.Enqueue(
            new State(currentState.OtherValve, currentState.OtherValve)
            {
                Action = PuzzleAction.Wait,
                MinutesPast = 26,
                PressureReleased = currentState.PressureReleased + ((26 - currentState.MinutesPast) * flowrate),
                ValvesOpened = newOpenedState
            },
            -(currentState.PressureReleased + (26 - currentState.MinutesPast) * flowrate));
        }
        
    }
    else
    {
        bool anyAdded = false;
        foreach (Valve connectedValve in currentState.ValvesToOpen)
        {
            if (currentState.CurrentValve.Name != connectedValve.Name)
            {
                string path = currentState.CurrentValve.ShortestRouteTo(connectedValve);
                int timeCostValve1 = (path.Length / 2) + 1;
                int timeCostValve2 = currentState.OtherTimeRemaining;
                int timeCost = Math.Min(timeCostValve1, timeCostValve2);
                int timeRemainingForOther = Math.Max(timeCostValve1, timeCostValve2) - timeCost;
                List<Valve> newClosedState = new List<Valve>(currentState.ValvesToOpen);
                newClosedState.Remove(connectedValve);
                if (currentState.MinutesPast + timeCostValve1 <= 26)
                {
                    anyAdded = true;
                    State newState = new State(timeCost == timeCostValve1 ? connectedValve : currentState.OtherValve, timeCost == timeCostValve1 ? currentState.OtherValve : connectedValve)
                    {
                        Action = PuzzleAction.Open,
                        MinutesPast = currentState.MinutesPast + timeCost,
                        PressureReleased = currentState.PressureReleased + (flowrate * timeCost),
                        ValvesOpened = newOpenedState,
                        ValvesToOpen = newClosedState,
                        OtherTimeRemaining = timeRemainingForOther,
                    };
                    queue.Enqueue(
                        newState,
                        -(currentState.PressureReleased + (flowrate * Math.Min(timeCost, 26 - currentState.MinutesPast)) + ((26 - (currentState.MinutesPast + Math.Min(timeCost, 26 - currentState.MinutesPast))) * Valve.MaxRelease)));
                }
            }
        }
        if(!anyAdded)
        {
            if (currentState.OtherValve.Name != "DONE")
            {
                State newState = new State(currentState.OtherValve, new Valve() { Name = "DONE" })
                {
                    Action = PuzzleAction.Open,
                    MinutesPast = currentState.MinutesPast + currentState.OtherTimeRemaining,
                    PressureReleased = currentState.PressureReleased + (flowrate * currentState.OtherTimeRemaining),
                    ValvesOpened = newOpenedState,
                    OtherTimeRemaining = 26 - (currentState.MinutesPast + currentState.OtherTimeRemaining)
                };
                queue.Enqueue(
                    newState,
                    -(currentState.PressureReleased + (flowrate * Math.Min(currentState.OtherTimeRemaining, 26 - currentState.MinutesPast)) + ((26 - (currentState.MinutesPast + Math.Min(currentState.OtherTimeRemaining, 26 - currentState.MinutesPast))) * Valve.MaxRelease)));
            }
            else
            {
                queue.Enqueue(
                new State(currentState.OtherValve, currentState.OtherValve)
                {
                    Action = PuzzleAction.Wait,
                    MinutesPast = 26,
                    PressureReleased = currentState.PressureReleased + ((26 - currentState.MinutesPast) * flowrate),
                    ValvesOpened = newOpenedState
                },
                -(currentState.PressureReleased + (26 - currentState.MinutesPast) * flowrate));
            }
        }
    }

}
Console.WriteLine((currentState ?? new State(currentValve, currentValve)).PressureReleased);


