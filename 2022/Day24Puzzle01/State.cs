// See https://aka.ms/new-console-template for more information
using Day24Puzzle01;

internal class State
{
    public int Minute { get; set; }
    public Tile CurrentTile { get; set; }
    public string Path { get; set; } = string.Empty;

    public State(Tile currentTile)
    {
        CurrentTile = currentTile;
    }
}