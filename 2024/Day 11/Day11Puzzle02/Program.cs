// See https://aka.ms/new-console-template for more information
using Day11Puzzle02;

const int NumBlinks = 75;
Dictionary<(long, int), long> filesAfterIterations = new Dictionary<(long, int), long>();
long[] numbers = File.ReadAllText("input.txt").Split(' ').Select(long.Parse).ToArray();
PriorityQueue<QueueItem, int> queue = new PriorityQueue<QueueItem, int>();
foreach(long number in numbers)
{
    queue.Enqueue(new QueueItem() { Number = number, NumBlinksRemaining = NumBlinks }, NumBlinks);
}
while(queue.TryDequeue(out QueueItem? queueItem, out int priority))
{
    if (filesAfterIterations.ContainsKey((queueItem.Number, queueItem.NumBlinksRemaining)))
    {
        continue;
    }
    if (priority == 0)
    {
        filesAfterIterations.Add((queueItem.Number, queueItem.NumBlinksRemaining), 1);
        continue;
    }
    if(queueItem.WaitingFor == null)
    {
        int numDigits = queueItem.Number.ToString().Length;
        int halfDigits = numDigits / 2;

        if (queueItem.Number == 0)
        {
            queueItem.WaitingFor = [1];
        }
        else if (halfDigits * 2 == numDigits)
        {
            long divider = (long)Math.Pow(10, halfDigits);
            long number1 = queueItem.Number / divider;
            long number2 = queueItem.Number - (divider * number1);
            queueItem.WaitingFor = [number1, number2];
        }
        else
        {
            queueItem.WaitingFor = [queueItem.Number * 2024];
        }
    }
    if (queueItem.WaitingFor.All(i => filesAfterIterations.ContainsKey((i, queueItem.NumBlinksRemaining - 1))))
    {
        filesAfterIterations.Add((queueItem.Number, queueItem.NumBlinksRemaining), queueItem.WaitingFor.Sum(i => filesAfterIterations[(i, queueItem.NumBlinksRemaining - 1)]));
    }
    else
    {
        foreach (var waitNumber in queueItem.WaitingFor)
        {
            queue.Enqueue(new QueueItem() { Number = waitNumber, NumBlinksRemaining = queueItem.NumBlinksRemaining - 1 }, queueItem.NumBlinksRemaining - 1);
        }
        queue.Enqueue(queueItem, queueItem.NumBlinksRemaining);
    }
}
Console.WriteLine(numbers.Sum(i => filesAfterIterations[(i, NumBlinks)]));