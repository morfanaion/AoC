﻿namespace Day05Part1
{
    internal class Map<TDestination, TSource>
        where TDestination : class
        where TSource : Entity<TSource, TDestination>
    {
        public long DestinationRangeStart { get; set; }
        public long SourceRangeStart { get; set; }
        public long RangeLength { get; set; }

        public virtual bool SetDestinationForSource(TSource source, Func<long, TDestination> generator)
        {
            long destinationId = source.Id - SourceRangeStart + DestinationRangeStart;
            if (destinationId > DestinationRangeStart + RangeLength || destinationId < DestinationRangeStart)
            {
                return false;
            }
            if(_destinations.TryGetValue(destinationId, out TDestination? destination)) 
            {
                source.Destination = destination;
                return true;
            }
            else
            {
                TDestination newDestination = generator(destinationId);
                _destinations.Add(destinationId, newDestination);
                source.Destination = newDestination;
                return true;
            }
        }

        protected Dictionary<long, TDestination> _destinations = new Dictionary<long, TDestination>();
    }
}
