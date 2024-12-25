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
						IsX = true; break;
                    case 'y':
						IsY = true; break;
                    case 'z':
						IsZ = true; break;
                }
				if(IsX || IsY || IsZ)
				{
					Index = int.Parse(_id.Substring(1));
					ValueIfSet = ((long)0x1) << Index;
                }
			}
		}
		protected long ValueIfSet { get; set; }
		public long Value => Output ? ValueIfSet : 0;
		public void SetExpectedValue(long expectedResult)
		{
			ExpectedOutput = (expectedResult & ValueIfSet) == ValueIfSet;
		}
		public bool ExpectedOutput { get; set; }
		public bool ValueAsExpected => ExpectedOutput == Output;
		protected virtual void IdSet() { }
		public abstract bool Output { get; }
		public abstract string GetDefinitionString();
		public bool IsX { get; private set; }
        public bool IsY { get; private set; }
        public bool IsZ { get; private set; }
		public int Index { get; private set; }
    }
}
