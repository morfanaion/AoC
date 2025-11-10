namespace Day07Puzzle02
{
    internal class WireInput : IWireInput
    {
        public string WireId { get; set; } = string.Empty;

        public ushort GetValue() => Wire.Wires[WireId].GetValue();
    }
}
