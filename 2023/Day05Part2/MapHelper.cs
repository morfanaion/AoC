namespace Day05Part2
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

        private static MapHelper<Soil, Seed>? _seedToSoilHelper;
        public static MapHelper<Soil, Seed> SeedToSoilHelper => _seedToSoilHelper ??= GetMapHelper<Seed, Soil>();

        private static MapHelper<Fertilizer, Soil>? _soilToFertilizerHelper;
        public static MapHelper<Fertilizer, Soil> SoilToFertilizerHelper => _soilToFertilizerHelper ??= GetMapHelper<Soil, Fertilizer>();

        private static MapHelper<Water, Fertilizer>? _fertilizerToWaterHelper;
        public static MapHelper<Water, Fertilizer> FertilizerToWaterHelper => _fertilizerToWaterHelper ??= GetMapHelper<Fertilizer, Water>();

        private static MapHelper<Light, Water>? _waterToLightHelper;
        public static MapHelper<Light, Water> WaterToLightHelper => _waterToLightHelper ??= GetMapHelper<Water, Light>();

        private static MapHelper<Temperature, Light>? _lightToTemperatureHelper;
        public static MapHelper<Temperature, Light> LightToTemperatureHelper => _lightToTemperatureHelper ??= GetMapHelper<Light, Temperature>();

        private static MapHelper<Humidity, Temperature>? _temperatureToHumidityHelper;
        public static MapHelper<Humidity, Temperature> TemperatureToHumidityHelper => _temperatureToHumidityHelper ??= GetMapHelper<Temperature, Humidity>();

        private static MapHelper<Location, Humidity>? _humidityToLocationHelper;
        public static MapHelper<Location, Humidity> HumidityToLocationHelper => _humidityToLocationHelper ??= GetMapHelper<Humidity, Location>();

        public static long GetLocationForSeedId(long seedId)
        {
            long soilId = SeedToSoilHelper.GetDestinationIdForSourceId(seedId);
            long fertilizerId = SoilToFertilizerHelper.GetDestinationIdForSourceId(soilId);
            long waterId = FertilizerToWaterHelper.GetDestinationIdForSourceId(fertilizerId);
            long lightId = WaterToLightHelper.GetDestinationIdForSourceId(waterId);
            long temperatureId = LightToTemperatureHelper.GetDestinationIdForSourceId(lightId);
            long humidityId = TemperatureToHumidityHelper.GetDestinationIdForSourceId(temperatureId);
            long locationId = HumidityToLocationHelper.GetDestinationIdForSourceId(humidityId);
            return locationId;
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

        public long GetDestinationIdForSourceId(long sourceId)
        {
            foreach (Map<TDestination, TSource> mapEntry in _maps)
            {
                long? destinationId = mapEntry.GetDestinationId(sourceId);
                if (destinationId.HasValue)
                {
                    return destinationId.Value;
                }
            }
            return long.MaxValue;
        }
    }
}
