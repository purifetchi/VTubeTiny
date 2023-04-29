using System.Text.Json;
using Raylib_cs;
using VTTiny.Components.Data;
using VTTiny.Editor;

namespace VTTiny.Components
{
    public class TextRendererComponent : RendererComponent
    {
        /// <summary>
        /// The text.
        /// </summary>
        public string Text { get; set; } = string.Empty;

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

        public override Rectangle GetBoundingBox()
        {
            return new Rectangle(Parent.Transform.Position.X, Parent.Transform.Position.Y, Raylib.MeasureText(Text, FontSize), FontSize);
        }

        public override void InheritParametersFromConfig(JsonElement? parameters)
        {
            var config = JsonObjectToConfig<TextRendererConfig>(parameters);

            Text = config.Text;
            FontSize = config.FontSize;
            Color = config.Color;
        }

        public override void RenderEditorGUI()
        {
            Text = EditorGUI.InputText("Text", Text);
            Color = EditorGUI.ColorEdit("Color", Color);
            FontSize = EditorGUI.DragInt("Font size", FontSize);
        }

        protected override object PackageParametersIntoConfig()
        {
            return new TextRendererConfig
            {
                Color = Color,
                Text = Text,
                FontSize = FontSize
            };
        }
    }
}
