using Day03Puzzle01;
using System.Text.RegularExpressions;

long sum = 0;
foreach (string dos in File.ReadAllText("input.txt").Split("do()").Select(s => s.Split("don't()")[0]))
{
    foreach (Match match in RegexHelper.MulRegex().Matches(dos))
    {
        sum += long.Parse(match.Groups["num1"].Value) * long.Parse(match.Groups["num2"].Value);
    }
}
Console.WriteLine(sum);