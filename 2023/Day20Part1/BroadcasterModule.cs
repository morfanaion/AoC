
namespace Day20Part1
{
    internal class BroadcasterModule : Module
    {
        public BroadcasterModule(string id, string[] outputModules) : base(id, outputModules)
        {
        }

        public override IEnumerable<Pulse> HandlePulse(Pulse pulse)
        {
            foreach(string outputModule in OutputModules)
            {
                yield return new Pulse(Id, outputModule, pulse.High);
            }
        }
    }
}