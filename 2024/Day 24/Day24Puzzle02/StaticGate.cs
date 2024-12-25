namespace Day24Puzzle02
{
	internal class StaticGate : Gate
	{
		public bool Input { get; set; }
		public override bool Output => Input;

        public override string GetDefinitionString() => $"{Id}: {(Input ? 1 : 0)}";

		public void SetInput(long input)
		{
			Input = (input & ValueIfSet) == ValueIfSet;
		}
    }
}
