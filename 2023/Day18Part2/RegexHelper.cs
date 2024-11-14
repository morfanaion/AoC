using System.Text.RegularExpressions;

namespace Day18Part2
{
    internal partial class RegexHelper
    {
        [GeneratedRegex("[LRDU] [0-9]{1,2} \\(#(?<hexdistance>[0-9a-f]{5})(?<direction>[0-3])\\)")]
        public partial Regex LineRegex();
    }
}
