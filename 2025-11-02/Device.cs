namespace _2025_11_02
{
    internal abstract class Device
    {
        public static Dictionary<string, Device> Devices = new();

        public string Name { get; set; } = string.Empty;

        public abstract (long numRoutesThroughBoth, long numRoutesThroughDac, long numRoutesThroughFft, long totalNumRoutes) NumPathsToOut { get; }
    }
}
