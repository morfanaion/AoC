namespace Day17Puzzle02
{
    internal class Shape
    {
        public int Id { get; set; }
        private char[][] _shape;
        public int PosX { get; set; }
        public int PosY { get; set; }
        public Shape(char[][] shape)
        {
            _shape = shape;
            _firstIndices = _shape.Select(line => Array.IndexOf(line, '█')).ToArray();
            _lastIndices = _shape.Select(line => Array.LastIndexOf(line, '█')).ToArray();
            _yindeces = new int[_shape[0].Length];
            for (int x = 0; x < _shape[0].Length; x++)
            {
                int y;
                for (y = 0; y < _shape.Length; y++)
                {
                    if (_shape[y][x] != ' ')
                    {
                        break;
                    }

                }
                _yindeces[x] = y;
            }

        }

        private int[] _firstIndices;
        private int[] _lastIndices;
        private int[] _yindeces;


        public bool CanMove(List<List<char>> theRoom, Direction direction)
        {
            switch(direction)
            {
                case Direction.Left:
                    for(int y = 0; y < _firstIndices.Length; y++)
                    {
                        if(theRoom[PosY + y][PosX + _firstIndices[y] - 1] != ' ')
                        { 
                            return false; 
                        }
                    }
                    return true;
                case Direction.Right:
                    for (int y = 0; y < _lastIndices.Length; y++)
                    {
                        if (theRoom[PosY + y][PosX + _lastIndices[y] + 1] != ' ')
                        { 
                            return false; 
                        }
                    }
                    return true;
                case Direction.Down:
                    for(int x = 0; x < _shape[0].Length; x++)
                    {
                        if (theRoom[PosY + _yindeces[x] - 1][PosX + x] != ' ')
                        {
                            return false;
                        }
                    }
                    return true;
            }
            return false;
        }

        internal void Place(List<List<char>> theRoom)
        {
            for (int y = 0; y < _shape.Length; y++)
            {
                for (int x = 0; x < _shape[y].Length; x++)
                {
                    if(_shape[y][x] != ' ')
                    {
                        theRoom[PosY + y][PosX + x] = _shape[y][x];
                    }
                }
            }
        }
    }
}
