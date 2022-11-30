using Newtonsoft.Json.Linq;
using Raylib_cs;
using VTTiny.Components.Data;
using VTTiny.Editor;

namespace VTTiny.Components
{
    public class TextureRendererComponent : Component
    {
        /// <summary>
        /// The texture to be drawn. (Null by default.)
        /// </summary>
        public Texture Texture { get; set; }

        /// <summary>
        /// The tint of this texture. (White by default).
        /// </summary>
        public Color Tint { get; set; } = Color.WHITE;

        /// <summary>
        /// Rotation (in degrees) of the texture.
        /// </summary>
        public float Rotation { get; set; } = 0;

        /// <summary>
        /// Scale of the texture.
        /// </summary>
        public float Scale { get; set; } = 1f;

        public override void Render()
        {
            if (Texture == null)
                return;

            Raylib.DrawTextureEx(Texture.BackingTexture, Parent.Transform.Position, Rotation, Scale, Tint);
        }

        public override void Destroy()
        {
            if (Texture == null)
                return;

            Texture.Dispose();
        }

        internal override void InheritParametersFromConfig(JObject parameters)
        {
            var config = JsonObjectToConfig<TextureRendererConfig>(parameters);
            if (!string.IsNullOrEmpty(config.Image))
                Texture = new Texture(config.Image);

            Tint = config.Tint;
        }

        internal override void RenderEditorGUI()
        {
            if (EditorGUI.DragAndDropTextureButton("Texture", Texture, out Texture newTexture))
            {
                Texture?.Dispose();
                Texture = newTexture;
            }

            Tint = EditorGUI.ColorEdit("Tint", Tint);
            Rotation = EditorGUI.DragFloat("Rotation", Rotation);
            Scale = EditorGUI.DragFloat("Scale", Scale, 0.05f);
        }
    }
}
