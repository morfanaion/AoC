namespace Day03Puzzle02
{
    internal static class Extensions
    {
        public static IEnumerable<IEnumerable<T>> CreateChunks<T>(this IEnumerable<T> collection, int chunkSize)
        {
            int i = 0;
            IEnumerable<T> result = collection.Take(chunkSize);
            while (result.Any())
            {
                yield return result;
                i += chunkSize;
                result = collection.Skip(i).Take(chunkSize);
            }
        }
    }
}
