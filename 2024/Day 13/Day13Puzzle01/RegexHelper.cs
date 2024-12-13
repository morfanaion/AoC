using System.Text.RegularExpressions;

namespace Day13Puzzle01
{
	internal partial class RegexHelper
	{
		[GeneratedRegex("Button [AB]: X\\+(?<Xchange>\\d*), Y\\+(?<Ychange>\\d*)")]
		public static partial Regex GetButtonRegex();

		[GeneratedRegex("Prize: X=(?<X>\\d*). Y=(?<Y>\\d*)")]
		public static partial Regex GetPrizeRegex();
	}
}
