namespace Day24Puzzle02
{
	internal abstract class Gate
	{
		public static List<Gate> Gates = new List<Gate>();
		public static bool ValidResult = true;
		private string _id = string.Empty;
		public string Id
		{
			get => _id;
			set
			{
				_id = value;
				switch(_id[0])
				{
					case 'x':
                    case 'y':
                    case 'z':
						ValueIfSet = ((long)0x1) << int.Parse(_id.Substring(1));
						break;
                }
			}
		}
		private long ValueIfSet { get; set; }
		public long Value => Output ? ValueIfSet : 0;
		public void SetExpectedValue(long expectedResult)
		{
			ExpectedOutput = (expectedResult & ValueIfSet) == ValueIfSet;
		}
		private bool ExpectedOutput { get; set; }
		public bool ValueAsExpected => ExpectedOutput == Output;
		protected virtual void IdSet() { }
		public abstract bool Output { get; }
		public abstract string GetDefinitionString();
	}
}
