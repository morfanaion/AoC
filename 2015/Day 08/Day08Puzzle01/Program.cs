using Day08Puzzle01;
using System.Text.RegularExpressions;

int totalCodestringLength = 0;
int totalParsedStringLength = 0;
foreach (string str in File.ReadAllLines("input.txt"))
{
    totalCodestringLength += str.Length;
    string actualString = str
        .Substring(1, str.Length - 2)
        .Replace(@"\\", @"\")
        .Replace("\\\"", "\"");

    string result = actualString;
    foreach(Match match in RegexHelper.HexcodeRegex().Matches(actualString))
    {
        result = result.Replace(match.Value, ((char)Convert.ToInt32(match.Groups["hexcode"].Value, 16)).ToString());
    }
    totalParsedStringLength += result.Length;
}
Console.WriteLine(totalCodestringLength - totalParsedStringLength);