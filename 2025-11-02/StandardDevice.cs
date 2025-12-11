namespace _2025_11_02
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

        private (long numRoutesThroughBoth, long numRoutesThroughDac, long numRoutesThroughFft, long totalNumRoutes)? _numPathsToOut;
        public override (long numRoutesThroughBoth, long numRoutesThroughDac, long numRoutesThroughFft, long totalNumRoutes) NumPathsToOut
        {
            get
            {
                if (_numPathsToOut.HasValue)
                {
                    return _numPathsToOut.Value;
                }
                long numRoutesThroughBoth = 0;
                long numRoutesThroughDac = 0;
                long numRoutesThroughFft = 0;
                long totalNumRoutes = 0;
                foreach (Device device in OutputDeviceNames.Select(deviceName => Devices[deviceName]))
                {
                    var numPathsForDevice = device.NumPathsToOut;
                    numRoutesThroughBoth += numPathsForDevice.numRoutesThroughBoth;
                    numRoutesThroughDac += numPathsForDevice.numRoutesThroughDac;
                    numRoutesThroughFft += numPathsForDevice.numRoutesThroughFft;
                    totalNumRoutes += numPathsForDevice.totalNumRoutes;
                }
                switch(Name)
                {
                    case "dac":
                        numRoutesThroughDac = totalNumRoutes;
                        numRoutesThroughBoth = numRoutesThroughFft;
                        break;
                    case "fft":
                        numRoutesThroughFft = totalNumRoutes;
                        numRoutesThroughBoth = numRoutesThroughDac;
                        break;
                }

                _numPathsToOut = (numRoutesThroughBoth, numRoutesThroughDac, numRoutesThroughFft, totalNumRoutes);
                return _numPathsToOut.Value;
            }
        }
    }
}
