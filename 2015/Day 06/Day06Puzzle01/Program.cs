// See https://aka.ms/new-console-template for more information
using Day06Puzzle01;
using System.Text.RegularExpressions;

Console.WriteLine("Hello, World!");

bool[] lightGrid = new bool[1000000];

Regex instructionRegex = RegexHelper.GetInstructionRegex();
foreach(string instruction in File.ReadAllLines("input.txt"))
{
    Match instructionMatch = instructionRegex.Match(instruction);
    if(!instructionMatch.Success )
    {
        Console.WriteLine("Eeeek, mismatch: " + instruction);
        return;
    }
    int x1 = int.Parse(instructionMatch.Groups["x1"].Value);
    int x2 = int.Parse(instructionMatch.Groups["x2"].Value);
    int y1 = int.Parse(instructionMatch.Groups["y1"].Value);
    int y2 = int.Parse(instructionMatch.Groups["y2"].Value);
    Func<bool, bool> getNewValue = _ => false;
    switch (instructionMatch.Groups["instruction"].Value)
    {
        case "turn on":
            getNewValue = _ => true;
            break;
        case "turn off":
            getNewValue = _ => false;
            break;
        case "toggle":
            getNewValue = b => !b;
            break;
    }
    for(int y = y1; y <= y2; y++)
    {
        for(int x = x1; x <= x2; x++)
        {
            int idx = y * 1000 + x;
            lightGrid[idx] = getNewValue(lightGrid[idx]);
        }
    }
}
Console.WriteLine(lightGrid.Count(b => b));