
namespace Day10Puzzle02
{
    internal class Element
    {
        public int Id { get; set; }
        public string ElementName { get; set; } = string.Empty;

        private string _representation = string.Empty;
        public string Representation
        {
            get => _representation;
            set
            {
                _representation = value;
                RepresentationLength = value.Length;
            }
        }
        public long RepresentationLength { get; private set; }

        public string[] DecayResult { get; set; } = new string[0];

        internal static Element FromString(string str)
        {
            string[] parts = str.Split('\t');
            return new Element()
            {
                Id = int.Parse(parts[0]),
                ElementName = parts[1],
                Representation = parts[2],
                DecayResult = parts[3].Split('.')
            };
        }
    }
}
