namespace Day05Part1
{
    internal class DefaultMap<TDestination, TSource> : Map<TDestination, TSource>
        where TDestination : class
        where TSource: Entity<TSource, TDestination>
    {
        public override bool SetDestinationForSource(TSource source, Func<long, TDestination> generator)
        {
            if (_destinations.TryGetValue(source.Id, out TDestination? destination))
            {
                source.Destination = destination;
                return true;
            }
            else
            {
                TDestination newDestination = generator(source.Id);
                _destinations.Add(source.Id, newDestination);
                source.Destination = newDestination;
                return true;
            }
        }
    }
}
