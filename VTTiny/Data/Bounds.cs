namespace VTTiny.Data
{
    public struct Bounds
    {
        public Vector2Int Position { get; private set; }
        public Vector2Int Extents { get; private set; }

        public Bounds(Vector2Int position, Vector2Int extents)
        {
            Position = position;
            Extents = extents;
        }

        public bool PointWithin(Vector2Int point)
        {
            var top = Position.Y;
            var bottom = Position.Y + Extents.Y;
            var left = Position.X;
            var right = Position.X + Extents.X;

            return point.X >= left && point.X <= right &&
                   point.Y >= top && point.Y <= bottom;
        }
    }
}
