using System.Text.RegularExpressions;

namespace TestDijkstra
{
	internal static partial class RegexHelper
	{
		[GeneratedRegex("(?<id>\\d*) (?<x>\\d*) (?<y>\\d*) (?<connected>(\\d* ?)*)")]
		public static partial Regex NodeRegex();
	}
}
