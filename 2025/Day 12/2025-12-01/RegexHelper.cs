using System.Text.RegularExpressions;

namespace _2025_12_01
{
    internal static partial class RegexHelper
    {

        [GeneratedRegex("(?<x>\\d*)x(?<y>\\d*): (?<n1>\\d*) (?<n2>\\d*) (?<n3>\\d*) (?<n4>\\d*) (?<n5>\\d*) (?<n6>\\d*)")]
        internal static partial Regex TreeRegex();

        [GeneratedRegex("^(?<identifier>\\d):$")]
        internal static partial Regex PackageIdentifierRegex();
    }
}
