using static System.Runtime.InteropServices.JavaScript.JSType;

long sum = 0;
Dictionary<(long, long, long, long), long> bananasDictionary = new Dictionary<(long, long, long, long), long>();
foreach(long number in File.ReadAllLines("input.txt").Select(long.Parse))
{
	Dictionary<(long, long, long, long), long> buyerDictionary = GetBuyerSequencePrices(number);
	foreach(KeyValuePair<(long, long, long, long), long> kvp in buyerDictionary)
	{
		if(bananasDictionary.ContainsKey(kvp.Key))
		{
			bananasDictionary[kvp.Key] += kvp.Value;
		}
		else
		{
			bananasDictionary[kvp.Key] = kvp.Value;
		}
	}
}

Console.Write(bananasDictionary.Max(kvp => kvp.Value));

Dictionary<(long, long, long, long), long> GetBuyerSequencePrices(long number)
{
	Dictionary<(long, long, long, long), long> buyerDictionary = new Dictionary<(long, long, long, long), long>();
	long previous1 = -1;
	long previous2 = -1;
	long previous3 = -1;
	long previous4 = -1;
	long current = number % 10;
	for (int i = 0; i < 2000; i++)
	{
		previous1 = previous2;
		previous2 = previous3;
		previous3 = previous4;
		previous4 = current;
		number = ProcessNumber(number);
		current = number % 10;
		long diff1 = previous2 - previous1;
		long diff2 = previous3 - previous2;
		long diff3 = previous4 - previous3;
		long diff4 = current - previous4;
		if (previous1 != -1 && !buyerDictionary.ContainsKey((diff1, diff2, diff3, diff4)))
		{
			buyerDictionary[(diff1, diff2, diff3, diff4)] = current;
		}
	}
	return buyerDictionary;
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