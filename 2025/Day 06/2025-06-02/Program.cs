using _2025_06_02;

string[] input = File.ReadAllLines("input.txt");
int startIdx = 0;
long total = 0;
for(int idx = 0; idx < input[0].Length; idx++)
{
    if(input.All(s => s[idx] == ' '))
    {
        Problem problem = new Problem(input.Select(line => string.Join("", line.Substring(startIdx, idx - startIdx))));
        total += problem.Solve();
        startIdx = idx + 1;
    }
}
Problem lastProblem = new Problem(input.Select(line => string.Join("", line.Substring(startIdx, input[0].Length - startIdx))));
total += lastProblem.Solve();
Console.WriteLine(total);
/*
string[][] input = ;
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
    int currentIdx = 0;
    string test = "Test";

    IEnumerable<char> numberBits = input.Skip(1).Select(line => line[x].Skip(currentIdx).Take(1).SelectMany(l => l);
    do
    { 
        currentNumber = process(currentNumber, long.Parse(string.Join("", numberBits)));
        numberBits = input.Skip(1).Select(line => line[x].Skip(currentIdx).Take(1)).Where(l => l.Any()).SelectMany(l => l);
    }
    while (numberBits.Any());
    total += currentNumber;
}
Console.WriteLine(total);

IEnumerable<string> ParseInput(string input)
{
    switch(input[0])
    {
        case '+':
        case '*':
            foreach(string @operator in input.Split(' ', StringSplitOptions.RemoveEmptyEntries))
            {
                yield return @operator;
            }
            yield break;
    }

    char lastChar = input[0];
    string currentString = string.Empty;
    for(int i = 0; i < input.Length; i++)
    {
        char newChar = input[i];
        if(lastChar == ' ' && char.IsDigit(newChar))
        {
            yield return currentString;
            currentString = string.Empty;
        }
        else
        {
            currentString += lastChar;
        }
        lastChar = newChar;
    }
    currentString += lastChar;
    yield return currentString;
}
*/