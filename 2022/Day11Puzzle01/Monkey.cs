namespace Day11Puzzle01
{
    internal class Monkey
    {
        public static List<Monkey> MonkeyList { get; set; } = new List<Monkey>();

        public int Id { get; set; }
        public Queue<int> ItemList { get; private set; }
        public Operation Operation { get; set; }
        public int OperationParameter { get; set; }
        public int IdForTrue { get; set; }
        public int IdForFalse { get; set; }
        public int TestDivisor { get; set; }
        public int NumInspections { get; set; } = 0;

        public Monkey()
        {
            ItemList = new Queue<int>();
        }

        public void HandleTurn()
        {
            while (ItemList.Any())
            {
                int item = ItemList.Dequeue();
                NumInspections++;
                int parameter = OperationParameter;
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
                item /= 3;
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
