namespace _2025_09_02
{
    internal class RedTile : Tile
    {
        public long ActualX { get; set; }
        public long ActualY { get; set; }
        public int VirtualX { get; set; }
        public int VirtualY { get; set; }

        public override char Color => 'R';
    }
}
