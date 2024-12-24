namespace Day24Puzzle02
{
    internal class NonStaticGate : Gate
    {
        public Gate Input1 { get; set; } = new StaticGate();
        public Gate Input2 { get; set; } = new StaticGate();

        public Func<Gate, Gate, bool> CompareFunction { get; set; } = (g1, g2) => g1 == g2;
        private bool _inGettingOutput = false;

        public override bool Output
        {
            get
            {
                if (_inGettingOutput)
                {
                    ValidResult = false;
                    return false;
                }
                _inGettingOutput = true;
                bool result = CompareFunction(Input1, Input2);
                _inGettingOutput = false;
                return result;
            }
        }

        public string Operator { get; set; }

        public IEnumerable<Gate> Nodes
        {
            get
            {
                if(Input1 is NonStaticGate nonStatic1)
                {
                    foreach(Gate gate in nonStatic1.Nodes)
                    {
                        yield return gate;
                    }
                }
                yield return Input1;
                if (Input2 is NonStaticGate nonStatic2)
                {
                    foreach (Gate gate in nonStatic2.Nodes)
                    {
                        yield return gate;
                    }
                }
                yield return Input2;
            }
        }

        public override string GetDefinitionString() => $"{Input1.Id} {Operator} {Input2.Id} -> {Id}";
    }
}
