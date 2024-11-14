namespace Day20Part2
{
    internal class FlipFlopModule(string id, string[] outputModules) : Module(id, outputModules)
    {
        private bool On { get; set; } = false;

        public override IEnumerable<Pulse> HandlePulse(Pulse pulse)
        {
            if (pulse.High)
            {
                yield break;
            }
            On = !On;
            foreach(string outputModule in OutputModules)
            {
                yield return new Pulse(Id, outputModule, On);
            }
        }
    }
}