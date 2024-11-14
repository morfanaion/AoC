using System.Text.RegularExpressions;

namespace Day20Part1
{
    internal abstract class Module(string id, string[] outputModules)
    {
        public static Dictionary<string, Module> AllModules { get; set; } = new Dictionary<string, Module>();

        public static Module FromDefinition(string definition)
        {
            Match match = RegexHelper.Instance.ModuleRegex().Match(definition);
            Module result;
            switch(match.Groups["type"].Value) 
            {
                case "%":
                    result = new FlipFlopModule(match.Groups["id"].Value, match.Groups["outputmodules"].Value.Split(", "));
                    break;
                case "&":
                    result = new ConjunctionModule(match.Groups["id"].Value, match.Groups["outputmodules"].Value.Split(", "));
                    break;
                default:
                    result = new BroadcasterModule(match.Groups["id"].Value, match.Groups["outputmodules"].Value.Split(", "));
                    break;
            }
            AllModules.Add(result.Id, result);
            return result;
        }

        public string Id { get; set; } = id;
        public string[] OutputModules { get; set; } = outputModules;

        public abstract IEnumerable<Pulse> HandlePulse(Pulse pulse);

        public virtual void Initialize() { }
    }
}
