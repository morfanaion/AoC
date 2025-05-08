using Day25Puzzle01;
string[] input = File.ReadAllLines("input.txt");
for(int i = 0; i < input.Length; i += 8)
{
	if(input[i].StartsWith('#'))
	{
		Lock.CreateLock(input[i..(i + 6)].ToArray());
	}
	else
	{
		Key.CreateKey(input[i..(i + 6)].ToArray());
	}
}
Console.WriteLine(Lock.Locks.Sum(@lock => Key.Keys.Count(key => key.PinSizes.Zip(@lock.PinSizes).All(pin => pin.First + pin.Second <= 5))));