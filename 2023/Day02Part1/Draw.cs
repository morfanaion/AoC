namespace Day02Part1
{
    internal class Draw
    {
        public int Red { get; private set; }
        public int Green { get; private set; }
        public int Blue { get; private set; }

        public static Draw FromString(string text)
        {
            Draw result = new Draw();
            foreach(string str in text.Split(','))
            {
                string[] parts = str.Trim().Split(" ");
                switch(parts[1])
                {
                    case "red":
                        result.Red = int.Parse(parts[0]);
                        break;
                    case "green":
                        result.Green = int.Parse(parts[0]);
                        break;
                    case "blue":
                        result.Blue = int.Parse(parts[0]);
                        break;
                }
            }
            return result;
        }

        internal bool IsDrawValid(int maxRed, int maxGreen, int maxBlue) => maxRed >= Red && maxGreen >= Green && maxBlue >= Blue;
    }
}
