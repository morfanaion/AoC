namespace _2025_11_01
{
    internal abstract class Device
    {
        public static Dictionary<string, Device> Devices = new();

        public string Name { get; set; } = string.Empty;

        public abstract long NumPathsToOut { get; }
    }
}
