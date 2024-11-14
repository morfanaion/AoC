// See https://aka.ms/new-console-template for more information
using Day20Puzzle01;
Number? previous = null;
List<Number> numbers = File.ReadAllLines("Input.txt").Select(line => (previous = new Number(long.Parse(line), previous))).ToList();

for (int i = 0; i < 10; i++)
{
    foreach (Number number in numbers)
    {
        long distance = number.Value % (numbers.Count - 1);
        if (distance != 0)
        {
            Number destination = (distance > 0) ? number : number.Prev;
            Number.RemoveNumber(number);
            Action increment = distance > 0 ? () => destination = destination.Next : () => destination = destination.Prev;
            for (int j = 0; j < Math.Abs(distance); j++)
            {
                increment();
            }
            Number.InsertNumber(number, destination);
        }
    }
}

Number zero = numbers.Single(numbers => numbers.Value == 0);
long sum = 0;
for (int i = 1; i <= 3000; i++)
{
    zero = zero.Next;
    if (i % 1000 == 0)
    {
        sum += zero.Value;
    }
}
Console.WriteLine(sum);