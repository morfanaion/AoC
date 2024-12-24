namespace Day24Puzzle01
{
	internal abstract class Gate
	{
		public string Id { get; set; } = string.Empty;
		public abstract bool Output { get; }
	}
}
