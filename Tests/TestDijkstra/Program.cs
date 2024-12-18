using AOC.Maths;

UInt128 num1 = UInt128.Parse("164984951315349843216488648641231864654");
UInt128 num2 = UInt128.Parse("134984951315349843216488648641231864654");
UInt128 num3 = UInt128.Parse("134951315349843216488648641231864654");
UInt128 num4 = UInt128.Parse("131315349843216488648641231864654");
UInt128 num5 = UInt128.Parse("1315349843216488648641231864654");
UInt128 num6 = UInt128.Parse("13951315349843216488648641231864654");
Console.WriteLine($"Min: {AoCMath.Min(num1, num2, num3, num4, num5, num6)}");
Console.WriteLine($"Max: {AoCMath.Max(num1, num2, num3, num4, num5, num6)}");
Console.WriteLine($"Median: {AoCMath.Median(num1, num2, num3, num4, num5, num6)}");
Console.WriteLine($"Average: {AoCMath.Average(num1, num2, num3, num4, num5, num6)}");
Console.WriteLine($"Sum: {AoCMath.Sum(num1, num2, num3, num4, num5, num6)}");
Console.WriteLine(Int128.MaxValue);
long test = long.MaxValue;
long test2 = 0;
test2 = ~test2;

//Console.WriteLine(IntegerMath<int>.PrimeNumbers.Count());

//Console.WriteLine(AoCMath.Average(new uint[] {253, 250, 128, 251, 255, 169, 94, 255, 249, 3}));