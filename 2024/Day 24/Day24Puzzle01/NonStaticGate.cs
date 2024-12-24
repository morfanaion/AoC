namespace Day24Puzzle01
{
	internal abstract class NonStaticGate : Gate
	{
		public Gate Input1 { get; set; } = new StaticGate();
		public Gate Input2 { get; set; } = new StaticGate();

	}
}
