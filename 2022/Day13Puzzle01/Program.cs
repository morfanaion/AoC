// See https://aka.ms/new-console-template for more information
using Day13Puzzle01;

List<Pair> pairs = new List<Pair>();
Pair currentPair = new Pair();
bool isLeft = true;
foreach (string line in File.ReadAllLines("Input.txt"))
{
    if(string.IsNullOrWhiteSpace(line))
    {
        pairs.Add(currentPair);
        currentPair = new Pair();
    }
    else
    {
        if(isLeft)
        {
            currentPair.Left = ToList(line.Substring(1, line.Length - 1));
            isLeft = false;
        }
        else
        {
            currentPair.Right = ToList(line.Substring(1, line.Length - 1));
            isLeft = true;
        }
    }
}
if(currentPair.Left != null && currentPair.Right != null)
{
    pairs.Add(currentPair);
}
int sum = 0;
for(int i = 0; i < pairs.Count; i++)
{
    if(pairs[i].IsRightOrder)
    {
        sum += (i + 1);
    }
}
Console.WriteLine(sum);

List<object> ToList(string line)
{
    List<object> result = new List<object>();
    string numberString = string.Empty;
    int incrementer = 0;
    while(!line.StartsWith(']'))
    {
        switch(line[0])
        {
            case '[':
                result.Add(ToList(line.Substring(1, line.Length - 1)));
                incrementer = FindIndexOfClosingBracket(line) + 1;
                break;
            case '0':
            case '1':
            case '2':
            case '3':
            case '4':
            case '5':
            case '6':
            case '7':
            case '8':
            case '9':
                numberString += line[0];
                incrementer = 1;
                break;
            case ',':
                if (numberString != string.Empty)
                {
                    result.Add(int.Parse(numberString));
                    numberString = string.Empty;
                }
                incrementer = 1;
                break;
        }
        line = line.Substring(incrementer, line.Length - incrementer);
    }
    if(numberString != String.Empty)
    {
        result.Add(int.Parse(numberString));
    }
    return result;
}

int FindIndexOfClosingBracket(string line)
{
    int numOpenBrackets = 0;
    for(int i = 0; i < line.Length; i++)
    {
        switch(line[i])
        {
            case '[':
                numOpenBrackets++;
                break;
            case ']':
                numOpenBrackets--;
                break;
        }
        if(numOpenBrackets == 0)
        {
            return i;
        }
    }
    return -1;
}