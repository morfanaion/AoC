namespace Day24Puzzle02
{
	internal class AndGate : NonStaticGate
	{
		public override bool Output => Input1.Output && Input2.Output;
	}
}
