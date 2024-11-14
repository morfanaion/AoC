// See https://aka.ms/new-console-template for more information

Console.WriteLine(File.ReadAllLines("Input.txt").Sum(s => ChosenItemScore(s) + MatchResult(s)));

int ChosenItemScore(string match)
{
    switch(match[2])
    {
        case 'X':
            return 1;
        case 'Y':
            return 2;
        case 'Z':
            return 3;
        default:
            throw new ArgumentException("Third character of 'match' has to be X, Y or Z");
    }
}

int MatchResult(string match)
{
    switch(match[0])
    {
        case 'A': // rock
            switch(match[2])
            {
                case 'X': // rock
                    return 3;
                case 'Y': // paper
                    return 6;
                case 'Z': // scissors
                    return 0;
                default:
                    throw new ArgumentException("Third character of 'match' has to be X, Y or Z");
            }
        case 'B': // paper
            switch (match[2])
            {
                case 'X': // rock
                    return 0;
                case 'Y': // paper
                    return 3;
                case 'Z': // scissors
                    return 6;
                default:
                    throw new ArgumentException("Third character of 'match' has to be X, Y or Z");
            }
        case 'C': // scissors
            switch (match[2])
            {
                case 'X': // rock
                    return 6;
                case 'Y': // paper
                    return 0;
                case 'Z': // scissors
                    return 3;
                default:
                    throw new ArgumentException("Third character of 'match' has to be X, Y or Z");
            }
        default:
            throw new ArgumentException("First character of 'match' has to be A, B or C");
    }
}