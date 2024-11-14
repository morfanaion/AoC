// See https://aka.ms/new-console-template for more information

int currentCycle = 0;
List<int> signalStrengths = new List<int>();
int x = 1;

foreach(string line in File.ReadAllLines("Input.txt"))
{
    string[] command = line.Split(' ');
    switch(command[0])
    {
        case "noop":
            Tick();
            break;
        case "addx":
            Tick();
            Tick();
            x += (int.Parse(command[1]));
            break;
    }
}

Console.WriteLine(signalStrengths.Take(6).Sum());


void Tick()
{
    currentCycle++;
    if(currentCycle % 40 == 20)
    {
        signalStrengths.Add(x * currentCycle);
    }
}