using System.Text.RegularExpressions;

namespace Day19Part2
{
    internal class Workflow
    {
        public static Dictionary<string, Workflow> Workflows = new Dictionary<string, Workflow>();
        public Workflow(string definition)
        {
            Match match = RegexHelper.Instance.WorkflowRegex().Match(definition);
            Id = match.Groups["workflowname"].Value;
            Rules = match.Groups["rules"].Value.Split(",").Select(Rule.FromDefinition).ToList();
        }

        public string Id { get; }
        public List<Rule> Rules { get; }

        public long GetNumAccepted(WorkingRange workingRange)
        {
            long result = 0;
            foreach (var rule in Rules)
            {
                (WorkingRange Range, long numAccepted) ruleResult = rule.GetNumAccepted(workingRange);
                workingRange = ruleResult.Range;
                result += ruleResult.numAccepted;
            }
            return result;
        }
    }

    
}
