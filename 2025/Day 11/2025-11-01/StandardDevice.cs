namespace _2025_11_01
{
    internal class StandardDevice : Device
    {
        public static Device FromString(string str)
        {
            string[] parts = str.Split(":");
            StandardDevice newDevice = new StandardDevice()
            {
                Name = parts[0]
            };
            newDevice.OutputDeviceNames.AddRange(parts[1].Trim().Split(' '));
            Devices.Add(newDevice.Name, newDevice);
            return newDevice;
        }

        public List<string> OutputDeviceNames { get; } = new List<string>();

        private long? _numPathsToOut;
        public override long NumPathsToOut
        {
            get
            {
                if (_numPathsToOut.HasValue)
                {
                    return _numPathsToOut.Value;
                }
                _numPathsToOut = OutputDeviceNames.Sum(deviceName => Devices[deviceName].NumPathsToOut);
                return _numPathsToOut.Value;
            }
        }
    }
}
