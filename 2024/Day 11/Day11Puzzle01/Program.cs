// See https://aka.ms/new-console-template for more information
using Day11Puzzle01;
Stone? first = null;
Stone? previous = null;
foreach(long number in File.ReadAllText("input.txt").Split(' ').Select(long.Parse))
{
    Stone current = new Stone() { Number = number };
    if(first == null)
    {
        first = current;
    }
    if (previous != null)
    {
        current.SetPrevious(previous);
    }
    previous = current;
}
if (first != null)
{
    for (int i = 0; i < 75; i++)
    {
        Stone.Blink(first);
    }
    Console.WriteLine(first.AllSubsequentStones.Count());
}
