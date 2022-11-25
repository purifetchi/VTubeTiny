using Newtonsoft.Json.Linq;
using Raylib_cs;
using VTTiny.Components.Data;

namespace VTTiny.Components
{
    public class TextRendererComponent : Component
    {
        /// <summary>
        /// The text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// The font size. (20px by default.)
        /// </summary>
        public int FontSize { get; set; } = 20;

        /// <summary>
        /// The color of the text. (Black by default.)
        /// </summary>
        public Color Color { get; set; } = Color.BLACK;

        public override void Render()
        {
            Raylib.DrawText(Text, Parent.Transform.Position.X, Parent.Transform.Position.Y, FontSize, Color);
        }

        internal override void InheritParametersFromConfig(JObject parameters)
        {
            var config = parameters.ToObject<TextRendererConfig>();

            Text = config.Text;
            FontSize = config.FontSize;
            Color = config.Color;
        }
    }
}
