// See https://aka.ms/new-console-template for more information

using Day01Puzzle02;

Console.WriteLine(File.ReadAllText("Input.txt").Split("\r\n\r\n").Select(s => new ElfInventory(s)).OrderByDescending(elfInventory => elfInventory.NumCalories).Take(3).Sum(elfInventory => elfInventory.NumCalories));