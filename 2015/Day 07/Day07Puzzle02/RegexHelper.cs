using System.Text.RegularExpressions;

namespace Day07Puzzle02
{
    internal static partial class RegexHelper
    {
        [GeneratedRegex("^((?<staticInput>((\\d*)|([a-z]*)))|((?<leftOperand>((\\d*)|([a-z]*))) (?<operator>((AND)|(OR)|(LSHIFT)|(RSHIFT))) (?<rightOperand>((\\d*)|([a-z]*))))|(NOT (?<notOperand>[a-z]*))) -> (?<wireId>.*)")]
        internal static partial Regex WireRegex();
    }
}
