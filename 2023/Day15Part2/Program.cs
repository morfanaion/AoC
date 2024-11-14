using Day15Part2;

internal class Program
{
    public static readonly char[] OperationChars = { '=', '-' };

    private static void Main(string[] args)
    {
        Dictionary<byte, Box> boxes = new Dictionary<byte, Box>();

        foreach (string instruction in File.ReadAllText("input.txt").Split(','))
        {
            byte boxNr = 0;
            int indexOfOperation = instruction.IndexOfAny(OperationChars);
            foreach (char c in instruction.Take(indexOfOperation))
            {
                boxNr += (byte)c;
                boxNr *= 17;
            }
            Box box = boxes.TryGetValue(boxNr, out Box? receivedBox) ? receivedBox : boxes[boxNr] = new Box() { Id = boxNr };
            Lens? lens;
            int lensIdx = -1;
            switch(instruction[indexOfOperation])
            {
                case '=':
                    lens = box.FirstOrDefault(l => MemoryExtensions.Equals(instruction.AsSpan(0, indexOfOperation), l.Label, StringComparison.Ordinal));
                    if(lens != null)
                    {
                        lens.FocalStrength = long.Parse(instruction.AsSpan(indexOfOperation + 1));
                    }
                    else
                    {
                        box.Add(new Lens() { Label = string.Join("", instruction.Take(indexOfOperation)), FocalStrength = long.Parse(instruction.AsSpan(indexOfOperation + 1)) });
                    }
                    break;
                case '-':
                    lensIdx = box.FindIndex(l => MemoryExtensions.Equals(instruction.AsSpan(0, indexOfOperation), l.Label, StringComparison.Ordinal));
                    if (lensIdx != -1)
                    {
                        box.RemoveAt(lensIdx);
                    }
                    break;
            }
        }
        Console.WriteLine(boxes.Values.Sum(b => b.FocusingPower));
    }
}