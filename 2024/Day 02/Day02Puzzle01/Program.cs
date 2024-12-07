using Day02Puzzle01;

Console.WriteLine(File.ReadAllLines("input.txt").Select(Report.FromString).Count(r => r.Safe));