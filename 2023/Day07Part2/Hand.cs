namespace Day07Part2
{
    internal class Hand : IComparable<Hand>
    {
        private byte[] _labels = new byte[0];
        public byte[] Labels {
            get => _labels;
            set
            {
                _labels = value;
            }
        }
        public long Bid { get; set; }
        public string HandString { get; set; }

        private HandTypes? _handType;
        public HandTypes HandType
        {
            get => _handType ??= DetermineHandType();
        }

        private HandTypes DetermineHandType()
        {
            int numJokers = Labels.Count(label => label == 1);
            if(numJokers == 5)
            {
                return HandTypes.FiveOfAKind;
            }
            List<byte> remaining = Labels.Where(label => label != 1).ToList();
            switch (remaining.Distinct().Count())
            {
                case 1:
                    return HandTypes.FiveOfAKind;
                case 2:
                    switch(numJokers)
                    {
                        case 0:
                            switch (remaining.Count(label => label == remaining.First()))
                            {
                                case 1:
                                case 4:
                                    return HandTypes.FourOfAKind;
                                default:
                                    return HandTypes.FullHouse;
                            }
                        case 1:
                            switch (remaining.Count(label => label == remaining.First()))
                            {
                                case 2:
                                    return HandTypes.FullHouse;
                                default:
                                    return HandTypes.FourOfAKind;
                            }
                        default:
                            return HandTypes.FourOfAKind;
                    }
                    
                case 3:
                    switch(numJokers)
                    {
                        case 0:
                            switch (remaining.GroupBy<byte, byte>(label => label).Max(group => group.Count()))
                            {
                                case 3:
                                    return HandTypes.ThreeOfAKind;
                                default:
                                    return HandTypes.TwoPair;
                            }
                        default:
                            return HandTypes.ThreeOfAKind;
                    }
                    
                case 4:
                    return HandTypes.OnePair;
                default:
                    return HandTypes.HighCard;
            }
        }

        public static Hand FromString(string str)
        {
            string[] parts = str.Split(' ');
            return new Hand()
            {
                Labels = parts[0].Select(CharToLabel).ToArray(),
                Bid = long.Parse(parts[1]),
                HandString = parts[0]
            };
        }

        private static byte CharToLabel(char c)
        {
            switch(c)
            {
                case 'A':
                    return 14;
                case 'K':
                    return 13;
                case 'Q':
                    return 12;
                case 'J':
                    return 1;
                case 'T':
                    return 10;
                default:
                    return (byte)(c - '0');
            }
        }

        public int CompareTo(Hand? other)
        {
            if (!(other is Hand otherHand)) throw new ArgumentException("Null not allowed");
            if(otherHand.HandType > HandType)
            {
                return -1;
            }
            if(otherHand.HandType < HandType)
            {
                return 1;
            }
            // equal handtype, time to order...
            for(int i = 0; i < Labels.Length; i++)
            {
                if (Labels[i] == otherHand.Labels[i])
                {
                    continue;
                }
                if (otherHand.Labels[i] > Labels[i])
                {
                    return -1;
                }
                return 1;
            }
            return 0;
        }
    }
}
