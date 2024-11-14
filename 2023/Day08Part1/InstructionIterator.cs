namespace Day08Part1
{
    internal class InstructionIterator
    {
        private string _instructionString;
        private int _currentIndex = 0;
        public InstructionIterator(string instructionString)
        {
            _instructionString = instructionString;
        }

        public char Next()
        {
            if(_currentIndex == _instructionString.Length)
            {
                _currentIndex = 0;
            }
            return _instructionString[_currentIndex++];
        }
    }
}
