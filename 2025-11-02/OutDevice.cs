namespace _2025_11_02
{
    internal class OutDevice : Device
    {
        public override (long numRoutesThroughBoth, long numRoutesThroughDac, long numRoutesThroughFft, long totalNumRoutes) NumPathsToOut => (0,0,0,1);
    }
}
