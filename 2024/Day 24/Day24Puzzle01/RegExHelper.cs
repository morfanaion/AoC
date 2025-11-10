namespace Day24Puzzle01
{
	internal static partial class RegExHelper
	{
		[GeneratedRegex("(?<gateId>[a-z0-9]{3}): (?<output>1|0|)")]
		public static partial Regex GetStaticGateRegex();

		[GeneratedRegex("(?<gate1>[a-z0-9]{3}) (?<operator>AND|OR|XOR) (?<gate2>[a-z0-9]{3}) -> (?<gateId>[a-z0-9]{3})")]
		public static partial Regex GetNonStaticGateRegex();
	}
}
