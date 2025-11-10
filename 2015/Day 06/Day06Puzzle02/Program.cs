// See https://aka.ms/new-console-template for more information
using Day06Puzzle02;
using System.Text.RegularExpressions;

Console.WriteLine("Hello, World!");

int[] lightGrid = new int[1000000];

Regex instructionRegex = RegexHelper.GetInstructionRegex();
foreach (string instruction in File.ReadAllLines("input.txt"))
{
    Match instructionMatch = instructionRegex.Match(instruction);
    if (!instructionMatch.Success)
    {
        Console.WriteLine("Eeeek, mismatch: " + instruction);
        return;
    }
    int x1 = int.Parse(instructionMatch.Groups["x1"].Value);
    int x2 = int.Parse(instructionMatch.Groups["x2"].Value);
    int y1 = int.Parse(instructionMatch.Groups["y1"].Value);
    int y2 = int.Parse(instructionMatch.Groups["y2"].Value);
    Func<int, int> getNewValue = _ => 0;
    switch (instructionMatch.Groups["instruction"].Value)
    {
        case "turn on":
            getNewValue = i => i + 1;
            break;
        case "turn off":
            getNewValue = i => Math.Max(i - 1, 0);
            break;
        case "toggle":
            getNewValue = i => i + 2;
            break;
    }
    for (int y = y1; y <= y2; y++)
    {
        for (int x = x1; x <= x2; x++)
        {
            int idx = y * 1000 + x;
            lightGrid[idx] = getNewValue(lightGrid[idx]);
        }
    }
}
Console.WriteLine(lightGrid.Sum(i => i));