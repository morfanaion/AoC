namespace _2025_12_01
{
    internal class Package
    {
        public int Id { get; set; }
        public char[][] Grid { get; set; } = new char[3][];

        private int? _totalSize;
        public int TotalSize
        {
            get
            {
                if (_totalSize == null)
                {
                    _totalSize = Grid.SelectMany(line => line).Count(c => c == '#');
                }
                return _totalSize.Value;
            }
        }
    }
}
