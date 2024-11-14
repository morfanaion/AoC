// See https://aka.ms/new-console-template for more information
using Day13Puzzle02;

List<Item> lists = new List<Item>();
foreach (string line in File.ReadAllLines("Input.txt").Append("[[2]]").Append("[[6]]"))
{
    if (string.IsNullOrWhiteSpace(line))
    {
        continue;
    }
    bool inserted = false;
    Item newList = new Item(ToList(line.Substring(1, line.Length - 1))) { OriginalString = line };
    for (int i = 0; i < lists.Count; i++)
    {
        if (CompareLeftToRight(lists[i], newList) == 1)
        {
            lists.Insert(i, newList);
            inserted = true;
            break;
        }

    }
    if (!inserted)
    {
        lists.Add(newList);
    }
}

int product = 1;
for(int i = 0; i < lists.Count; i++)
{
    if(lists[i].OriginalString == "[[2]]" || lists[i].OriginalString == "[[6]]")
    {
        product *= (i + 1);
    }
}
Console.WriteLine(product);

List<object> ToList(string line)
{
    List<object> result = new List<object>();
    string numberString = string.Empty;
    int incrementer = 0;
    while (!line.StartsWith(']'))
    {
        switch (line[0])
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
    if (numberString != String.Empty)
    {
        result.Add(int.Parse(numberString));
    }
    return result;
}

int FindIndexOfClosingBracket(string line)
{
    int numOpenBrackets = 0;
    for (int i = 0; i < line.Length; i++)
    {
        switch (line[i])
        {
            case '[':
                numOpenBrackets++;
                break;
            case ']':
                numOpenBrackets--;
                break;
        }
        if (numOpenBrackets == 0)
        {
            return i;
        }
    }
    return -1;
}

int CompareLeftToRight(List<object> left, List<object> right)
{
    int listSize = Math.Max(left.Count, right.Count);
    for (int i = 0; i < listSize; i++)
    {
        if (i == left.Count)
        {
            return -1;
        }
        if (i == right.Count)
        {
            return 1;
        }
        if (left[i] is int iLeft && right[i] is int iRight)
        {
            if (iLeft < iRight)
            {
                return -1;
            }
            if (iLeft > iRight)
            {
                return 1;
            }
        }
        if (left[i] is List<object> subLeft && right[i] is List<object> subRight)
        {
            switch (CompareLeftToRight(subLeft, subRight))
            {
                case -1:
                    return -1;
                case 1:
                    return 1;
            }
        }
        if (left[i] is List<object> subLeft2 && right[i] is int iRight2)
        {
            switch (CompareLeftToRight(subLeft2, new List<object>() { iRight2 }))
            {
                case -1:
                    return -1;
                case 1:
                    return 1;
            }
        }
        if (left[i] is int iLeft2 && right[i] is List<object> subRight2)
        {
            switch (CompareLeftToRight(new List<object>() { iLeft2 }, subRight2))
            {
                case -1:
                    return -1;
                case 1:
                    return 1;
            }
        }
    }
    return 0;
}