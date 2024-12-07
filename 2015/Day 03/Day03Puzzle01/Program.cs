Dictionary<(int x, int y), int> housesDictionary = new Dictionary<(int x, int y), int>
{
    { (0, 0), 1 }
};

int x = 0;
int y = 0;
foreach(char c in File.ReadAllText("Input.txt"))
{
    switch(c)
    {
        case '<':
            x--;
            break;
        case '>':
            x++;
            break;
        case '^':
            y--;
            break;
        case 'v':
            y++;
            break;
    }
    if(housesDictionary.ContainsKey((x, y)))
    {
        housesDictionary[(x, y)]++;
    }
    else
    {
        housesDictionary[(x, y)] = 1;
    }
}
Console.WriteLine(housesDictionary.Count);