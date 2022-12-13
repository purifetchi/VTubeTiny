using System.Runtime.InteropServices;

namespace VTTiny.Editor.Native.Win32
{
    /// <summary>
    /// This class wraps the Win32 methods for opening open/save file dialog boxes.
    /// 
    /// NOTE: So far this is the only FileDialog implementation we have, it'd be nice to have more cross platform solutions.
    ///       PRs welcome.
    /// </summary>
    public static class FileDialog
    {
        private const string COMDLG_LIBRARY_NAME = "comdlg32";

        [DllImport(COMDLG_LIBRARY_NAME, SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool GetOpenFileName(ref OpenFileName ofn);

        /// <summary>
        /// Show an open file dialog.
        /// </summary>
        /// <returns>The path to the opened file, or nothing.</returns>
        public static string OpenFile()
        {
            var ofn = OpenFileName.Create();
            if (GetOpenFileName(ref ofn))
                return ofn.file;

            return string.Empty;
        }

        [DllImport(COMDLG_LIBRARY_NAME, SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool GetSaveFileName(ref OpenFileName ofn);

        /// <summary>
        /// Show a save file dialog.
        /// </summary>
        /// <returns>The path to the saved file, or nothing.</returns>
        public static string SaveFile()
        {
            var ofn = OpenFileName.Create();
            if (GetSaveFileName(ref ofn))
                return ofn.file;

            return string.Empty;
        }
    }
}
