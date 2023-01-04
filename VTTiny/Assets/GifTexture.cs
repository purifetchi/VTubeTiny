using System;
using System.Diagnostics;
using System.Text.Json;
using Raylib_cs;
using VTTiny.Assets.Data;
using VTTiny.Editor;

namespace VTTiny.Assets
{
    /// <summary>
    /// This represents a texture loaded from a GIF file.
    /// 
    /// They're not too efficient, (we have to upload the data on the CPU side every new frame) 
    /// and should hopefully be replaced by spritesheets soon.
    /// </summary>
    public class GifTexture : Texture
    {
        public override Texture2D BackingTexture
        {
            get
            {
                if (_stopwatch.ElapsedMilliseconds >= FrameDelay)
                {
                    _stopwatch.Restart();
                    AdvanceFrame();
                }

                return _backingTexture;
            }
            protected set
            {
                _backingTexture = value;
            }
        }

        /// <summary>
        /// The frame delay (in msec).
        /// 
        /// Default value is ~30fps.
        /// </summary>
        public int FrameDelay { get; private set; } = 33;

        /// <summary>
        /// The backing image for this gif texture.
        /// </summary>
        private Image _image;

        /// <summary>
        /// The texture where the GIF will be loaded to.
        /// </summary>
        private Texture2D _backingTexture;

        /// <summary>
        /// A stopwatch for calculating the time between frames.
        /// </summary>
        private Stopwatch _stopwatch;

        /// <summary>
        /// The current frame.
        /// </summary>
        private int _frame = 0;

        /// <summary>
        /// The frame count.
        /// </summary>
        private int _frameCount = 0;

        /// <summary>
        /// Creates a new gif texture.
        /// </summary>
        public GifTexture()
            : base()
        {
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
        }

        /// <summary>
        /// Advances the current frame.
        /// </summary>
        private unsafe void AdvanceFrame()
        {
            _frame = (_frame + 1) % _frameCount;
            var offset = _image.width * _image.height * 4 * _frame;

            var dataPtr = (IntPtr)_image.data;
            dataPtr += offset;

            Raylib.UpdateTexture(_backingTexture, dataPtr.ToPointer());
        }

        public override void LoadTextureFromFile(string path)
        {
            Path = path;

            var frameCount = new int[1];
            _image = Raylib.LoadImageAnim(Path, frameCount);
            _frameCount = frameCount[0];

            BackingTexture = Raylib.LoadTextureFromImage(_image);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
                Raylib.UnloadImage(_image);
        }

        protected override void InternalRenderEditorGUI()
        {
            base.InternalRenderEditorGUI();
            FrameDelay = EditorGUI.DragInt("Frame delay", FrameDelay);

            EditorGUI.Text("Warning: GIF textures aren't ideal for performance.");
        }

        internal override void InheritParametersFromConfig(JsonElement? parameters)
        {
            base.InheritParametersFromConfig(parameters);

            var config = JsonObjectToConfig<GifTextureConfig>(parameters);
            FrameDelay = config.FrameDelay;
        }

        protected override object PackageParametersIntoConfig()
        {
            return new GifTextureConfig
            {
                FilteringMode = FilteringMode,
                FrameDelay = FrameDelay,
                Path = Path
            };
        }
    }
}
