using System;
using System.Runtime.InteropServices;

namespace VTTiny.Editor.Native.Win32
{
    /// <summary>
    /// The OpenFileDialog struct required for calling GetOpenFileName.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    internal struct OpenFileName
    {
        private const int MAX_FILE_NAME_LENGTH = 256;
        private const int MAX_FILE_TITLE_LENGTH = 64;

        /// <summary>
        /// Constructs a new OpenFileName struct with generic data.
        /// </summary>
        /// <returns>The OpenFileName struct.</returns>
        public static OpenFileName Create()
        {
            var ofn = new OpenFileName
            {
                structSize = Marshal.SizeOf<OpenFileName>(),
                filter = "Scene data\0*.json\0",
                defExt = "json",

                file = new(new char[MAX_FILE_NAME_LENGTH]),
                maxFile = MAX_FILE_NAME_LENGTH,

                fileTitle = new(new char[MAX_FILE_TITLE_LENGTH]),
                maxFileTitle = MAX_FILE_TITLE_LENGTH
            };

            return ofn;
        }

        public int structSize;
        public IntPtr dlgOwner;
        public IntPtr instance;

        public string filter;
        public string customFilter;
        public int maxCustFilter;
        public int filterIndex;

        public string file;
        public int maxFile;

        public string fileTitle;
        public int maxFileTitle;

        public string initialDir;

        public string title;

        public int flags;
        public short fileOffset;
        public short fileExtension;

        public string defExt;

        public IntPtr custData;
        public IntPtr hook;

        public string templateName;

        public IntPtr reservedPtr;
        public int reservedInt;
        public int flagsEx;
    }
}
