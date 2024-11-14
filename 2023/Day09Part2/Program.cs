using Day09Part2;

List<Sequence> sequences = File.ReadAllLines("input.txt").Select(l => new Sequence(l)).ToList();

long result = 0;
foreach (Sequence sequence in sequences)
{
    sequence.ExtrapolatePreviousNumber();
    result += sequence.First();
}
Console.WriteLine(result);