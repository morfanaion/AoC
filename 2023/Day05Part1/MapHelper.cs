namespace Day05Part1
{
    internal class MapHelper
    {
        private static Dictionary<Type, object> _mapHelpers = new Dictionary<Type, object>();
        public static void RegisterMapHelper<TKeySource, TKeyDestination>(MapHelper<TKeyDestination, TKeySource> mapHelper)
            where TKeyDestination : class
            where TKeySource : Entity<TKeySource, TKeyDestination>
        {
            _mapHelpers.Add(typeof(TKeySource), mapHelper);
        }

        public static MapHelper<TKeyDestination, TKeySource> GetMapHelper<TKeySource, TKeyDestination>()
            where TKeyDestination : class
            where TKeySource : Entity<TKeySource, TKeyDestination>
        {
            return (MapHelper<TKeyDestination, TKeySource>)_mapHelpers[typeof(TKeySource)];
        }
    }

    internal class MapHelper<TDestination, TSource>
        where TDestination : class
        where TSource : Entity<TSource, TDestination>
    {
        

        private Func<long, TDestination> _destinationGenerator;
        private List<Map<TDestination, TSource>> _maps;

        public MapHelper(Func<long, TDestination> destinationGenerator, IEnumerable<string> mapEntries)
        {
            _destinationGenerator = destinationGenerator;
            _maps = mapEntries.Select(MapFromString).ToList(); 
            _maps.Add(new DefaultMap<TDestination, TSource>());
        }

        private Map<TDestination, TSource> MapFromString(string str)
        {
            string[] parts = str.Trim().Split(" ");
            return new Map<TDestination, TSource>()
            {
                DestinationRangeStart = long.Parse(parts[0]),
                SourceRangeStart = long.Parse(parts[1]),
                RangeLength = long.Parse(parts[2])
            };
        }

        public void SetDestinationForSource(TSource source)
        {
            foreach(Map<TDestination, TSource> mapEntry in _maps) 
            {
                if(mapEntry.SetDestinationForSource(source, _destinationGenerator))
                {
                    return;
                }
            }

        }
    }
}
