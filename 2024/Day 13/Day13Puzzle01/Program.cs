// See https://aka.ms/new-console-template for more information
using Day13Puzzle01;
using System.Text.RegularExpressions;

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
		AChangeX = int.Parse(buttonAMatch.Groups["Xchange"].Value),
		AChangeY = int.Parse(buttonAMatch.Groups["Ychange"].Value),
		BChangeX = int.Parse(buttonBMatch.Groups["Xchange"].Value),
		BChangeY = int.Parse(buttonBMatch.Groups["Ychange"].Value),
		PrizeX = int.Parse(prizeMatch.Groups["X"].Value),
		PrizeY = int.Parse(prizeMatch.Groups["Y"].Value)
	});
}

Console.WriteLine(machines.Where(m => m.Solve()).Sum(m => m.CoinCost));