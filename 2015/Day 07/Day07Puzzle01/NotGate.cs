namespace Day07Puzzle01
{
    internal class NotGate : IWireInput
    {
        public string InputWireId { get; set; } = string.Empty;
        public ushort GetValue() => (ushort)~Wire.Wires[InputWireId].GetValue();
    }
}
