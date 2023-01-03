using Raylib_cs;

namespace VTTiny.Rendering
{
    /// <summary>
    /// Generic rendering context. Draws directly to the Raylib framebuffer.
    /// </summary>
    internal class GenericRaylibRenderingContext : IRenderingContext
    {
        public void Begin(Color color)
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(color);
        }

        public void End()
        {
            Raylib.EndDrawing();
        }

        public Texture2D? GetFramebuffer()
        {
            return null;
        }

        public void Resize(Vector2Int dimensions)
        {
            Raylib.SetWindowSize(dimensions.X, dimensions.Y);
        }

        public void Dispose()
        {

        }
    }
}
