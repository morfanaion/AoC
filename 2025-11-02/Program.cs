using _2025_11_02;
foreach(string str in File.ReadAllLines("input.txt"))
{
    _ = StandardDevice.FromString(str);
}
Device.Devices.Add("out", new OutDevice());


Console.WriteLine(Device.Devices["svr"].NumPathsToOut.numRoutesThroughBoth);