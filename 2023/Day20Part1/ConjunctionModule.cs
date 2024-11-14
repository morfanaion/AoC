
namespace Day20Part1
{
    internal class ConjunctionModule(string id, string[] outputModules) : Module(id, outputModules)
    {
        private Dictionary<string, bool> InputMemory { get; set; } = new Dictionary<string, bool>();
        public override IEnumerable<Pulse> HandlePulse(Pulse pulse)
        {
            InputMemory[pulse.Sender] = pulse.High;
            bool high = InputMemory.Values.Any(x => !x);
            foreach(string outputModule in OutputModules)
            {
                yield return new Pulse(Id, outputModule, high);
            }
        }

        public override void Initialize()
        {
            foreach(string inputModuleId in Module.AllModules.Values.Where(m => m.OutputModules.Contains(Id)).Select(m => m.Id))
            {
                InputMemory[inputModuleId] = false;
            }
        }
    }
}