using Day02Puzzle01;

Console.WriteLine(File.ReadAllLines("input.txt").Select(Gift.FromString).Sum(g => g.RibbonSize));