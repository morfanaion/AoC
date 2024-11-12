using System.Security.AccessControl;
using static System.Math;

namespace AoC.Geometry
{
	public struct Vector2D : IVector
	{
		public double X { get; set; }
		public double Y { get; set; }

		public double Length => Sqrt(LengthSquared);

		public double LengthSquared => Pow(X, 2) + Pow(Y, 2);

		public double Dot(Vector2D v) => X * v.X + Y * v.Y;
		double IVector.Dot(IVector v)
		{
			if (v is Vector2D v2d)
			{
				return Dot(v2d);
			}
			if (v is Vector3D v3d)
			{
				return v3d.Dot(new Vector3D() { X = X, Y = Y, Z = 0 });
			}
			throw new ArgumentException($"{nameof(v)} must be of type {typeof(Vector2D)} or {typeof(Vector3D)}", nameof(v));
		}

		public IVector Cross(IVector v) => new Vector2D() { X = 0, Y = 0 };

		public static Vector2D operator +(Vector2D left, Vector2D right) => new Vector2D() { X = left.X + right.X, Y = left.Y + right.Y };
		public static Vector2D operator -(Vector2D left, Vector2D right) => new Vector2D() { X = left.X - right.X, Y = left.Y - right.Y };

		public static Vector2D operator *(Vector2D left, double right) => new Vector2D() { X = left.X * right, Y = left.Y * right };
		public static Vector2D operator *(double left, Vector2D right) => new Vector2D() { X = left * right.X, Y = left * right.Y };

		public static Vector2D operator /(Vector2D left, double right) => new Vector2D() { X = left.X / right, Y = left.Y / right };
	}
}
