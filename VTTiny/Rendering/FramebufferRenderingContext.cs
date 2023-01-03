using System;
using Raylib_cs;

namespace VTTiny.Rendering
{
    /// <summary>
    /// Framebuffer backed rendering context. Renders its contents to a render texture.
    /// </summary>
    internal class FramebufferRenderingContext : IRenderingContext
    {
        protected RenderTexture2D _renderTexture;
        private bool _disposedValue;

        private bool _drawInFullscreen;

        /// <summary>
        /// Sets up the render texture.
        /// </summary>
        public FramebufferRenderingContext()
        {
            _renderTexture = Raylib.LoadRenderTexture(800, 600);
        }

        public virtual void Begin(Color color)
        {
            Raylib.BeginTextureMode(_renderTexture);
            Raylib.ClearBackground(color);
        }

        public virtual void End()
        {
            Raylib.EndTextureMode();

            if (_drawInFullscreen)
                DrawFullscreen();
        }

        public Texture2D? GetFramebuffer()
        {
            return _renderTexture.texture;
        }

        public virtual void Resize(Vector2Int dimensions)
        {
            Raylib.UnloadRenderTexture(_renderTexture);
            _renderTexture = Raylib.LoadRenderTexture(dimensions.X, dimensions.Y);
        }

        public void SetCanDrawFullscreen(bool enabled)
        {
            _drawInFullscreen = enabled;
        }

        /// <summary>
        /// Draws this framebuffer to the entire screen.
        /// </summary>
        private void DrawFullscreen()
        {
            Raylib.BeginDrawing();

            var rect = new Rectangle(0, 0, _renderTexture.texture.width, -_renderTexture.texture.height);
            Raylib.DrawTextureRec(_renderTexture.texture, rect, Vector2Int.Zero, Color.WHITE);

            Raylib.EndDrawing();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                    Raylib.UnloadRenderTexture(_renderTexture);

                _disposedValue = true;
            }
        }

        ~FramebufferRenderingContext()
        {
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
