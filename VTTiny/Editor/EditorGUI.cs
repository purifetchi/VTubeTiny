using System;
using System.Numerics;
using System.Reflection;
using ImGuiNET;
using Raylib_cs;
using VTTiny.Components;
using System.Linq;

namespace VTTiny.Editor
{
    public static class EditorGUI
    {
        /// <summary>
        /// This serves as a cache for the components, which we can then use in combo boxes for
        /// component instantiation.
        /// </summary>
        private static string[] ComponentTypeCache { get; set; } = null;

        /// <summary>
        /// Stores the current component index for the combo box.
        /// </summary>
        private static int comboBoxComponentIndex = 0;

        static EditorGUI()
        {
            CollectComponents();
        }

        /// <summary>
        /// Collects all of the types that derive from Component and caches them for later use
        /// in the component combo box.
        /// 
        /// TODO: This is insanely ugly, and won't work for external plugin components. I wish
        /// we didn't have to rely on reflection in this way.
        /// </summary>
        private static void CollectComponents()
        {
            var components = Assembly.GetAssembly(typeof(Component))
                                     .GetTypes()
                                     .Where(type => type.IsSubclassOf(typeof(Component)) && !type.IsAbstract && type.IsClass)
                                     .Select(type => type.FullName)
                                     .ToArray();

            ComponentTypeCache = components;
        }

        /// <summary>
        /// Constructs a draggable gui component for a given vector value.
        /// </summary>
        /// <param name="label">The label to attach to the gui component.</param>
        /// <param name="vector">The beginning vector.</param>
        /// <returns>The updated vector value.</returns>
        public static Vector2Int DragVector2(string label, Vector2Int vector)
        {
            var copy = (Vector2)vector;
            ImGui.DragFloat2(label, ref copy, 1f);

            return copy;
        }

        /// <summary>
        /// Constructs a text input gui component for a given string value.
        /// </summary>
        /// <param name="label">The label to attach to the gui component.</param>
        /// <param name="value">The string value.</param>
        /// <returns>The modified string value.</returns>
        public static string InputText(string label, string value)
        {
            ImGui.InputText(label, ref value, 1024);
            return value;
        }

        /// <summary>
        /// Constructs a draggable int gui component for a given integer value.
        /// </summary>
        /// <param name="label">The label to attach to the gui component.</param>
        /// <param name="value">The integer value.</param>
        /// <returns>The modified integer value.</returns>
        public static int DragInt(string label, int value)
        {
            ImGui.DragInt(label, ref value);
            return value;
        }

        /// <summary>
        /// Constructs a draggable float gui component for a given float value.
        /// </summary>
        /// <param name="label">The label to attach to the gui component.</param>
        /// <param name="value">The float value.</param>
        /// <param name="step">The step value (what it should increment by).</param>
        /// <returns>The modified float value.</returns>
        public static float DragFloat(string label, float value, float step = 1f)
        {
            ImGui.DragFloat(label, ref value, step);
            return value;
        }

        /// <summary>
        /// Constructs a color editor gui component for a given color value.
        /// </summary>
        /// <param name="label">The label to attach to the gui component.</param>
        /// <param name="color">The color value.</param>
        /// <returns>The modified color value.</returns>
        public static Color ColorEdit(string label, Color color)
        {
            var vec = new Vector4(color.r / 255f, color.g / 255f, color.b / 255f, color.a / 255f);
            ImGui.ColorEdit4(label, ref vec);
            return new Color((int)(vec.X * 255), (int)(vec.Y * 255), (int)(vec.Z * 255), (int)(vec.W * 255));
        }

        /// <summary>
        /// Creates an image button.
        /// </summary>
        /// <param name="texture">The texture to be used as the image.</param>
        /// <param name="x">The desired x size.</param>
        /// <param name="y">The desired y size.</param>
        /// <returns>Whether it was pressed or not.</returns>
        public static bool ImageButton(Texture2D? texture, int x, int y)
        {
            if (!texture.HasValue)
                return ImGui.Button("No Image", new Vector2(x, y));
            return ImGui.ImageButton(new IntPtr(texture.Value.id), new Vector2(x, y));
        }

        /// <summary>
        /// Creates a texture button that can be dropped onto.
        /// </summary>
        /// <param name="label">The label for the button.</param>
        /// <param name="originalTexture">The original texture, or null.</param>
        /// <param name="dimensions">The dimensions of the button.</param>
        /// <param name="newTexture">The new texture, if one was dropped.</param>
        /// <returns>Whether we had a new texture dropped.</returns>
        public static bool DragAndDropTextureButton(string label, Texture2D? originalTexture, Vector2Int dimensions, out Texture2D newTexture)
        {
            Text(label);

            ImageButton(originalTexture, dimensions.X, dimensions.Y);
            if (AcceptFileDrop(out string path))
            {
                newTexture = Raylib.LoadTexture(path);
                return true;
            }

            newTexture = default;
            return false;
        }

        /// <summary>
        /// Creates a texture button that can be dropped onto.
        /// </summary>
        /// <param name="label">The label for the button.</param>
        /// <param name="originalTexture">The original texture, or null.</param>
        /// <param name="newTexture">The new texture, if one was dropped.</param>
        /// <returns>Whether we had a new texture dropped.</returns>
        public static bool DragAndDropTextureButton(string label, Texture2D? originalTexture, out Texture2D newTexture)
        {
            return DragAndDropTextureButton(label, originalTexture, new Vector2Int(100, 100), out newTexture);
        }

        /// <summary>
        /// Accepts a file drop onto the last drawn gui component.
        /// </summary>
        /// <param name="path">The path of the file that was dropped.</param>
        /// <returns>Whether the last gui component had a file dropped onto it.</returns>
        public static bool AcceptFileDrop(out string path)
        {
            path = string.Empty;
            if (!ImGui.IsItemHovered() || !Raylib.IsFileDropped())
                return false;

            path = Raylib.GetDroppedFiles()[0];
            Raylib.ClearDroppedFiles();
            return true;
        }

        /// <summary>
        /// Creates a text label.
        /// </summary>
        /// <param name="text">The text.</param>
        public static void Text(string text)
        {
            ImGui.Text(text);
        }

        /// <summary>
        /// Creates a component dropdown with an add button.
        /// </summary>
        /// <param name="componentType">The type of the component (if it was added).</param>
        /// <returns>True if the add button was clicked.</returns>
        public static bool ComponentDropdown(out Type componentType)
        {
            ImGui.Combo(" ", ref comboBoxComponentIndex, ComponentTypeCache, ComponentTypeCache.Length);
            ImGui.SameLine();

            if (ImGui.Button("Add Component"))
            {
                componentType = Type.GetType(ComponentTypeCache[comboBoxComponentIndex]);
                return true;
            }

            componentType = default;
            return false;
        }
    }
}
