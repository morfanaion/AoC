namespace Day11Puzzle02
{
    internal class Monkey
    {
        public static List<Monkey> MonkeyList { get; set; } = new List<Monkey>();
        public static int Threshold { get; set; } = 0;

        public int Id { get; set; }
        public Queue<long> ItemList { get; private set; }
        public Operation Operation { get; set; }
        public int OperationParameter { get; set; }
        public int IdForTrue { get; set; }
        public int IdForFalse { get; set; }
        public int TestDivisor { get; set; }
        public long NumInspections { get; set; } = 0;

        public Monkey()
        {
            ItemList = new Queue<long>();
        }

        public void HandleTurn()
        {
            while (ItemList.Any())
            {
                long item = ItemList.Dequeue();
                NumInspections++;
                long parameter = OperationParameter;
                if (parameter == -1)
                {
                    parameter = item;
                }
                switch(Operation)
                {
                    case Operation.Multiply:
                        item *= parameter;
                        break;
                    case Operation.Divide:
                        item /= parameter;
                        break;
                    case Operation.Add:
                        item += parameter;
                        break;
                    case Operation.Subtract:
                        item -= parameter;
                        break;
                }
                if(item >= Threshold)
                {
                    item -= (item / Threshold) * Threshold;
                }
                //item /= 3;
                if(item % TestDivisor == 0)
                {
                    MonkeyList.Single(otherMonkey => otherMonkey.Id == IdForTrue).ItemList.Enqueue(item);
                }
                else
                {
                    MonkeyList.Single(otherMonkey => otherMonkey.Id == IdForFalse).ItemList.Enqueue(item);
                }
            }
        }
    }
}
