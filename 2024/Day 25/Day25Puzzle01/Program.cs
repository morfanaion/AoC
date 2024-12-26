using Day25Puzzle01;

string[] input = File.ReadAllLines("input.txt");
for(int i = 0; i < input.Length; i += 8)
{
	if(input[i].StartsWith('#'))
	{
		Lock.CreateLock([input[i], input[i + 1], input[i + 2], input[i + 3], input[i + 4], input[i + 5], input[i + 6]]);
	}
	else
	{
		Key.CreateKey([input[i], input[i + 1], input[i + 2], input[i + 3], input[i + 4], input[i + 5], input[i + 6]]);
	}
}

int result = 0;
foreach(Lock @lock in Lock.Locks)
{
	foreach(Key key in Key.Keys)
	{
		if(@lock.PinSizes.Zip(key.PinSizes).All(pin => pin.First + pin.Second <= 5))
		{
			result++;
		}
	}
}
Console.WriteLine(result);