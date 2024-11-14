using Day21Puzzle01;
using System.Text.RegularExpressions;

Regex lineRegex = new Regex(@"(?<id>[a-z]{4}): (?<definition>(([a-z]{4} . [a-z]{4})|(\d*)))");
Regex definitionRegex = new Regex(@"(?<othermonkey1id>[a-z]{4}) (?<operator>.) (?<othermonkey2id>[a-z]{4})");
foreach (string line in File.ReadAllLines("Input.txt"))
{
    Match lineMatch = lineRegex.Match(line);
    string id = lineMatch.Groups["id"].Value;
    string definition = lineMatch.Groups["definition"].Value;
    if (long.TryParse(definition, out long number))
    {
        IMonkey.Monkeys.Add(id, new NumberMonkey(id, number));
    }
    else
    {
        Match definitionMatch = definitionRegex.Match(definition);
        switch(definitionMatch.Groups["operator"].Value)
        {
            case "+":
                IMonkey.Monkeys.Add(id, new AdditionMonkey(id, definitionMatch.Groups["othermonkey1id"].Value, definitionMatch.Groups["othermonkey2id"].Value));
                break;
            case "-":
                IMonkey.Monkeys.Add(id, new SubtractionMonkey(id, definitionMatch.Groups["othermonkey1id"].Value, definitionMatch.Groups["othermonkey2id"].Value));
                break;
            case "*":
                IMonkey.Monkeys.Add(id, new MultiplicationMonkey(id, definitionMatch.Groups["othermonkey1id"].Value, definitionMatch.Groups["othermonkey2id"].Value));
                break;
            case "/":
                IMonkey.Monkeys.Add(id, new DivisionMonkey(id, definitionMatch.Groups["othermonkey1id"].Value, definitionMatch.Groups["othermonkey2id"].Value));
                break;
        }
    }
}


Console.WriteLine(IMonkey.Monkeys["root"].GetNumber());