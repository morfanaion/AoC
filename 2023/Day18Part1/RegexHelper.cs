using System.Text.RegularExpressions;

namespace Day18Part1
{
    internal partial class RegexHelper
    {
        [GeneratedRegex("(?<direction>[LRDU]) (?<number>[0-9]{1,2}) \\(#(?<color>[0-9a-f]{6})\\)")]
        public partial Regex LineRegex();
    }
}
