namespace Day22Part2
{
    internal class Brick
    {
        public static List<Brick> AllBricks { get; } = new List<Brick>();
        public char Id { get; set; }

        public Brick(string definition)
        {
            string[] parts = definition.Split('~');
            First = new Position(parts[0]);
            Last = new Position(parts[1]);
        }

        public int MaxZ => Math.Max(First.Z, Last.Z);
        public int MinZ => Math.Min(First.Z, Last.Z);
        public int MaxX => Math.Max(First.X, Last.X);
        public int MinX => Math.Min(First.X, Last.X);
        public int MaxY => Math.Max(First.Y, Last.Y);
        public int MinY => Math.Min(First.Y, Last.Y);

        public bool Disintegrated { get; set; }

        public bool Settled = false;
        public Position First { get; set; }
        public Position Last { get; set; }

        internal void SettleAtZ(int z)
        {
            int movedown = MinZ - z;
            First.Z -= movedown;
            Last.Z -= movedown;
            Settled = true;
        }

        internal bool WillSupport(Brick currentBrick)
        {
            if(IsAboveOrBelow(currentBrick))
            {
                return true;
            }
            return false;
        }

        public void DetermineSupports()
        {
            foreach(Brick otherBrick in AllBricks.Where(b => b.MaxZ + 1 == MinZ))
            {
                if(IsAboveOrBelow(otherBrick))
                {
                    otherBrick.Supports.Add(this);
                    SupportedBy.Add(otherBrick);
                }
            }
        }

        public bool IsAboveOrBelow(Brick otherBrick)
        {
            // Check if any part of the current brick is directly above or below the other brick
            bool isAbove =
                (MaxX >= otherBrick.MinX && MinX <= otherBrick.MaxX) &&
                (MaxY >= otherBrick.MinY && MinY <= otherBrick.MaxY);

            return isAbove;
        }

        public List<Brick> Supports { get; } = new List<Brick>();
        public List<Brick> SupportedBy { get; } = new List<Brick>();

        public void CheckDisintegration()
        {
            if(SupportedBy.Any() && SupportedBy.All(b => b.Disintegrated))
            {
                Disintegrated = true;
            }
        }
    }
}
