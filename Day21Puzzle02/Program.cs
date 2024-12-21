string[] codesToPress = File.ReadAllLines("input.txt");
Dictionary<(string, int), long> keyPressesDictionary = new Dictionary<(string, int), long>();

long result = 0;
foreach(string code in codesToPress)
{
	long minimum = MinNumKeyPresses((2, 3), code.Select(KeyPadKeyToPosition), (0, 3), 25);
	Console.WriteLine($"{code}: {minimum}");
	result += minimum * long.Parse(code.Substring(0, 3));
}
Console.WriteLine(result);

(int, int) KeyPadKeyToPosition(char c) => c switch
{
	'A' => (2, 3),
	'0' => (1, 3),
	'1' => (0, 2),
	'2' => (1, 2),
	'3' => (2, 2),
	'4' => (0, 1),
	'5' => (1, 1),
	'6' => (2, 1),
	'7' => (0, 0),
	'8' => (1, 0),
	'9' => (2, 0),
	_ => throw new ArgumentException("Code contains illegal characters")
};

long MinNumKeyPresses((int x, int y) start, IEnumerable<(int x, int y)> positions, (int x, int y) forbidden, int robotsInBetween)
{
	long result = 0;
	if (robotsInBetween == 0)
	{
		foreach ((int x, int y) position in positions)
		{
			result += PossibleKeyPresses(start, position, forbidden).Min(s => s.Length);
			start = position;
		}
	}
	else
	{
		foreach ((int x, int y) position in positions)
		{
			result += PossibleKeyPresses(start, position, forbidden).Min(s =>
			{
				if(keyPressesDictionary.TryGetValue((s, robotsInBetween), out long min))
				{
					return min;
				}
				return keyPressesDictionary[(s, robotsInBetween)] = MinNumKeyPresses((2, 0), s.Select(RemoteKeyToPosition), (0, 0), robotsInBetween - 1);
			});
			start = position;
		}
	}
	return result;
}

(int, int) RemoteKeyToPosition(char c) => c switch
{
	'A' => (2, 0),
	'^' => (1, 0),
	'<' => (0, 1),
	'v' => (1, 1),
	'>' => (2, 1),
	_ => throw new ArgumentException("Code contains illegal characters")
};

IEnumerable<string> PossibleKeyPresses((int x, int y) start, (int x, int y) end, (int x, int y) forbidden)
{
	if(end == start)
	{
		yield return "A";
	}
	Func<int, int> IncrementX = i => i + 1;
	Func<int, int> IncrementY = i => i + 1;
	string xPress = ">";
	string yPress = "v";
	if (start.x > end.x)
	{
		IncrementX = i => i - 1;
		xPress = "<";
	}
	if (start.y > end.y)
	{
		IncrementY = i => i - 1;
		yPress = "^";
	}
	if(start.x != end.x)
	{
		if ((IncrementX(start.x), start.y) != forbidden)
		{
			foreach (string option in PossibleKeyPresses((IncrementX(start.x), start.y), end, forbidden))
			{
				yield return xPress + option;
			}
		}
	}
	if (start.y != end.y)
	{
		if ((start.x, IncrementY(start.y)) != forbidden)
		{
			foreach (string option in PossibleKeyPresses((start.x, IncrementY(start.y)), end, forbidden))
			{
				yield return yPress + option;
			}
		}
	}
}