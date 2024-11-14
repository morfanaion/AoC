using System.Text.RegularExpressions;

namespace Day19Part1
{
    internal class Workflow
    {
        public Workflow(string definition)
        {
            Match match = RegexHelper.Instance.WorkflowRegex().Match(definition);
            Id = match.Groups["workflowname"].Value;
            Rules = match.Groups["rules"].Value.Split(",").Select(Rule.FromDefinition).ToList();
        }

        public string Id { get; }
        public List<Rule> Rules { get; }

        internal void Process(Part part, Action<Part, string> enqueue)
        {
            foreach(Rule rule in Rules)
            {
                if(rule.Process(part, enqueue))
                {
                    return;
                }
            }
        }
    }
}
