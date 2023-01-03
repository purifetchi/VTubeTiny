using Raylib_cs;
using Spout.NET;

namespace VTTiny.Rendering
{
#if ARCH_WINDOWS
    /// <summary>
    /// A framebuffer context that also sends the render texture after drawing to Spout.
    /// </summary>
    internal class SpoutFramebufferRenderingContext : FramebufferRenderingContext
    {
        /// <summary>
        /// The sender name for Spout.
        /// </summary>
        private const string SPOUT_SENDER_NAME = "VTubeTiny";

        /// <summary>
        /// The Texture2D constant for OpenGL.
        /// </summary>
        private const uint GL_TEXTURE_2D = 3553u;

        /// <summary>
        /// The Spout sender.
        /// </summary>
        private SpoutSender _sender;

        /// <summary>
        /// Constructs a new spout framebuffer rendering context and sets up the spout sender.
        /// </summary>
        public SpoutFramebufferRenderingContext()
            : base()
        {
            _sender = new SpoutSender();
            _sender.CreateSender(SPOUT_SENDER_NAME, (uint)_renderTexture.texture.width, (uint)_renderTexture.texture.height, 0);
        }

        public override void Begin(Color _)
        {
            Raylib.BeginTextureMode(_renderTexture);

            // We clear the stage with a blank color for transparency.
            Raylib.ClearBackground(Color.BLANK);
        }

        public override void End()
        {
            base.End();

            // Send the data straight to Spout.
            _sender.SendTexture(_renderTexture.texture.id, GL_TEXTURE_2D, (uint)_renderTexture.texture.width, (uint)_renderTexture.texture.height, true, 0);
        }

        public override void Resize(Vector2Int dimensions)
        {
            base.Resize(dimensions);

            // Resize the sender as well.
            _sender.UpdateSender(SPOUT_SENDER_NAME, (uint)dimensions.X, (uint)dimensions.Y);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing &&
                _sender != null)
            {
                _sender.ReleaseSender(0);
                _sender.Dispose();

                _sender = null;
            }
        }
    }
#else
    /// <summary>
    /// Mock framebuffer context for non-Windows platforms. (Spout is not supported on non-windows architectures)
    /// </summary>
    
    internal class SpoutFramebufferRenderingContext : FramebufferRenderingContext
    {
    }
#endif
}
