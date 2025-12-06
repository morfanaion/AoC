string[][] input = File.ReadAllLines("input.txt").Reverse().Select(line => line.Split(' ', StringSplitOptions.RemoveEmptyEntries)).ToArray();
long total = 0;
for(int x = 0; x < input[0].Length; x++)
{
    Func<long, long, long> process;
    long currentNumber;
    switch(input[0][x])
    {
        case "+":
            process = (current, number) => current + number;
            currentNumber = 0;
            break;
        case "*":
            process = (current, number) => current * number;
            currentNumber = 1;
            break;
        default:
            throw new InvalidDataException("Unknown operator");
    }
    for(int y = 1; y < input.Length; y++)
    {
        currentNumber = process(currentNumber, long.Parse(input[y][x]));
    }
    total += currentNumber;
}
Console.WriteLine(total);