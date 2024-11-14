namespace Day16Puzzle01
{
    internal class Valve
    {
        public static List<Valve> AllValves { get; set; } = new List<Valve>();
        private static int? _maxRelease;
        public static int MaxRelease => _maxRelease ??= AllValves.Sum(valve => valve.FlowRate);

        public string Name { get; set; } = string.Empty;
        public int FlowRate { get; set; }
        public string[] ConnectedValveNames { get; set; } = new string[0];
        public IEnumerable<Valve> ConnectedValves => AllValves.Where(valve => ConnectedValveNames.Contains(valve.Name));
        string Path = string.Empty;

        Dictionary<string, string>? _shortestRoutes;

        public string ShortestRouteTo(Valve valve)
        {
            if (_shortestRoutes == null)
            {
                foreach(Valve valve2 in Valve.AllValves)
                {
                    valve2.Path = string.Empty;
                }
                PriorityQueue<Valve, int> queue = new PriorityQueue<Valve, int>();
                foreach(Valve connected in ConnectedValves)
                {
                    connected.Path = connected.Name;
                    queue.Enqueue(connected, connected.Path.Length);
                }
                while(queue.Count != 0)
                {
                    Valve currentValve = queue.Dequeue();
                    foreach(Valve connected in currentValve.ConnectedValves.Where(sub => string.IsNullOrEmpty(sub.Path)))
                    {
                        connected.Path = currentValve.Path + connected.Name;
                        queue.Enqueue(connected, connected.Path.Length);
                    }
                }
                _shortestRoutes = new Dictionary<string, string>();
                _shortestRoutes.Add(Name, "");
                foreach (Valve valve2 in Valve.AllValves)
                {
                    if(valve2.Name != Name)
                    {
                        _shortestRoutes.Add(valve2.Name, valve2.Path);
                    }
                }
            }
            return _shortestRoutes[valve.Name];
        }

        private void AddRoutesFromValve(Valve valve, string routeThusfar)
        {
            if (_shortestRoutes == null) throw new InvalidOperationException();
            if (_shortestRoutes.ContainsKey(valve.Name)) return;
            foreach(Valve connectedValve in valve.ConnectedValves)
            {
                if(!_shortestRoutes.ContainsKey(connectedValve.Name))
                {
                    _shortestRoutes.Add(connectedValve.Name, routeThusfar + connectedValve.Name);
                }
            }
            foreach(Valve connectedValve in valve.ConnectedValves)
            {
                AddRoutesFromValve(connectedValve, routeThusfar + connectedValve.Name);
            }
        }
    }
}
