namespace Day12Puzzle02
{
    internal class Node
    {
        public int Elevation { get; private set; }
        public bool IsStartPoint { get; private set; }
        public bool IsEndPoint { get; private set; }
        public List<Node> ConnectedNodes { get; private set; }
        public int NumStepsRequired;

        public Node(char c)
        {
            if(c == 'S')
            {
                IsStartPoint = true;
                c = 'a';
            }
            else if(c == 'E')
            {
                IsEndPoint = true;
                c = 'z';
            }
            Elevation = c - 'a';
            ConnectedNodes = new List<Node>();
            NumStepsRequired = -1;
        }
    }
}
