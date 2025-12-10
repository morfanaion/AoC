using _2025_10_01;

List<Machine> machines = File.ReadAllLines("input.txt").Select(Machine.FromString).ToList();

Console.WriteLine(machines.Sum(m => m.FindMinNumPressesToGetTargetIndicators()));