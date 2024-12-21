using System.Text;

string[] codesToPress = File.ReadAllLines("input.txt");

int codeResult = 0;
foreach (string code in codesToPress)
{
	string keyPadInput = ToNumericPadInput(code);
	string robot1Input = ToRobotInput(keyPadInput);
	string robot2Input = ToRobotInput(robot1Input);
	string backRobot1Input = ToRobotOutput(robot2Input);
	string backKeyPadInput = ToRobotOutput(backRobot1Input);
	string numericOutput = ToNumericOutput(backKeyPadInput);
	Console.WriteLine($"{code} {robot2Input}: {numericOutput}");
	codeResult += int.Parse(code.Substring(0, 3)) * robot2Input.Length;
}
Console.WriteLine(codeResult);


string ToNumericPadInput(string code)
{
	StringBuilder stringBuilder = new StringBuilder();
	(int x, int y) currentPos = (2, 3);
	foreach(char c in code)
	{
		(int x, int y) target = c switch
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
		if (target.x == 0 && currentPos.y == 3)
		{
			// prioritize up
			if (target.y - currentPos.y < 0)
			{
				stringBuilder.Append(Enumerable.Repeat('^', currentPos.y - target.y).ToArray());
			}
			stringBuilder.Append(Enumerable.Repeat('<', currentPos.x - target.x).ToArray());
			if (target.y - currentPos.y > 0)
			{
				stringBuilder.Append(Enumerable.Repeat('v', target.y - currentPos.y).ToArray());
			}
		}
		else if (target.y == 3)
		{
			// prioritize right
			if (target.x - currentPos.x > 0)
			{
				stringBuilder.Append(Enumerable.Repeat('>', target.x - currentPos.x).ToArray());
			}
			stringBuilder.Append(Enumerable.Repeat('v', target.y - currentPos.y).ToArray());
			if (target.x - currentPos.x < 0)
			{
				stringBuilder.Append(Enumerable.Repeat('<', currentPos.x - target.x).ToArray());
			}
		}
		else
		{
			if (target.x - currentPos.x > 0)
			{
				stringBuilder.Append(Enumerable.Repeat('>', target.x - currentPos.x).ToArray());
			}
			if (target.x - currentPos.x < 0)
			{
				stringBuilder.Append(Enumerable.Repeat('<', currentPos.x - target.x).ToArray());
			}
			if (target.y - currentPos.y < 0)
			{
				stringBuilder.Append(Enumerable.Repeat('^', currentPos.y - target.y).ToArray());
			}
			if (target.y - currentPos.y > 0)
			{
				stringBuilder.Append(Enumerable.Repeat('v', target.y - currentPos.y).ToArray());
			}
		}
		stringBuilder.Append('A');
		currentPos = target;
	}
	return stringBuilder.ToString();
}

string ToNumericOutput(string code)
{
	StringBuilder stringBuilder = new StringBuilder();
	(int x, int y) currentPos = (2, 3);
	foreach (char c in code)
	{
		switch (c)
		{
			case 'A':
				switch (currentPos)
				{
					case (2, 3): stringBuilder.Append('A'); break;
					case (1, 3): stringBuilder.Append('0'); break;
					case (0, 2): stringBuilder.Append('1'); break;
					case (1, 2): stringBuilder.Append('2'); break;
					case (2, 2): stringBuilder.Append('3'); break;
					case (0, 1): stringBuilder.Append('4'); break;
					case (1, 1): stringBuilder.Append('5'); break;
					case (2, 1): stringBuilder.Append('6'); break;
					case (0, 0): stringBuilder.Append('7'); break;
					case (1, 0): stringBuilder.Append('8'); break;
					case (2, 0): stringBuilder.Append('9'); break;
				}
				break;
			case '^':
				currentPos.y--;
				break;
			case 'v':
				currentPos.y++;
				break;
			case '<':
				currentPos.x--;
				break;
			case '>':
				currentPos.x++;
				break;
		}
		if (currentPos.x == 0 && currentPos.y == 3)
		{
			throw new InvalidOperationException("Robot is going haywire");
		}
	}
	return stringBuilder.ToString();
}

string ToRobotInput(string code)
{
	StringBuilder stringBuilder = new StringBuilder();
	(int x, int y) currentPos = (2, 0);
	foreach (char c in code)
	{
		(int x, int y) target = c switch
		{
			'A' => (2, 0),
			'^' => (1, 0),
			'<' => (0, 1),
			'v' => (1, 1),
			'>' => (2, 1),
			_ => throw new ArgumentException("Code contains illegal characters")
		};
		if (target.x == 0)
		{
			// we have to avoid hitting the gap, prioritize down
			stringBuilder.Append(Enumerable.Repeat('v', target.y - currentPos.y).ToArray());
			stringBuilder.Append(Enumerable.Repeat('<', currentPos.x - target.x).ToArray());
			// left is not an option, neither is up, because there is only one button below the gap
		}
		else if (target.y == 0)
		{
			// we have to avoid the gap, prioritize right
			if (target.x - currentPos.x > 0)
			{
				stringBuilder.Append(Enumerable.Repeat('>', target.x - currentPos.x).ToArray());
			}
			stringBuilder.Append(Enumerable.Repeat('^', currentPos.y - target.y).ToArray());
			if (target.x - currentPos.x < 0)
			{
				stringBuilder.Append(Enumerable.Repeat('<', currentPos.x - target.x).ToArray());
			}
		}
		else
		{
			if (target.x - currentPos.x < 0)
			{
				stringBuilder.Append(Enumerable.Repeat('<', currentPos.x - target.x).ToArray());
			}
			if (target.x - currentPos.x > 0)
			{
				stringBuilder.Append(Enumerable.Repeat('>', target.x - currentPos.x).ToArray());
			}
			if (target.y - currentPos.y > 0)
			{
				stringBuilder.Append(Enumerable.Repeat('v', target.y - currentPos.y).ToArray());
			}
			if (target.y - currentPos.y < 0)
			{
				stringBuilder.Append(Enumerable.Repeat('^', currentPos.y - target.y).ToArray());
			}
		}
		stringBuilder.Append('A');
		currentPos = target;
	}
	return stringBuilder.ToString();
}

string ToRobotOutput(string code)
{
	StringBuilder stringBuilder = new StringBuilder();
	(int x, int y) currentPos = (2, 0);
	foreach (char c in code)
	{
		switch(c)
		{
			case 'A':
				switch(currentPos)
				{
					case (2, 0): stringBuilder.Append('A'); break;
					case (1, 0):
						stringBuilder.Append('^'); break;
					case (0, 1):
						stringBuilder.Append('<'); break;
					case (1, 1):
						stringBuilder.Append('v'); break;
					case (2, 1):
						stringBuilder.Append('>'); break;
				}
				break;
			case '^':
				currentPos.y--;
				break;
			case 'v':
				currentPos.y++;
				break;
			case '<':
				currentPos.x--;
				break;
			case '>':
				currentPos.x++;
				break;
		}
		if(currentPos.x == 0 && currentPos.y == 0)
		{
			throw new InvalidOperationException("Robot is going haywire");
		}
	}
	return stringBuilder.ToString();
}