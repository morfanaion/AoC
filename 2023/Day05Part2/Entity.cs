namespace Day05Part2
{
    internal class Entity<TSource, TDestination>
        where TDestination : class
        where TSource : Entity<TSource, TDestination>
    {
        public long Id { get; set; }
        private TDestination? _destination { get; set; }
        public TDestination? Destination
        {
            get
            {
                if (_destination == null)
                {
                    MapHelper.GetMapHelper<TSource, TDestination>().SetDestinationForSource((TSource)this);
                }
                return _destination;
            }
            set
            {
                _destination = value;
            }
        }
    }
}
