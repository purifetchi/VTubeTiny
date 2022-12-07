using ImGuiNET;

namespace VTTiny.Editor.UI
{
    /// <summary>
    /// The about window for VTubeTiny.
    /// </summary>
    internal class AboutWindow : EditorWindow
    {
        /// <summary>
        /// The editor.
        /// </summary>
        private VTubeTinyEditor Editor { get; set; }

        /// <summary>
        /// The license of VTubeTiny.
        /// </summary>
        private const string LICENSE = @"MIT License

Copyright (c) 2022 prefetcher

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the 'Software'), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED 'AS IS', WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
";

        /// <summary>
        /// Constructs a new About VTubeTiny window.
        /// </summary>
        public AboutWindow(VTubeTinyEditor editor)
            : base("About VTubeTiny", ImGuiWindowFlags.NoDocking)
        {
            Editor = editor;
        }

        protected override void DrawUI()
        {
            ImGui.Text("VTubeTiny - tiny VTuber/PNGTuber suite with minimal overhead.");
            ImGui.Text("(c) prefetcher, et al. 2022, licensed under MIT (the license follows).");

            ImGui.NewLine();
            ImGui.Separator();
            ImGui.Text(LICENSE);
            ImGui.Separator();
            ImGui.NewLine();

            ImGui.Text("View the project on GitHub here: https://github.com/naomiEve/VTubeTiny");

            if (ImGui.SmallButton("Close"))
                Editor.RemoveWindow(this);
        }
    }
}
