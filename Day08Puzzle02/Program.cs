int totalCodestringLength = 0;
int totalParsedStringLength = 0;
foreach (string str in File.ReadAllLines("input.txt"))
{
    totalCodestringLength += str.Length;
    string actualString = "\"" + str
        .Replace(@"\", @"\\")
        .Replace("\"", "\\\"") + "\"";

    totalParsedStringLength += actualString.Length;
}
Console.WriteLine(totalParsedStringLength - totalCodestringLength);