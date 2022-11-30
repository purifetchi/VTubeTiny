using Raylib_cs;

namespace VTTiny.Components
{
    /// <summary>
    /// This abstract class is derived by all components that can be rendered.
    /// </summary>
    public abstract class RendererComponent : Component
    {
        /// <summary>
        /// Gets the bounding box for this renderer component.
        /// </summary>
        /// <returns>The bounding box of this renderer component.</returns>
        public virtual Rectangle GetBoundingBox() { return new Rectangle(); }


        /// <summary>
        /// The rendering method, called after Update.
        /// </summary>
        public virtual void Render() { }

        /// <summary>
        /// Draws the bounding box of this renderer component.
        /// </summary>
        internal void DrawBoundingBox()
        {
            Raylib.DrawRectangleRec(GetBoundingBox(), new Color(255, 0, 0, 100));
        }
    }
}
