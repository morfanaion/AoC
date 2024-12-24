namespace Day24Puzzle01
{
	internal class OrGate : NonStaticGate
	{
		public override bool Output => Input1.Output || Input2.Output;
	}
}
