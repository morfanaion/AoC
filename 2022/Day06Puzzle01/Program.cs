// See https://aka.ms/new-console-template for more information
string test = File.ReadAllText("Input.txt");
int index = 0;
while(test.Skip(index).Take(4).Distinct().Count() != 4)
{
    index++;
}
Console.WriteLine($"New way: {index + 4}");