namespace VTTiny
{
    public struct Vector2Int
    {
        public int X, Y;

        public static implicit operator Vector2Int(System.Numerics.Vector2 vec)
        {
            return new Vector2Int((int)vec.X, (int)vec.Y);
        }

        public static Vector2Int operator-(Vector2Int left, Vector2Int right)
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

        public Vector2Int(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"[{X}, {Y}]";
        }
    }
}
