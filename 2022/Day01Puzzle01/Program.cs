// See https://aka.ms/new-console-template for more information

using Day01Puzzle01;

Console.WriteLine(File.ReadAllText("Input.txt").Split("\r\n\r\n").Select(s => new ElfInventory(s)).Max(elfInventory => elfInventory.NumCalories));