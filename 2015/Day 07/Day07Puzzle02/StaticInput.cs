namespace Day07Puzzle02
{
    internal class StaticInput : IWireInput
    {
        public string Value { get; set; } = string.Empty;
        public ushort GetValue()
        {
            ushort value;
            if (!ushort.TryParse(Value, out value))
            {
                value = Wire.Wires[Value].GetValue();
            }
            return value;
        }
    }
}
