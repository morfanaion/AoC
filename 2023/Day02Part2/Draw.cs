namespace Day02Part2
{
    internal class Draw
    {
        public int Red { get; set; }
        public int Green { get; set; }
        public int Blue { get; set; }

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

        public int Power => Red * Green * Blue;
    }
}
