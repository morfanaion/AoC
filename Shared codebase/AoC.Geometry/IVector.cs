namespace AoC.Geometry
{
	public interface IVector
	{
		double Length { get; }
		double LengthSquared { get; }

		double Dot(IVector v);
		IVector Cross(IVector v);
		double GreatestAbsoluteDimension { get; }
		double SumOfDimensions { get; }
        double SumOfAbsoluteDimensions { get; }
    }
}
