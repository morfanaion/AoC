using System.Text.RegularExpressions;

namespace Day03Puzzle01
{
    internal partial class RegexHelper
    {
        [GeneratedRegex("mul\\((?<num1>[0-9]{1,3}),(?<num2>[0-9]{1,3})\\)")]
        public static partial Regex MulRegex();
    }
}
