using Day02Puzzle02;

Console.WriteLine(File.ReadAllLines("input.txt").Select(Report.FromString).Count(r => r.Safe));