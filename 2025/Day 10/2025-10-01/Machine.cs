

namespace _2025_10_01
{
    internal class Machine
    {
        public static Machine FromString(string input)
        {
            Machine machine = new Machine();
            while (input.Length > 0)
            {
                switch (input[0])
                {
                    case '[':
                        (machine.IndicatorLightTarget, input) = ProcessIndicatorLights(input);
                        break;
                    case '(':
                        (ushort button, input) = ProcessButton(input);
                        machine.Buttons.Add(button);
                        break;
                    case '{':
                        input = input.Substring(input.IndexOf('}') + 1);
                        break;
                    default:
                        input = input.Substring(1);
                        break;
                }

            }
            return machine;
        }

        private static (ushort button, string input) ProcessButton(string input)
        {
            int indexOfClosingParantheses = input.IndexOf(')');
            ushort button = 0;
            foreach (ushort indicatorIdx in input.Substring(1, indexOfClosingParantheses - 1).Split(',').Select(ushort.Parse))
            {
                button |= (ushort)(1 << indicatorIdx);
            }

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

        public List<ushort> Buttons { get; } = new List<ushort>();

        public int FindMinNumPressesToGetTargetIndicators()
        {
            PriorityQueue<ushort, int> priorityQueue = new PriorityQueue<ushort, int>();
            foreach (ushort button in Buttons)
            {
                priorityQueue.Enqueue(button, 1);
            }
            while (priorityQueue.TryDequeue(out ushort currentIndicator, out int currentPresses))
            {
                if (currentIndicator == IndicatorLightTarget)
                {
                    return currentPresses;
                }
                foreach (ushort button in Buttons)
                {
                    priorityQueue.Enqueue((ushort)(currentIndicator ^ button), currentPresses + 1);
                }
            }
            return 0;
        }
    }
}
