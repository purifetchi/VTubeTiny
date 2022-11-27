using Raylib_cs;

namespace VTTiny.Components.Data
{
    internal class TextRendererConfig
    {
        public string Text { get; set; } = string.Empty;
        public Color Color { get; set; } = Color.BLACK;
        public int FontSize { get; set; } = 30;
    }
}
