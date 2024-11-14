namespace Day07Part1
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

        private HandTypes? _handType;
        public HandTypes HandType
        {
            get => _handType ??= DetermineHandType();
        }

        private HandTypes DetermineHandType()
        {
            switch (Labels.Distinct().Count())
            {
                case 1:
                    return HandTypes.FiveOfAKind;
                case 2:
                    switch (Labels.Count(label => label == Labels.First()))
                    {
                        case 1:
                        case 4:
                            return HandTypes.FourOfAKind;
                        default:
                            return HandTypes.FullHouse;
                    }
                case 3:
                    switch(Labels.GroupBy<byte, byte>(label => label).Max(group => group.Count()))
                    {
                        case 3:
                            return HandTypes.ThreeOfAKind;
                        default:
                            return HandTypes.TwoPair;
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
                Bid = long.Parse(parts[1])
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
                    return 11;
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
