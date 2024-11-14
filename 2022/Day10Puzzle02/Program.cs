// See https://aka.ms/new-console-template for more information
// See https://aka.ms/new-console-template for more information

int currentCycle = 0;
List<int> signalStrengths = new List<int>();
int x = 1;
string screen = string.Empty;

foreach (string line in File.ReadAllLines("Input.txt"))
{
    string[] command = line.Split(' ');
    switch (command[0])
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

Console.Write(screen);


void Tick()
{
    if(currentCycle % 40 >= (x-1) && currentCycle % 40 <= (x + 1))
    {
        screen += "#";
    }
    else
    {
        screen += ".";
    }
    currentCycle++;
    if(currentCycle % 40 == 0)
    {
        screen += "\n";
    }
}
