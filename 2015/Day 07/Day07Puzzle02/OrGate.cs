namespace Day07Puzzle02
{
    internal class OrGate : IWireInput
    {
        public string LeftWireId { get; set; } = string.Empty;
        public string RightWireId { get; set; } = string.Empty;

        public ushort GetValue()
        {
            ushort leftValue;
            if (!ushort.TryParse(LeftWireId, out leftValue))
            {
                leftValue = Wire.Wires[LeftWireId].GetValue();
            }
            ushort rightValue;
            if (!ushort.TryParse(RightWireId, out rightValue))
            {
                rightValue = Wire.Wires[RightWireId].GetValue();
            }
            return (ushort)(leftValue | rightValue);
        }
    }
}
