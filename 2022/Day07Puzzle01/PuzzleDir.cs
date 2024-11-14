namespace Day07Puzzle01
{
    internal class PuzzleDir : List<IFileSystemItem>, IFileSystemItem
    {
        public PuzzleDir? AncestorDir { get; set; }
        public string Name { get; set; } = string.Empty;

        public int Size => this.Sum(fsi => fsi.Size);
    }
}
