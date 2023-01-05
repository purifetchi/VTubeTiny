using ImGuiNET;
using rlImGui_cs;

namespace VTTiny.Editor.Theming
{
    /// <summary>
    /// The font settings.
    /// </summary>
    internal class FontSettings
    {
        /// <summary>
        /// The path to the font file.
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// The font size.
        /// </summary>
        public float Size { get; set; }

        /// <summary>
        /// Loads the font based on these settings.
        /// </summary>
        /// <returns>The pointer to the font</returns>
        public ImFontPtr LoadFont()
        {
            var font = ImGui.GetIO().Fonts.AddFontFromFileTTF(FilePath, Size);
            rlImGui.ReloadFonts();

            return font;
        }
    }
}
