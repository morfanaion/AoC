using Day03Puzzle02;

Dictionary<(int x, int y), int> housesDictionary = new Dictionary<(int x, int y), int>
{
    { (0, 0), 2 }
};

Santa currentSanta = new Santa();
Santa otherSanta = new Santa();

int x = 0;
int y = 0;
foreach (char c in File.ReadAllText("Input.txt"))
{
    switch (c)
    {
        case '<':
            currentSanta.X--;
            break;
        case '>':
            currentSanta.X++;
            break;
        case '^':
            currentSanta.Y--;
            break;
        case 'v':
            currentSanta.Y++;
            break;
    }
    if (housesDictionary.ContainsKey((currentSanta.X, currentSanta.Y)))
    {
        housesDictionary[(currentSanta.X, currentSanta.Y)]++;
    }
    else
    {
        housesDictionary[(currentSanta.X, currentSanta.Y)] = 1;
    }
    Santa temp = currentSanta;
    currentSanta = otherSanta;
    otherSanta = temp;
}
Console.WriteLine(housesDictionary.Count);