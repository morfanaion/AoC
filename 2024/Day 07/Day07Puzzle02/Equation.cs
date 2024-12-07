namespace Day07Puzzle02
{
	internal class Equation(long[] numbers)
	{
		public static Equation FromString(string value)
		{
			string[] parts = value.Split(": ", StringSplitOptions.RemoveEmptyEntries);
			return new Equation(parts[1].Split(' ').Select(long.Parse).ToArray())
			{
				Test = long.Parse(parts[0])
			};
		}

		public long Test { get; set; }
		public long[] Numbers { get; set; } = numbers;

		public bool PossiblyTrue => PossibleRecurse(Numbers[0], Numbers.Skip(1));

		private bool PossibleRecurse(long resultTillNow, IEnumerable<long> numbersToProcess)
		{
			if (resultTillNow == Test && !numbersToProcess.Any())
			{
				return true;
			}
			if(resultTillNow > Test)
			{
				return false;
			}
			if(numbersToProcess.Any() && 
				(
					PossibleRecurse(resultTillNow + numbersToProcess.First(), numbersToProcess.Skip(1)) ||
					PossibleRecurse(resultTillNow * numbersToProcess.First(), numbersToProcess.Skip(1)) ||
					PossibleRecurse(long.Parse($"{resultTillNow}{numbersToProcess.First()}"), numbersToProcess.Skip(1))))
			{
				return true;
			}
			return false;
		}
	}
}
