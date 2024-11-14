namespace Day13Puzzle02
{
    internal class Item : List<object>
    {
        public string OriginalString { get; set; } = string.Empty;

        public Item(List<object> originalList)
        {
            AddRange(originalList);
        }
    }
}
