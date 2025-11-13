using System.Text.RegularExpressions;

namespace Day08Puzzle01
{
    internal static partial class RegexHelper
    {
        [GeneratedRegex("\\\\x(?<hexcode>[0-9a-f]{2})")]
        internal static partial Regex HexcodeRegex();
    }
}
