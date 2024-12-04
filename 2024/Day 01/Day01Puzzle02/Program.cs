List<long> list1 = new List<long>();
List<long> list2 = new List<long>();
foreach (string line in File.ReadAllLines("input.txt"))
{
    string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    list1.Add(long.Parse(parts[0]));
    list2.Add(long.Parse(parts[1]));
}
long sum = 0;
foreach (long location in list1)
{
    sum += location * list2.Count(l => l == location);
}
Console.WriteLine(sum);