using Raylib_cs;
using System;

namespace VTTiny.Rendering
{
    /// <summary>
    /// Framebuffer backed rendering context. Renders its contents to a render texture.
    /// </summary>
    internal class FramebufferRenderingContext : IRenderingContext, IDisposable
    {
        private RenderTexture2D _renderTexture;
        private bool _disposedValue;

        /// <summary>
        /// Sets up the render texture.
        /// </summary>
        public FramebufferRenderingContext()
        {
            _renderTexture = Raylib.LoadRenderTexture(800, 600);
        }

        public void Begin()
        {
            Raylib.BeginTextureMode(_renderTexture);
        }

        public void End()
        {
            Raylib.EndTextureMode();
        }

        public Texture2D? GetFramebuffer()
        {
            return _renderTexture.texture;
        }

        public void Resize(Vector2Int dimensions)
        {
            Raylib.UnloadRenderTexture(_renderTexture);
            _renderTexture = Raylib.LoadRenderTexture(dimensions.X, dimensions.Y);
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
