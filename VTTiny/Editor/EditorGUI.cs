using System.Numerics;
using ImGuiNET;
using Raylib_cs;

namespace VTTiny.Editor
{
    public static class EditorGUI
    {
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
        /// <returns>The modified float value.</returns>
        public static float DragFloat(string label, float value)
        {
            ImGui.DragFloat(label, ref value);
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
    }
}
