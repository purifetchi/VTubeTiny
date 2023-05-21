﻿using System.Numerics;
using System.Text.Json;
using Raylib_cs;
using VTTiny.Assets;
using VTTiny.Components.Data;
using VTTiny.Editor;

namespace VTTiny.Components
{
    public class TextureRendererComponent : RendererComponent
    {
        /// <summary>
        /// The texture to be drawn. (Null by default.)
        /// </summary>
        public Texture Texture { get; private set; }

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

        /// <summary>
        /// The center of the texture.
        /// </summary>
        private Vector2 Center => new((Texture.Width * Scale) / 2f, (Texture.Height * Scale) / 2f);

        /// <summary>
        /// Set the texture for this texture renderer.
        /// </summary>
        /// <param name="texture">The texture to set.</param>
        public void SetTexture(Texture texture)
        {
            Texture = texture;
        }

        /// <inheritdoc/>
        public override void Render()
        {
            if (Texture == null)
                return;

            var dest = new Rectangle(
                Parent.Transform.Position.X, 
                Parent.Transform.Position.Y, 
                Texture.Width * Scale, 
                Texture.Height * Scale);

            Raylib.DrawTexturePro(
                Texture.BackingTexture, 
                Texture.SourceRect, 
                dest, 
                Center, 
                Rotation, 
                Tint);
        }

        /// <inheritdoc/>
        public override Rectangle GetBoundingBox()
        {
            if (Texture == null)
                return new Rectangle();

            return new Rectangle(
                Parent.Transform.Position.X - Center.X, 
                Parent.Transform.Position.Y - Center.Y, 
                Texture.Width * Scale, 
                Texture.Height * Scale
                );
        }

        /// <inheritdoc/>
        public override void InheritParametersFromConfig(JsonElement? parameters)
        {
            var config = JsonObjectToConfig<TextureRendererConfig>(parameters);

            Tint = config.Tint;
            Rotation = config.Rotation;
            Scale = config.Scale;

            SetTexture(config.Image?.Resolve(Parent.OwnerStage.AssetDatabase));
        }

        /// <inheritdoc/>
        public override void RenderEditorGUI()
        {
            if (EditorGUI.AssetDropdown("Texture", Parent.OwnerStage.AssetDatabase, Texture, out Texture newTexture))
                SetTexture(newTexture);

            Tint = EditorGUI.ColorEdit("Tint", Tint);
            Rotation = EditorGUI.DragFloat("Rotation", Rotation);
            Scale = EditorGUI.DragFloat("Scale", Scale, 0.005f);
        }

        /// <inheritdoc/>
        protected override object PackageParametersIntoConfig()
        {
            return new TextureRendererConfig
            {
                Image = Texture?.ToAssetReference<Texture>(),
                Tint = Tint,
                Rotation = Rotation,
                Scale = Scale
            };
        }
    }
}
