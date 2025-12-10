
using Google.OrTools.LinearSolver;

namespace _2025_10_02
{
    internal class Machine
    {
        private static int counter = 0;
        public static Machine FromString(string input)
        {
            Machine machine = new Machine();
            while(input.Length > 0)
            {
                switch (input[0])
                {
                    case '[':
                        (machine.IndicatorLightTarget, input) = ProcessIndicatorLights(input);
                        break;
                    case '(':
                        (int[] button, input) = ProcessButton(input);
                        machine.Buttons.Add(button);
                        break;
                    case '{':
                        (machine.TargetJoltages, input) = ProcessJoltageTargets(input);
                        input = input.Substring(input.IndexOf('}') + 1);
                        break;
                    default:
                        input = input.Substring(1);
                        break;
                }
                 
            }
            return machine;
        }

        public int Id { get; } = counter++;

        private static (int[] joltageTargets, string input) ProcessJoltageTargets(string input)
        {
            int indexOfClosingAccolade = input.IndexOf('}');
            int[] joltageTargets = input.Substring(1, indexOfClosingAccolade - 1).Split(',').Select(int.Parse).ToArray();
            return (joltageTargets, input.Substring(indexOfClosingAccolade + 1));
        }

        private static (int[] button, string input) ProcessButton(string input)
        {
            int indexOfClosingParantheses = input.IndexOf(')');
            int[] button = input.Substring(1, indexOfClosingParantheses - 1).Split(',').Select(int.Parse).ToArray();
            return (button, input.Substring(indexOfClosingParantheses + 1));
        }

        private static (ushort indicatorLightTarget, string input) ProcessIndicatorLights(string input)
        {
            int indexOfClosingBracket = input.IndexOf(']');
            ushort indicatorTarget = 0;
            for (int i = 0; i < indexOfClosingBracket - 1; i++)
            {
                if (input[i + 1] == '#')
                {
                    indicatorTarget |= (ushort)(1 << i);
                }
            }
            return (indicatorTarget, input.Substring(indexOfClosingBracket + 1));
        }

        public ushort IndicatorLightTarget { get; set; } = 0;
        
        public List<int[]> Buttons { get; } = new List<int[]>();

        public int[] TargetJoltages { get; set; } = [];

        public int FindMinNumPressesToGetTargeJoltages()
        {
            int[][] buttonTargetMatrix = CreateButtonTargetMatrix();

            int numberOfTargetValues = buttonTargetMatrix.Length; ;
            int numberOfButtons = buttonTargetMatrix[0].Length;

            Solver solver = Solver.CreateSolver("CBC_MIXED_INTEGER_PROGRAMMING");

            Variable[] allXs = new Variable[numberOfButtons];
            for (int i = 0; i < numberOfButtons; i++)
            {
                allXs[i] = solver.MakeIntVar(0, double.PositiveInfinity, $"x_{i}");
            }

            for(int i = 0; i < numberOfTargetValues; i++)
            {
                LinearExpr totalIncreaseForPositionIByAllButtonPressesExpression = new LinearExpr();
                for(int j = 0; j < numberOfButtons; j++)
                {
                    totalIncreaseForPositionIByAllButtonPressesExpression += buttonTargetMatrix[i][j] * allXs[j];
                }
                solver.Add(totalIncreaseForPositionIByAllButtonPressesExpression == TargetJoltages[i]);
            }

            LinearExpr sumToMinimizeExpression = new LinearExpr();
            for(int i = 0; i < numberOfButtons; i++)
            {
                sumToMinimizeExpression += allXs[i];
            }

            solver.Minimize(sumToMinimizeExpression);

            Solver.ResultStatus resultStatus = solver.Solve();

            if(resultStatus == Solver.ResultStatus.OPTIMAL)
            {
                return allXs.Select(button => (int)button.SolutionValue()).Sum();
            }

            throw new InvalidOperationException("No solution found");
        }

        private int[][] CreateButtonTargetMatrix()
        {
            List<int[]> matrix = new List<int[]>();
            for(int i = 0; i < TargetJoltages.Length; i++)
            {
                matrix.Add(Buttons.Select(button => button.Contains(i) ? 1 : 0).ToArray());
            }
            return matrix.ToArray();
        }
    }
}
