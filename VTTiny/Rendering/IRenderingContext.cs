using System;
using Raylib_cs;

namespace VTTiny.Rendering
{
    /// <summary>
    /// A rendering context. Responsible for managing the drawing systems.
    /// </summary>
    public interface IRenderingContext : IDisposable
    {
        /// <summary>
        /// Enters drawing mode.
        /// </summary>
        /// <param name="color">The color to clear the context with.</param>
        void Begin(Color color);

        /// <summary>
        /// Resizes the rendering context.
        /// </summary>
        /// <param name="dimensions">The new dimensions.</param>
        void Resize(Vector2Int dimensions);

        /// <summary>
        /// Leaves drawing mode.
        /// </summary>
        void End();

        /// <summary>
        /// Sets whether this rendering context can present itself in fullscreen.
        /// </summary>
        /// <param name="enabled">Whether this rendering context can present itself in fullscreen.</param>
        void SetCanDrawFullscreen(bool enabled);

        /// <summary>
        /// Gets the framebuffer (if possible).
        /// </summary>
        /// <returns>The framebuffer.</returns>
        Texture2D? GetFramebuffer();
    }
}
