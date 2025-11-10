namespace Day07Puzzle01
{
    internal class LeftShiftGate : IWireInput
    {
        public string LeftWireId { get; set; } = string.Empty;
        public byte ShiftAmount { get; set; }

        public ushort GetValue() => (ushort)(Wire.Wires[LeftWireId].GetValue() << ShiftAmount);
    }
}
