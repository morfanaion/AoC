using System.Text.RegularExpressions;

namespace Day09Puzzle01
{
    internal static partial class RegexHelper
    {
        [GeneratedRegex("(?<city1>[A-Za-z]*) to (?<city2>[A-Za-z]*) = (?<distance>\\d*)")]
        public static partial Regex DistancesRegex();
    }
}
