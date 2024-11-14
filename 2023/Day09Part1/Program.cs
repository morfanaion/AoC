using Day09Part1;

List<Sequence> sequences = File.ReadAllLines("input.txt").Select(l =>  new Sequence(l)).ToList();

long result = 0;
foreach (Sequence sequence in sequences)
{
    sequence.ExtrapolateNextNumber();
    result += sequence.Last();
}
Console.WriteLine(result);