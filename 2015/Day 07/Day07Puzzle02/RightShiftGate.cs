namespace Day07Puzzle02
{
    internal class RightShiftGate : IWireInput
    {
        public string LeftWireId { get; set; } = string.Empty;
        public byte ShiftAmount { get; set; }

        public ushort GetValue() => (ushort)(Wire.Wires[LeftWireId].GetValue() >> ShiftAmount);
    }
}
