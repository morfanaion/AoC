using Day07Part1;

List<Hand> hands = File.ReadAllLines("input.txt").Select(Hand.FromString).OrderBy(hand => hand).ToList();
long result = 0;
for(int i = 0; i < hands.Count; i++)
{
    result += (i + 1) * hands[i].Bid;
}
Console.WriteLine(result);