using _2025_10_02;

List<Machine> machines = File.ReadAllLines("input.txt").Select(Machine.FromString).ToList();
int result = 0;
Console.WriteLine(result);