long sum = 0;
foreach(long number in File.ReadAllLines("input.txt").Select(long.Parse))
{
	long newNumber = GetNumberAfterNumIterations(number, 2000);
	Console.WriteLine($"{number}: {newNumber}");
	sum += newNumber;
}

Console.Write(sum);

long GetNumberAfterNumIterations(long number, int numIterations)
{
	List<long> result = [number];
	for (int i = 0; i < numIterations; i++)
	{
		number = ProcessNumber(number);
	}
	return number;
}

long ProcessNumber(long number)
{
	number = MixAndPrune(number, number * 64);
	number = MixAndPrune(number, number / 32);
	return MixAndPrune(number, number * 2048);
}

long MixAndPrune(long number, long result)
{
	return (number ^ result) % 16777216;
}