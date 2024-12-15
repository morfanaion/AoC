namespace Day15Puzzle01
{
	internal class Wall : Entity
	{
		public override bool Fixed => true;

		public override char Symbol => '#';
	}
}
