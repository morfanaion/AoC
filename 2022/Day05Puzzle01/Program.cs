// See https://aka.ms/new-console-template for more information

using Day05Puzzle01;

List<string> lines = File.ReadAllLines("Input.txt").ToList();
int numLinesForStacks = lines.IndexOf(string.Empty);

List<CrateStack> stackList = CreateStackList(lines.Take(numLinesForStacks));

Regex captureRegex = new Regex(@"move (?<amount>\d*) from (?<source>\d*) to (?<target>\d*)");
foreach(string command in lines.Skip(numLinesForStacks + 1))
{
    Match match = captureRegex.Match(command);
    int amount = int.Parse(match.Groups["amount"].Value);
    int source = int.Parse(match.Groups["source"].Value);
    int target = int.Parse(match.Groups["target"].Value);
    CrateStack sourceStack = stackList.Single(stack => stack.StackNumber == source);
    CrateStack targetStack = stackList.Single(stack => stack.StackNumber == target);
    for(int i = 0; i < amount; i++)
    {
        targetStack.Push(sourceStack.Pop());
    }
}
Console.WriteLine(String.Concat(stackList.Select(s => s.First())));

List<CrateStack> CreateStackList(IEnumerable<string> enumerable)
{
    List<CrateStack> crateStacks = new List<CrateStack>();
    string stackNumbers = enumerable.Last();
    for(int i = 0; i <= stackNumbers.Length / 4; i++)
    {
        crateStacks.Add(new CrateStack() { StackNumber = int.Parse(stackNumbers[(i * 4) + 1].ToString()) });
    }
    foreach(string row in enumerable.Reverse().Skip(1))
    {
        for (int i = 0; i < crateStacks.Count; i++)
        {
            char c = row[(i * 4) + 1];
            if(c != ' ')
            crateStacks[i].Push(c);
        }
    }
    return crateStacks;
}