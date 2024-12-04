List<long> list1 = new List<long>();
List<long> list2 = new List<long>();
foreach (string line in File.ReadAllLines("input.txt"))
{
    string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    list1.Add(long.Parse(parts[0]));
    list2.Add(long.Parse(parts[1]));
}
list1.Sort(); 
list2.Sort();
long sum = 0;
foreach((long left, long right) in list1.Zip(list2))
{
    sum += Math.Abs(left - right);
}
Console.WriteLine(sum);