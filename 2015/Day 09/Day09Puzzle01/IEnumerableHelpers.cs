namespace Day09Puzzle01
{
    internal static class IEnumerableHelpers
    {
        internal static IEnumerable<IEnumerable<T>> Permute<T>(this IEnumerable<T> source)
        {
            if(source == null)
            {
                yield break;
            }

            if(!source.Any())
            {
                yield return Enumerable.Empty<T>();
                yield break;
            }

            var currentItemIndex = 0;
            foreach (T item in source)
            {
                var remainingItems = source.Where((e, i) => i != currentItemIndex);
                foreach(var permutationOfRemainder in remainingItems.Permute())
                {
                    yield return permutationOfRemainder.Prepend(item);
                }
                currentItemIndex++;
            }
        }
    }
}
