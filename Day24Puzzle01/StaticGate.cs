namespace Day24Puzzle01
{
	internal class StaticGate : Gate
	{
		public bool Input { get; set; }
		public override bool Output => Input;
	}
}
