

namespace _2025_06_02
{
    internal class Problem
    {
        private IEnumerable<string> _input;

        public Problem(IEnumerable<string> input)
        {
            _input = input.Reverse();
        }

        internal long Solve()
        {
            Func<long, long, long> process;
            long currentNumber;
            switch (_input.First().Trim())
            {
                case "+":
                    process = (current, number) => current + number;
                    currentNumber = 0;
                    break;
                case "*":
                    process = (current, number) => current * number;
                    currentNumber = 1;
                    break;
                default:
                    throw new InvalidDataException("Unknown operator");
            }
            int length = _input.First().Length;
            for(int idx = 0; idx < length; idx++)
            {
                long number = long.Parse(string.Join(string.Empty, _input.Skip(1).Select(s => s.Skip(idx).First()).Where(char.IsDigit).Reverse()));
                currentNumber = process(currentNumber, number);
            }
            return currentNumber;
        }
    }
}
