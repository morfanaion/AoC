using System.Collections;

Dictionary<char, List<(int, int)>> antennas = new Dictionary<char, List<(int, int)>>();
int maxX = -1;
int maxY = -1;
foreach(string line in File.ReadAllLines("input.txt"))
{
    maxY++;
    maxX = -1;
    foreach (char c in line)
    {
        maxX++;
        if (c != '.')
        {
            if (!antennas.ContainsKey(c))
            {
                antennas.Add(c, new List<(int, int)> { (maxX, maxY) });
            }
            else
            {
                antennas[c].Add((maxX, maxY));
            }
        }
        
    }
    
}
Dictionary<(int, int), int> antinodes = new Dictionary<(int, int), int>();
foreach(KeyValuePair<char, List<(int, int)>> kvp in antennas)
{
    int i = 0;
    foreach((int x1, int y1) in kvp.Value)
    {
        i++;
        foreach((int x2, int y2) in kvp.Value.Skip(i))
        {
            processAntiNode(x1, y1);
            processAntiNode(x2, y2);
            int dx = x2 - x1;
            int dy = y2 - y1;
            int antiNodeX = x2 + dx;
            int antiNodeY = y2 + dy;
            while (antiNodeX <= maxX && antiNodeY <= maxY && antiNodeX >= 0 && antiNodeY >= 0)
            {
                processAntiNode(antiNodeX, antiNodeY);
                antiNodeX += dx;
                antiNodeY += dy;
            }
            antiNodeX = x1 - dx;
            antiNodeY = y1 - dy;
            while (antiNodeX <= maxX && antiNodeY <= maxY && antiNodeX >= 0 && antiNodeY >= 0)
            {
                processAntiNode(antiNodeX, antiNodeY);
                antiNodeX -= dx;
                antiNodeY -= dy;
            }
        }
    }
}
Console.WriteLine(antinodes.Count);

void processAntiNode(int x, int y)
{
    if (!antinodes.ContainsKey((x, y)))
    {
        antinodes.Add((x, y), 1);
    }
    else
    {
        antinodes[(x, y)]++;
    }
}