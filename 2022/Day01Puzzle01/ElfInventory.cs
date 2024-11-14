namespace Day01Puzzle01
{
    internal class ElfInventory
    {
        public static int ElfCounter = 0;

        public int ElfIdentifier { get; private set; }
        public int NumCalories { get; private set; }
        public ElfInventory(string inventory)
        {
            ElfIdentifier = ++ElfCounter;
            NumCalories = inventory.Split("\r\n").Sum(s => int.Parse(s));
        }
    }
}
