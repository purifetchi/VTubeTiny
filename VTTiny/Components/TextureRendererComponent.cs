﻿using Newtonsoft.Json.Linq;
using Raylib_cs;
using VTTiny.Components.Data;

namespace VTTiny.Components
{
    public class TextureRendererComponent : Component
    {
        /// <summary>
        /// The texture to be drawn. (Null by default.)
        /// </summary>
        public Texture2D? Texture { get; set; }

        /// <summary>
        /// The tint of this texture. (White by default).
        /// </summary>
        public Color Tint { get; set; } = Color.WHITE;

        public override void Render()
        {
            if (!Texture.HasValue)
                return;

            Raylib.DrawTexture(Texture.Value, Parent.Transform.Position.X, Parent.Transform.Position.Y, Tint);
        }

        internal override void InheritParametersFromConfig(JObject parameters)
        {
            var config = parameters?.ToObject<TextureRendererConfig>() ?? new TextureRendererConfig();
            if (!string.IsNullOrEmpty(config.Image))
                Texture = Raylib.LoadTexture(config.Image);

            Tint = config.Tint;
        }
    }
}
