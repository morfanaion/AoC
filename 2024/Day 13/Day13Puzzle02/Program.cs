// See https://aka.ms/new-console-template for more information
using Day13Puzzle02;

Regex buttonRegex = RegexHelper.GetButtonRegex();
Regex prizeRegex = RegexHelper.GetPrizeRegex();
string[] inputLines = File.ReadAllLines("input.txt");
List<ClawMachine> machines = new List<ClawMachine>();
for(int i = 0; i < inputLines.Length; i+=4)
{
	Match buttonAMatch = buttonRegex.Match(inputLines[i]);
	Match buttonBMatch = buttonRegex.Match(inputLines[i+1]);
	Match prizeMatch = prizeRegex.Match(inputLines[i+2]);

	machines.Add(new ClawMachine()
	{
		AChangeX = long.Parse(buttonAMatch.Groups["Xchange"].Value),
		AChangeY = long.Parse(buttonAMatch.Groups["Ychange"].Value),
		BChangeX = long.Parse(buttonBMatch.Groups["Xchange"].Value),
		BChangeY = long.Parse(buttonBMatch.Groups["Ychange"].Value),
		PrizeX = 10000000000000 + long.Parse(prizeMatch.Groups["X"].Value),
		PrizeY = 10000000000000 + long.Parse(prizeMatch.Groups["Y"].Value)
	});
}

Console.WriteLine(machines.Where(m => m.Solve()).Sum(m => m.CoinCost));