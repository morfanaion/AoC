using System.Text.RegularExpressions;

namespace Day19Part1
{
    internal abstract class Rule
    {
        public static Rule FromDefinition(string definition)
        {
            Match match = RegexHelper.Instance.RuleRegex().Match(definition);
            string op = match.Groups["operator"].Value;
            switch(op)
            {
                case "":
                    return new StraightDestinationRule(match.Groups["start"].Value);
                case ">":
                    return new GreaterThanRule(int.Parse(match.Groups["number"].Value), match.Groups["destination"].Value, GetterForValue(match.Groups["start"].Value));
                case "<":
                    return new LesserThanRule(int.Parse(match.Groups["number"].Value), match.Groups["destination"].Value, GetterForValue(match.Groups["start"].Value));
            }
            throw new InvalidOperationException($"Unknown operator {op}");
        }

        private static Func<Part, int> GetterForValue(string value)
        {
            switch(value)
            {
                case "x":
                    return part => part.X;
                case "m":
                    return part => part.M;
                case "a":
                    return part => part.A;
                case "s":
                    return part => part.S;
            }
            throw new ArgumentException("Unknown value");
        }

        internal abstract bool Process(Part part, Action<Part, string> enqueue);
    }
}
