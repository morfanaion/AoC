using Day15Puzzle01;

string movementInstructions = string.Empty;
Entity?[][] grid = ProcessFile().ToArray();

foreach (char c in movementInstructions)
{
	switch(c)
	{
		case '<':
			MoveLeft();
			break;
		case '>':
			MoveRight();
			break;
		case '^':
			MoveUp();
			break;
		case 'v':
			MoveDown();
			break;
	}
	if (Box.AllBoxes.All(b => b.Fixed))
	{
		// no box can be moved anymore, stop
		break;
	}
}

Console.WriteLine(Box.AllBoxes.Sum(b => b.GPSCoordinate));

void PrintState()
{
	for (int y = 0; y < grid.Length; y++)
	{
		for(int x = 0; x < grid[y].Length; x++)
		{
			if (grid[y][x] is Entity entity)
			{
				Console.Write(entity.Symbol);
			}
			else
			{
				Console.Write('.');
			}
		}
		Console.WriteLine();
	}
}

void PerformMoves(int dx, int dy)
{
	Stack<Entity> stack = new Stack<Entity>();
	stack.Push(Robot.TheRobot);
	int currentX = Robot.TheRobot.X;
	int currentY = Robot.TheRobot.Y;
	currentX += dx;
	currentY += dy;
	while (grid[currentY][currentX] is Entity entity && !entity.Fixed)
	{
		stack.Push(entity);
		currentX += dx;
		currentY += dy;

	}
	Entity firstToMove = stack.Peek();
	{
		if (grid[firstToMove.Y + dy][firstToMove.X + dx] != null)
		{
			// we hit a wall or a fixed item, we can't move
			return;
		}
	}
	while (stack.Count > 0)
	{
		Entity entity = stack.Pop();
		
		grid[entity.Y][entity.X] = null;
		entity.X += dx;
		entity.Y += dy;
		grid[entity.Y][entity.X] = entity;
		if(entity is Box box)
		{
			if ((grid[box.Y][box.X - 1] is Entity && entity.Fixed ||
				grid[box.Y][box.X + 1] is Entity && entity.Fixed) &&
				(grid[box.Y - 1][box.X] is Entity && entity.Fixed ||
				grid[box.Y + 1][box.X] is Entity && entity.Fixed))
			{
				box.SetFixed();
			}
		}
	}
}

void MoveLeft()
{
	PerformMoves(-1, 0);
}

void MoveRight()
{
	PerformMoves(1, 0);
}

void MoveUp()
{
	PerformMoves(0, -1);
}

void MoveDown()
{
	PerformMoves(0, 1);
}

IEnumerable<Entity?[]> ProcessFile()
{
	int y = 0;
	bool processingMovement = false;
	foreach (string line in File.ReadAllLines("input.txt"))
	{
		if (!processingMovement)
		{
			if (string.IsNullOrEmpty(line))
			{
				processingMovement = true; continue;
			}
			yield return ProcessLine(line, y).ToArray();
			y++;
		}
		else
		{
			movementInstructions += line;
		}
	}
}

IEnumerable<Entity?> ProcessLine(string str, int y)
{
	int x = 0;
	foreach(char c in str)
	{
		yield return CreateEntity(c, x, y);
		x++;
	}
}

Entity? CreateEntity(char c, int x, int y)
{
	switch (c)
	{
		case '#': return new Wall() { X = x, Y = y };
		case '@': return new Robot() { X = x, Y = y };
		case 'O': return new Box() { X = x, Y = y };
		default: return null;
	}
}