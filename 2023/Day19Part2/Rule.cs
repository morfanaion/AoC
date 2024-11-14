using System.Text.RegularExpressions;

namespace Day19Part2
{
    internal abstract class Rule
    {

        public string Destination { get; set; }
        public Rule(string destination)
        {
            Destination = destination;
        }

        public static Rule FromDefinition(string definition)
        {
            Match match = RegexHelper.Instance.RuleRegex().Match(definition);
            string op = match.Groups["operator"].Value;
            switch(op)
            {
                case "":
                    return new StraightDestinationRule(match.Groups["start"].Value);
                case ">":
                    return new GreaterThanRule(int.Parse(match.Groups["number"].Value), match.Groups["start"].Value, match.Groups["destination"].Value);
                case "<":
                    return new LesserThanRule(int.Parse(match.Groups["number"].Value), match.Groups["start"].Value, match.Groups["destination"].Value);
            }
            throw new InvalidOperationException($"Unknown operator {op}");
        }

        public abstract (WorkingRange otherRange, long numAccepted) GetNumAccepted(WorkingRange range);
    }
}
