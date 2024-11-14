namespace Day20Part1
{
    internal class Pulse(string sender, string receipient, bool high)
    {
        public string Sender { get; set; } = sender;
        public string Receipient { get; set; } = receipient;
        public bool High { get; set; } = high;
    }
}
