namespace VTTiny.Editor.Native
{
    /// <summary>
    /// This is the multiplatform wrapper for the open/save file dialog functionality.
    /// It just calls back into the platform-specific implementations.
    /// 
    /// NOTE: So far the only FileDialog implementation we have is for Win32.
    ///       PRs to improve that are welcome.
    /// </summary>
    public static class FileDialog
    {
        /// <summary>
        /// Show an open file dialog.
        /// </summary>
        /// <returns>The path to the opened file, or nothing.</returns>
        public static string OpenFile()
        {
#if ARCH_WINDOWS
            return Win32.FileDialog.OpenFile();
#else
            throw new System.NotImplementedException($"OpenFile() is not yet supported on {System.Runtime.InteropServices.RuntimeInformation.OSDescription}!");
#endif
        }

        /// <summary>
        /// Show a save file dialog.
        /// </summary>
        /// <returns>The path to the saved file, or nothing.</returns>
        public static string SaveFile()
        {
#if ARCH_WINDOWS
            return Win32.FileDialog.SaveFile();
#else
            throw new System.NotImplementedException($"SaveFile() is not yet supported on {System.Runtime.InteropServices.RuntimeInformation.OSDescription}!");
#endif
        }
    }
}
