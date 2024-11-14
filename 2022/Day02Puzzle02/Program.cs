// See https://aka.ms/new-console-template for more information
Console.WriteLine(File.ReadAllLines("Input.txt").Sum(s => ChosenScore(s) + MatchScore(s)));

int MatchScore(string match)
{
    switch(match[2])
    {
        case 'X':
            return 0;
        case 'Y':
            return 3;
        case 'Z':
            return 6;
        default:
            throw new ArgumentException("Third character of 'match' has to be X, Y or Z");
    }
}

int ChosenScore(string match)
{
    switch (match[0])
    {
        case 'A': // rock
            switch (match[2])
            {
                case 'X': // scissors
                    return 3;
                case 'Y': // rock
                    return 1;
                case 'Z': // paper
                    return 2;
                default:
                    throw new ArgumentException("Third character of 'match' has to be X, Y or Z");
            }
        case 'B': // paper
            switch (match[2])
            {
                case 'X': // rock
                    return 1;
                case 'Y': // paper
                    return 2;
                case 'Z': // scissors
                    return 3;
                default:
                    throw new ArgumentException("Third character of 'match' has to be X, Y or Z");
            }
        case 'C': // scissors
            switch (match[2])
            {
                case 'X': // paper
                    return 2;
                case 'Y': // scissors
                    return 3;
                case 'Z': // rock
                    return 1;
                default:
                    throw new ArgumentException("Third character of 'match' has to be X, Y or Z");
            }
        default:
            throw new ArgumentException("First character of 'match' has to be A, B or C");
    }
}