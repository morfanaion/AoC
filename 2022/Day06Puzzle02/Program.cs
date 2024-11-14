// See https://aka.ms/new-console-template for more information
string test = File.ReadAllText("Input.txt");
int index = 0;
while (test.Skip(index).Take(14).Distinct().Count() != 14)
{
    index++;
}
Console.WriteLine($"New way: {index + 14}");
