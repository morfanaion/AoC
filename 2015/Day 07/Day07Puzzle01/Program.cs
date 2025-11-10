using Day07Puzzle01;
using System.Text.RegularExpressions;

Regex wireRegex = RegexHelper.WireRegex();
foreach (string wireDefinition in File.ReadAllLines("input.txt"))
{
    Match match = wireRegex.Match(wireDefinition);
    if(!match.Success)
    {
        Console.WriteLine("Mismatch: " + wireDefinition);
        return;
    }
    string currentOperator = match.Groups["operator"].Value;
    if(string.IsNullOrEmpty(currentOperator) )
    {
        if (!string.IsNullOrEmpty(match.Groups["notOperand"].Value))
        {
            currentOperator = "NOT";
        }
    }
    switch (currentOperator)
    {
        case "AND":
            _ = new Wire(match.Groups["wireId"].Value, new AndGate() { LeftWireId = match.Groups["leftOperand"].Value, RightWireId = match.Groups["rightOperand"].Value });
            break;
        case "OR":
            _ = new Wire(match.Groups["wireId"].Value, new OrGate() { LeftWireId = match.Groups["leftOperand"].Value, RightWireId = match.Groups["rightOperand"].Value });
            break;
        case "LSHIFT":
            _ = new Wire(match.Groups["wireId"].Value, new LeftShiftGate() { LeftWireId = match.Groups["leftOperand"].Value, ShiftAmount = byte.Parse(match.Groups["rightOperand"].Value) });
            break;
        case "RSHIFT":
            _ = new Wire(match.Groups["wireId"].Value, new RightShiftGate() { LeftWireId = match.Groups["leftOperand"].Value, ShiftAmount = byte.Parse(match.Groups["rightOperand"].Value) });
            break;
        case "NOT":
            _ = new Wire(match.Groups["wireId"].Value, new NotGate() { InputWireId = match.Groups["notOperand"].Value });
            break;
        default:
            _ = new Wire(match.Groups["wireId"].Value, new StaticInput() { Value = match.Groups["staticInput"].Value });
            break;
    }
        
    /*if(match.Groups.ContainsKey("notOperand"))
    {
        _ = new Wire(match.Groups["wireId"].Value, new NotGate() { InputWireId = match.Groups["notOperand"].Value });
        continue;
    }*/
    
}

Console.WriteLine(Wire.Wires["a"].GetValue());
