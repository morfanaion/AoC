using System.Text.RegularExpressions;

namespace Day06Puzzle01
{
    internal static partial class RegexHelper
    {
        [GeneratedRegex("(?<instruction>(turn on)|(turn off)|(toggle)) (?<x1>\\d*),(?<y1>\\d*) through (?<x2>\\d*),(?<y2>\\d*)")]
        internal static partial Regex GetInstructionRegex();
    }
}
