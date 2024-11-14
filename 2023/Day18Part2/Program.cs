using System.Text.RegularExpressions;

namespace Day18Part2
{

    public class Program
    {
        public static void Main(string[] args)
        {
            List<DugTrenchPart> trench = new List<DugTrenchPart>();
            long currentX = 0;
            long currentY = 0;
            RegexHelper helper = new RegexHelper();
            Func<long> yIncrementer = () => currentY;
            Func<long> xIncrementer = () => currentX;
            DugTrenchPart previous = new DugTrenchPart() { X = 0, Y = 0, Type = TrenchPartType.LeftDown };
            trench.Add(previous);
            foreach (string line in File.ReadAllLines("input.txt"))
            {
                Match match = helper.LineRegex().Match(line);
                TrenchPartType type = TrenchPartType.Horizontal;
                switch (match.Groups["direction"].Value[0])
                {
                    case '3':
                        type = TrenchPartType.Vertical;
                        xIncrementer = () => currentX;
                        yIncrementer = () => --currentY;
                        break;
                    case '0':
                        type = TrenchPartType.Horizontal;
                        xIncrementer = () => ++currentX;
                        yIncrementer = () => currentY;
                        break;
                    case '1':
                        type = TrenchPartType.Vertical;
                        xIncrementer = () => currentX;
                        yIncrementer = () => ++currentY;
                        break;
                    case '2':
                        type = TrenchPartType.Horizontal;
                        xIncrementer = () => --currentX;
                        yIncrementer = () => currentY;
                        break;
                }
                long numSteps = Convert.ToInt64(match.Groups["hexdistance"].Value, 16);

                for (long i = 0; i < numSteps; i++)
                {
                    DugTrenchPart newPart = new DugTrenchPart() { X = xIncrementer(), Y = yIncrementer(), Type = type };
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
                    trench.First().Type = previous.Type;
                    previous = previous.Previous;
                    trench.RemoveAt(trench.Count - 1);
                }
                previous.SetNext(trench.First());
                trench.First().Type = previous.Type;
                trench.First().SetNext(trench.First().Next);
            }
            long count = 0;
            foreach (var group in trench.GroupBy(tp => tp.Y))
            {
                bool inside = false;
                bool lastStartWasDown = false;
                bool goOutsideIfSame = false;
                long startX = 0;
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