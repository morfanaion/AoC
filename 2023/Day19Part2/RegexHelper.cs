using System.Text.RegularExpressions;

namespace Day19Part2
{
    internal partial class RegexHelper
    {
        private static RegexHelper? _instance;
        public static RegexHelper Instance => _instance ?? (_instance = new RegexHelper());

        [GeneratedRegex("(?<workflowname>[a-z]*){(?<rules>([a-z\\<\\>0-9:AR]*,?)*)}")]
        public partial Regex WorkflowRegex();

        [GeneratedRegex("(?<start>[a-zAR]*)((?<operator>[\\<\\>])(?<number>[0-9]*):(?<destination>[ARa-z]*))?")]
        public partial Regex RuleRegex();

        [GeneratedRegex("(?<var>[xmas])=(?<value>[0-9]*)")]
        public partial Regex PartValueRegex();
    }
}
