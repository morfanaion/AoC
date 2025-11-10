namespace Day18Part1
{

    public class Program
    {
        public static void Main(string[] args)
        {
            List<DugTrenchPart> trench = new List<DugTrenchPart>();
            int currentX = 0;
            int currentY = 0;
            RegexHelper helper = new RegexHelper();
            Func<int> yIncrementer = () => currentY;
            Func<int> xIncrementer = () => currentX;
            DugTrenchPart previous = new DugTrenchPart() { X = 0, Y = 0, Red = 0, Green = 0, Blue = 0, Type = TrenchPartType.LeftDown };
            trench.Add(previous);
            foreach (string line in File.ReadAllLines("input.txt"))
            {
                Match match = helper.LineRegex().Match(line);
                TrenchPartType type = TrenchPartType.Horizontal;
                switch (match.Groups["direction"].Value[0])
                {
                    case 'U':
                        type = TrenchPartType.Vertical;
                        xIncrementer = () => currentX;
                        yIncrementer = () => --currentY;
                        break;
                    case 'R':
                        type = TrenchPartType.Horizontal;
                        xIncrementer = () => ++currentX;
                        yIncrementer = () => currentY;
                        break;
                    case 'D':
                        type = TrenchPartType.Vertical;
                        xIncrementer = () => currentX;
                        yIncrementer = () => ++currentY;
                        break;
                    case 'L':
                        type = TrenchPartType.Horizontal;
                        xIncrementer = () => --currentX;
                        yIncrementer = () => currentY;
                        break;
                }
                int numSteps = int.Parse(match.Groups["number"].Value);
                string colorCode = match.Groups["color"].Value;
                byte red = Convert.ToByte(colorCode.Substring(0, 2), 16);
                byte green = Convert.ToByte(colorCode.Substring(2, 2), 16);
                byte blue = Convert.ToByte(colorCode.Substring(4, 2), 16);

                for (int i = 0; i < numSteps; i++)
                {
                    DugTrenchPart newPart = new DugTrenchPart() { X = xIncrementer(), Y = yIncrementer(), Red = red, Green = green, Blue = blue, Type = type };
                    if (previous != null)
                    {
                        previous.SetNext(newPart);
                    }
                    trench.Add(newPart);
                    previous = newPart;
                }
            }
            if (previous != null)
            {
                if (previous.X == 0 && previous.Y == 0)
                {
                    trench.First().ColorCode = previous.ColorCode;
                    trench.First().Type = previous.Type;
                    previous = previous.Previous;
                    trench.RemoveAt(trench.Count - 1);
                }
                previous.SetNext(trench.First());
                trench.First().Type = previous.Type;
                trench.First().SetNext(trench.First().Next);
            }
            int count = 0;
            foreach (var group in trench.GroupBy(tp => tp.Y))
            {
                bool inside = false;
                bool lastStartWasDown = false;
                bool goOutsideIfSame = false;
                int startX = 0;
                foreach (DugTrenchPart part in group.OrderBy(p => p.X))
                {
                    bool oldInside = inside;
                    switch (part.Type)
                    {
                        case TrenchPartType.Vertical:
                            inside = !inside;
                            break;
                        case TrenchPartType.RightDown:
                            if (!inside)
                            {
                                inside = true;
                                goOutsideIfSame = true;
                            }
                            else
                            {
                                goOutsideIfSame = false;
                            }
                            lastStartWasDown = true;
                            break;
                        case TrenchPartType.RightUp:
                            if (!inside)
                            {
                                inside = true;
                                goOutsideIfSame = true;
                            }
                            else
                            {
                                goOutsideIfSame = false;
                            }
                            lastStartWasDown = false;
                            break;
                        case TrenchPartType.LeftDown:
                            if (lastStartWasDown)
                            {
                                if (goOutsideIfSame)
                                {
                                    inside = false;
                                }
                            }
                            else
                            {
                                if (!goOutsideIfSame)
                                {
                                    inside = false;
                                }
                            }
                            break;
                        case TrenchPartType.LeftUp:
                            if (!lastStartWasDown)
                            {
                                if (goOutsideIfSame)
                                {
                                    inside = false;
                                }
                            }
                            else
                            {
                                if (!goOutsideIfSame)
                                {
                                    inside = false;
                                }
                            }
                            break;
                    }
                    if (inside != oldInside)
                    {
                        if (inside)
                        {
                            startX = part.X;
                        }
                        else
                        {
                            count += (1 + part.X - startX);
                        }
                    }
                }
            }
            Console.WriteLine(count);
        }


    }
}