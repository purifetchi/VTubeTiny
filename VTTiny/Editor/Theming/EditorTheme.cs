using System;
using System.Collections.Generic;
using System.Numerics;
using ImGuiNET;
using rlImGui_cs;

namespace VTTiny.Editor.Theming
{
    /// <summary>
    /// A theme for the editor.
    /// </summary>
    internal class EditorTheme
    {
        /// <summary>
        /// The font settings.
        /// </summary>
        public FontSettings Font { get; set; }

        /// <summary>
        /// The dictionary holding all of the color values.
        /// </summary>
        public Dictionary<string, Vector4> Colors { get; set; }

        public float Alpha { get; set; } = 1.0f;
        public float DisabledAlpha { get; set; } = 0.60f;
        public Vector2 WindowPadding { get; set; } = new Vector2(8, 8);
        public float WindowRounding { get; set; } = 0.0f;
        public float WindowBorderSize { get; set; } = 1.0f;
        public Vector2 WindowMinSize { get; set; } = new Vector2(32, 32);
        public Vector2 WindowTitleAlign { get; set; } = new Vector2(0.0f, 0.5f);
        public float ChildRounding { get; set; } = 0.0f;
        public float ChildBorderSize { get; set; } = 1.0f;
        public float PopupRounding { get; set; } = 0.0f;
        public float PopupBorderSize { get; set; } = 1.0f;
        public Vector2 FramePadding { get; set; } = new Vector2(4, 3);
        public float FrameRounding { get; set; } = 0.0f;
        public float FrameBorderSize { get; set; } = 0.0f;
        public Vector2 ItemSpacing { get; set; } = new Vector2(8, 4);
        public Vector2 ItemInnerSpacing { get; set; } = new Vector2(4, 4);
        public Vector2 CellPadding { get; set; } = new Vector2(4, 2);
        public Vector2 TouchExtraPadding { get; set; } = new Vector2(0, 0);
        public float IndentSpacing { get; set; } = 21.0f;
        public float ColumnsMinSpacing { get; set; } = 6.0f;
        public float ScrollbarSize { get; set; } = 14.0f;
        public float ScrollbarRounding { get; set; } = 9.0f;
        public float GrabMinSize { get; set; } = 12.0f;
        public float GrabRounding { get; set; } = 0.0f;
        public float LogSliderDeadzone { get; set; } = 4.0f;
        public float TabRounding { get; set; } = 4.0f;
        public float TabBorderSize { get; set; } = 0.0f;
        public float TabMinWidthForCloseButton { get; set; } = 0.0f;
        public Vector2 ButtonTextAlign { get; set; } = new Vector2(0.5f, 0.5f);
        public Vector2 SelectableTextAlign { get; set; } = new Vector2(0.0f, 0.0f);
        public Vector2 DisplayWindowPadding { get; set; } = new Vector2(19, 19);
        public Vector2 DisplaySafeAreaPadding { get; set; } = new Vector2(3, 3);
        public float MouseCursorScale { get; set; } = 1.0f;
        public bool AntiAliasedLines { get; set; } = true;
        public bool AntiAliasedLinesUseTex { get; set; } = true;
        public bool AntiAliasedFill { get; set; } = true;
        public float CurveTessellationTol { get; set; } = 1.25f;
        public float CircleTessellationMaxError { get; set; } = 0.30f;

        /// <summary>
        /// The font loaded from the FontSettings.
        /// </summary>
        private ImFontPtr? _font;

        /// <summary>
        /// Set this theme's values.
        /// </summary>
        internal void LoadTheme()
        {
            var style = ImGui.GetStyle();

            // Set each color
            var colors = style.Colors;
            if (Colors != null)
            {
                foreach (var item in Colors)
                {
                    if (!Enum.TryParse(item.Key, out ImGuiCol colorKey))
                    {
                        Console.WriteLine($"Unknown color key {item.Key}.");
                        continue;
                    }

                    colors[(int)colorKey] = item.Value;
                }
            }

            // Set all of the values
            style.Alpha = Alpha;
            style.DisabledAlpha = DisabledAlpha;
            style.WindowPadding = WindowPadding;
            style.WindowRounding = WindowRounding;
            style.WindowBorderSize = WindowBorderSize;
            style.WindowMinSize = WindowMinSize;
            style.WindowTitleAlign = WindowTitleAlign;
            style.ChildRounding = ChildRounding;
            style.ChildBorderSize = ChildBorderSize;
            style.PopupRounding = PopupRounding;
            style.PopupBorderSize = PopupBorderSize;
            style.FramePadding = FramePadding;
            style.FrameRounding = FrameRounding;
            style.FrameBorderSize = FrameBorderSize;
            style.ItemSpacing = ItemSpacing;
            style.ItemInnerSpacing = ItemInnerSpacing;
            style.CellPadding = CellPadding;
            style.TouchExtraPadding = TouchExtraPadding;
            style.IndentSpacing = IndentSpacing;
            style.ColumnsMinSpacing = ColumnsMinSpacing;
            style.ScrollbarSize = ScrollbarSize;
            style.ScrollbarRounding = ScrollbarRounding;
            style.GrabMinSize = GrabMinSize;
            style.GrabRounding = GrabRounding;
            style.LogSliderDeadzone = LogSliderDeadzone;
            style.TabRounding = TabRounding;
            style.TabBorderSize = TabBorderSize;
            style.TabMinWidthForCloseButton = TabMinWidthForCloseButton;
            style.ButtonTextAlign = ButtonTextAlign;
            style.SelectableTextAlign = SelectableTextAlign;
            style.DisplayWindowPadding = DisplayWindowPadding;
            style.DisplaySafeAreaPadding = DisplaySafeAreaPadding;
            style.MouseCursorScale = MouseCursorScale;
            style.AntiAliasedLines = AntiAliasedLines;
            style.AntiAliasedLinesUseTex = AntiAliasedLinesUseTex;
            style.AntiAliasedFill = AntiAliasedFill;
            style.CurveTessellationTol = CurveTessellationTol;
            style.CircleTessellationMaxError = CircleTessellationMaxError;

            // Load the font.
            _font = Font?.LoadFont();
        }

        /// <summary>
        /// Push this theme's font.
        /// </summary>
        internal void PushFont()
        {
            if (_font.HasValue)
                ImGui.PushFont(_font.Value);
        }

        /// <summary>
        /// Pop this theme's font.
        /// </summary>
        internal void PopFont()
        {
            if (_font.HasValue)
                ImGui.PopFont();
        }

        /// <summary>
        /// Unload these settings.
        /// </summary>
        internal void UnloadTheme(bool exitting)
        {
            if (exitting)
                return;

            _font?.Destroy();
            rlImGui.ReloadFonts();
            ImGui.StyleColorsClassic();
        }
    }
}
