using System.Numerics;

namespace VTTiny
{
    /// <summary>
    /// A 2D vector that contains two integer values.
    /// </summary>
    public struct Vector2Int
    {
        public int X, Y;

        public static Vector2Int Zero => new Vector2Int(0, 0);

        public static implicit operator Vector2Int(Vector2 vec)
        {
            return new Vector2Int((int)vec.X, (int)vec.Y);
        }

        public static implicit operator Vector2(Vector2Int vec)
        {
            return new Vector2(vec.X, vec.Y);
        }

        public static Vector2Int operator -(Vector2Int left, Vector2Int right)
        {
            return new Vector2Int(left.X - right.X, left.Y - right.Y);
        }

        public static Vector2Int operator +(Vector2Int left, Vector2Int right)
        {
            return new Vector2Int(left.X + right.X, left.Y + right.Y);
        }

        public static Vector2Int operator *(Vector2Int vec, float scalar)
        {
            return new Vector2Int((int)(vec.X * scalar), (int)(vec.Y * scalar));
        }

        public static bool operator ==(Vector2Int left, Vector2Int right)
        {
            return left.X == right.X &&
                   left.Y == right.Y;
        }

        public static bool operator !=(Vector2Int left, Vector2Int right)
        {
            return !(left == right);
        }

        public Vector2Int(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            return obj is Vector2Int vec &&
                   this == vec;
        }

        public override int GetHashCode()
        {
            return System.HashCode.Combine(X, Y);
        }

        public override string ToString()
        {
            return $"[{X}, {Y}]";
        }
    }
}
