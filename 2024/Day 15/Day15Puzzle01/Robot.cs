namespace Day15Puzzle01
{
	internal class Robot : Entity
	{
		public Robot() 
		{
			TheRobot = this;
		}
		public static Robot TheRobot { get; private set; } = new Robot();
		public override bool Fixed => false;

		public override char Symbol => '@';
	}
}
