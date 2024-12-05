namespace Day05Puzzle01
{
    internal class OrderRule
    {
        public static OrderRule FromString(string str)
        {
            string[] parts = str.Split('|', StringSplitOptions.RemoveEmptyEntries);
            return new OrderRule()
            {
                FirstPageNumber = int.Parse(parts[0]),
                LastPageNumber = int.Parse(parts[1])
            };
        }

        public int FirstPageNumber { get; set; }
        public int LastPageNumber { get; set; }

        public bool RuleAppliesCorrectly(IEnumerable<int> update)
        {
            bool firstPageFound = false;
            bool lastPageFound = false;
            foreach (int item in update)
            {
                if (item == FirstPageNumber)
                {
                    firstPageFound = true;
                    if (lastPageFound)
                    {
                        return false;
                    }
                }
                if (item == LastPageNumber)
                {
                    lastPageFound = true;
                    if (firstPageFound)
                    {
                        return true;
                    }
                }
            }
            return true;
        }

        public void ApplyRule(ref int[] update)
        {
            if (!RuleAppliesCorrectly(update))
            {
                int lastPageIndex = Array.IndexOf(update, LastPageNumber);
                int firstPageIndex = Array.IndexOf(update, FirstPageNumber);
                update = update.Take(lastPageIndex)
                    .Concat(update.Skip(firstPageIndex).Take(1))
                    .Concat(update.Skip(lastPageIndex).Take(firstPageIndex - lastPageIndex))
                    .Concat(update.Skip(firstPageIndex + 1))
                    .ToArray();
            }
        }
    }
}