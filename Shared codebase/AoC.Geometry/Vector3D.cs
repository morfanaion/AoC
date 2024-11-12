using static System.Math;

namespace AoC.Geometry
{
	public struct Vector3D : IVector
	{
		public double X { get; set; }
		public double Y { get; set; }
		public double Z { get; set; }

		public double Length => Sqrt(LengthSquared);
		public double LengthSquared => Pow(X, 2) + Pow(Y, 2) + Pow(Z, 2);

		public double Dot(Vector3D v) => X * v.X + Y * v.Y + Z * v.Z;
		public IVector Cross(IVector v)
		{
			throw new NotImplementedException();
		}

		public double Dot(IVector v)
		{
			if (v is Vector2D v2d)
			{
				return Dot(new Vector3D() { X = v2d.X, Y = v2d.Y, Z = 0 });
			}
			if (v is Vector3D v3d)
			{
				return Dot(v3d);
			}
			throw new ArgumentException($"{nameof(v)} must be of type {typeof(Vector2D)} or {typeof(Vector3D)}", nameof(v));
		}

		public static Vector3D operator +(Vector3D left, Vector3D right) => new Vector3D() { X = left.X + right.X, Y = left.Y + right.Y, Z = left.Z + right.Z };
		public static Vector3D operator -(Vector3D left, Vector3D right) => new Vector3D() { X = left.X - right.X, Y = left.Y - right.Y, Z = left.Z - right.Z };
		public static Vector3D operator *(Vector3D left, double right) => new Vector3D() { X = left.X * right, Y = left.Y * right, Z = left.Z * right };
		public static Vector3D operator *(double left, Vector3D right) => new Vector3D() { X = left * right.X, Y = left * right.Y, Z = left * right.Z };
		public static Vector3D operator /(Vector3D left, double right) => new Vector3D() { X = left.X / right, Y = left.Y / right, Z = left.Z / right };
	}
}
