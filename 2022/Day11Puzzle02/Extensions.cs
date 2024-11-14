namespace Day11Puzzle02
{
    internal static class Extensions
    {
        public static int Product<T>(this IEnumerable<T> collection, Func<T, int> selector)
        {
            int result = 1;
            foreach(T item in collection)
            {
                result *= selector(item);
            }
            return result;
        }
    }
}
