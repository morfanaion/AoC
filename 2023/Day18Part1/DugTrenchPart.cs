namespace Day18Part1
{
    internal class DugTrenchPart
    {
        public DugTrenchPart? Next { get; set; }
        public DugTrenchPart? Previous { get; set; }
        public TrenchPartType Type { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public byte Red { get; set; }
        public byte Green { get; set; }
        public byte Blue { get; set; }
        public string ColorCode
        {
            get => $"{Red.ToString("X2")}{Green.ToString("X2")}{Blue.ToString("X2")}";
            set
            {
                Red = Convert.ToByte(value.Substring(0, 2), 16);
                Green = Convert.ToByte(value.Substring(0, 2), 16);
                Blue = Convert.ToByte(value.Substring(0, 2), 16);
            }
        }

        public void SetNext(DugTrenchPart next)
        {
            Next = next;
            next.Previous = this;
            if (next.Type != Type)
            {
                if (Previous != null)
                {
                    switch (Type)
                    {
                        case TrenchPartType.Horizontal:
                            if (Previous.X < X)
                            {
                                // to my left
                                Type = Next.Y > Y ? TrenchPartType.LeftDown : TrenchPartType.LeftUp;
                            }
                            else
                            {
                                Type = Next.Y > Y ? TrenchPartType.RightDown : TrenchPartType.RightUp;
                                // to my right
                            }
                            break;
                        case TrenchPartType.Vertical:
                            if (Previous.Y < Y)
                            {
                                // up
                                Type = Next.X > X ? TrenchPartType.RightUp : TrenchPartType.LeftUp;
                            }
                            else
                            {
                                //down
                                Type = Next.X > X ? TrenchPartType.RightDown : TrenchPartType.LeftDown;
                            }

                            break;
                    }
                }
            }
        }
    }
}
