using Day15Puzzle01;

string movementInstructions = string.Empty;
Entity?[][] grid = ProcessFile().ToArray();

foreach (char c in movementInstructions)
{
	switch (c)
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
				if (entity is not Robot)
				{
					x++;
				}
			}
			else
			{
				Console.Write('.');
			}
		}
		Console.WriteLine();
	}
}

IEnumerable<Entity> GetPushedEntities(IEnumerable<Entity> entities, int dx, int dy)
{
	if (dx == 0)
	{
		// we're moving along the y axis... just... great... now we might be branching, two boxes might be pushing the same item, a lotta crap to fix here
		List<Entity> entitiesBeingPushed = new List<Entity>();
		foreach (Entity entity in entities)
		{
			if (entity is Robot)
			{
				// well, he can at least move only one thing at a time, see if he's pushing anything and return it, cause in this call, he can only be the only one pushing yet
				if (grid[entity.Y + dy][entity.X] is Entity otherEntity)
				{
					return Enumerable.Empty<Entity>().Append(otherEntity);
				}
			}
			else
			{
				if (grid[entity.Y + dy][entity.X] is Entity otherEntity)
				{
					entitiesBeingPushed.Add(otherEntity);
				}
				if (grid[entity.Y + dy][entity.X + 1] is Entity otherEntity2)
				{
					entitiesBeingPushed.Add(otherEntity2);
				}
			}
		}
		return entitiesBeingPushed.Distinct();
	}
	else
	{
		// we're moving along the x axis, yes, this is much simpler, however, there is distinction between positive and negative X
		if (dx > 0)
		{
			foreach (Entity entity in entities)
			{
				if (entity is Robot)
				{
					// should be only 1 ever
					if (grid[entity.Y][entity.X + dx] is Entity otherEntity)
					{
						return Enumerable.Empty<Entity>().Append(otherEntity);
					}
				}
				else
				{
					// should be only 1 ever
					if (grid[entity.Y][entity.X + 2 * dx] is Entity otherEntity)
					{
						return Enumerable.Empty<Entity>().Append(otherEntity);
					}
				}
			}
		}
		else
		{
			foreach (Entity entity in entities)
			{
				// should be only 1 ever
				if (grid[entity.Y][entity.X + dx] is Entity otherEntity)
				{
					return Enumerable.Empty<Entity>().Append(otherEntity);
				}
			}
		}
	}
	return Enumerable.Empty<Entity>();
}


void PerformMoves(int dx, int dy)
{
	Stack<IEnumerable<Entity>> stack = new Stack<IEnumerable<Entity>>();
	IEnumerable<Entity> entitiesToPush = Enumerable.Empty<Entity>().Append(Robot.TheRobot);
	int currentX = Robot.TheRobot.X;
	int currentY = Robot.TheRobot.Y;
	while(entitiesToPush.Any())
	{
		if (entitiesToPush.Any(e => e.Fixed))
		{
			// we found an entity that can't be pushed, stop!
			return;
		}
		stack.Push(entitiesToPush);
		entitiesToPush = GetPushedEntities(entitiesToPush, dx, dy);
	}

	while (stack.Count > 0)
	{
		IEnumerable<Entity> entities = stack.Pop();
		foreach (Entity entity in entities)
		{
			if (entity is Box box)
			{
				grid[entity.Y][entity.X] = null;
				grid[entity.Y][entity.X + 1] = null;
				entity.X += dx;
				entity.Y += dy;
				grid[entity.Y][entity.X] = entity;
				grid[entity.Y][entity.X + 1] = entity;

				if ((grid[box.Y][box.X - 1] is Entity && entity.Fixed ||
					grid[box.Y][box.X + 2] is Entity && entity.Fixed) &&
					(grid[box.Y - 1][box.X] is Entity && entity.Fixed ||
					grid[box.Y + 1][box.X] is Entity && entity.Fixed ||
					grid[box.Y + 1][box.X + 1] is Entity && entity.Fixed ||
					grid[box.Y + 1][box.X + 1] is Entity && entity.Fixed))
				{
					box.SetFixed();
				}
			}
			else
			{
				grid[entity.Y][entity.X] = null;
				entity.X += dx;
				entity.Y += dy;
				grid[entity.Y][entity.X] = entity;
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
		Entity? entity = CreateEntity(c, x, y);
		yield return entity;
		x+=2;
		if (entity is not Robot)
		{
			yield return entity;
		}
		else
		{
			yield return null;
		}
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