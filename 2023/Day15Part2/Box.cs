namespace Day15Part2
{
    internal class Box : List<Lens>
    {
        public byte Id { get; set; }

        public long FocusingPower
        {
            get
            {
                long result = 0;
                int idx = 1;
                foreach(Lens l in this)
                {
                    result += idx++ * l.FocalStrength;
                }
                return result * (((long)Id) + 1);
            }
        }
    }
}
