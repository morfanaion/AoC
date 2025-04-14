Console.WriteLine(File.ReadAllLines("Input.txt").Count(IsNiceString));

bool IsNiceString(string arg)
{
	return arg.Skip(2).Zip(arg).Any(pair => pair.First == pair.Second) && HasDoublePair(arg);
}

bool HasDoublePair(string arg)
{
	for (int i1 = 0; i1 < arg.Length - 3; i1++)
	{
		for (int i2 = i1 + 2; i2 < arg.Length - 1; i2++)
		{
			if (arg[i1] == arg[i2] && arg[i1 + 1] == arg[i2 + 1])
			{
				return true;
			}
		}
	}
	return false;
}