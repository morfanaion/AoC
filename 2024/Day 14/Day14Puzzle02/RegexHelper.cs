using System.Text.RegularExpressions;

namespace Day14Puzzle02
{
	internal partial class RegexHelper
	{
		[GeneratedRegex("p=(?<x>\\d*),(?<y>\\d*) v=(?<dx>[-]?\\d*),(?<dy>[-]?\\d*)")]
		public static partial Regex RobotRegex();
	}
}
