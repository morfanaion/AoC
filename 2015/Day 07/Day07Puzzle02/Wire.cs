namespace Day07Puzzle02
{
    internal class Wire
    {
        public Wire(string id, IWireInput input)
        {
            Id = id;
            Input = input;
            Wires.Add(id, this);
        }
        public static Dictionary<string, Wire> Wires = new Dictionary<string, Wire>();
        public string Id { get; }
        public IWireInput Input { get; }
        private ushort? value;
        public ushort GetValue() => value ??= Input.GetValue();
    }
}
