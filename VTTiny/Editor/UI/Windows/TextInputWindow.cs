using System;
using ImGuiNET;
using Raylib_cs;

namespace VTTiny.Editor.UI
{
    /// <summary>
    /// A text input window.
    /// </summary>
    internal class TextInputWindow : EditorWindow
    {
        /// <summary>
        /// The callback to invoke after the window is done.
        /// </summary>
        private Action<string> Callback { get; set; }

        /// <summary>
        /// The initial string.
        /// </summary>
        private string _initialString;

        /// <summary>
        /// Creates a new text input window with a given callback.
        /// </summary>
        /// <param name="name">The name of the window.</param>
        /// <param name="callback">The callback to invoke after we're done.</param>
        public TextInputWindow(string name, string initial, Action<string> callback)
            : base(name, ImGuiWindowFlags.NoDocking | ImGuiWindowFlags.NoSavedSettings)
        {
            Callback = callback;

            _initialString = initial;
        }

        protected override void PreDrawUI()
        {
            ImGui.SetNextWindowSizeConstraints(new Vector2Int(300, 20), new Vector2Int(int.MaxValue, int.MaxValue));
        }

        protected override void DrawUI()
        {
            ImGui.InputText("", ref _initialString, 2048);
            ImGui.SameLine();

            var enterPressed = ImGui.IsWindowFocused() && Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER);
            if (ImGui.SmallButton("OK") || enterPressed)
            {
                Callback?.Invoke(_initialString);
                Editor.RemoveWindow(this);
            }
        }
    }
}
