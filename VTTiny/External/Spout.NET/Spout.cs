using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

#pragma warning disable IDE1006
#pragma warning disable IDE0060
#pragma warning disable CA2101
#pragma warning disable CA1507

[assembly: InternalsVisibleTo("Spout.NET")]
namespace Spout.NET
{
    public unsafe partial class SHELLEXECUTEINFOA
    {
        [StructLayout(LayoutKind.Explicit, Size = 112)]
        public partial struct __Internal
        {
            [FieldOffset(0)]
            internal uint cbSize;

            [FieldOffset(4)]
            internal uint fMask;

            [FieldOffset(8)]
            internal IntPtr hwnd;

            [FieldOffset(16)]
            internal IntPtr lpVerb;

            [FieldOffset(24)]
            internal IntPtr lpFile;

            [FieldOffset(32)]
            internal IntPtr lpParameters;

            [FieldOffset(40)]
            internal IntPtr lpDirectory;

            [FieldOffset(48)]
            internal int nShow;

            [FieldOffset(56)]
            internal IntPtr hInstApp;

            [FieldOffset(64)]
            internal IntPtr lpIDList;

            [FieldOffset(72)]
            internal IntPtr lpClass;

            [FieldOffset(80)]
            internal IntPtr hkeyClass;

            [FieldOffset(88)]
            internal uint dwHotKey;

            [FieldOffset(96)]
            internal _0.__Internal _0;

            [FieldOffset(104)]
            internal IntPtr hProcess;
        }

        public unsafe partial struct _0
        {
            [StructLayout(LayoutKind.Explicit, Size = 8)]
            public partial struct __Internal
            {
                [FieldOffset(0)]
                internal IntPtr hIcon;

                [FieldOffset(0)]
                internal IntPtr hMonitor;
            }
        }
    }


    public enum D3DFORMAT
    {
    }

    public enum D3D_FEATURE_LEVEL
    {
    }

    public enum D3D_DRIVER_TYPE
    {
    }

    public enum DXGI_FORMAT
    {
    }


    public enum SpoutCreateResult
    {
        SPOUT_CREATE_FAILED = 0,
        SPOUT_CREATE_SUCCESS = 1,
        SPOUT_ALREADY_EXISTS = 2,
        SPOUT_ALREADY_CREATED = 3
    }

    public unsafe partial class SpoutSharedMemory : IDisposable
    {
        [StructLayout(LayoutKind.Explicit, Size = 48)]
        public partial struct __Internal
        {
            [FieldOffset(0)]
            internal IntPtr m_pBuffer;

            [FieldOffset(8)]
            internal IntPtr m_hMap;

            [FieldOffset(16)]
            internal IntPtr m_hMutex;

            [FieldOffset(24)]
            internal int m_lockCount;

            [FieldOffset(32)]
            internal IntPtr m_pName;

            [FieldOffset(40)]
            internal int m_size;

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "??0SpoutSharedMemory@@QEAA@XZ")]
            internal static extern IntPtr ctor(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "??0SpoutSharedMemory@@QEAA@AEBV0@@Z")]
            internal static extern IntPtr cctor(IntPtr __instance, IntPtr _0);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "??1SpoutSharedMemory@@QEAA@XZ")]
            internal static extern void dtor(IntPtr __instance, int delete);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?Create@SpoutSharedMemory@@QEAA?AW4SpoutCreateResult@@PEBDH@Z")]
            internal static extern SpoutCreateResult Create(IntPtr __instance, [MarshalAs(UnmanagedType.LPUTF8Str)] string name, int size);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?Open@SpoutSharedMemory@@QEAA_NPEBD@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool Open(IntPtr __instance, [MarshalAs(UnmanagedType.LPUTF8Str)] string name);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?Close@SpoutSharedMemory@@QEAAXXZ")]
            internal static extern void Close(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?Unlock@SpoutSharedMemory@@QEAAXXZ")]
            internal static extern void Unlock(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?Debug@SpoutSharedMemory@@QEAAXXZ")]
            internal static extern void Debug(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?Lock@SpoutSharedMemory@@QEAAPEADXZ")]
            internal static extern sbyte* Lock(IntPtr __instance);
        }

        public IntPtr __Instance { get; protected set; }

        internal static readonly System.Collections.Concurrent.ConcurrentDictionary<IntPtr, SpoutSharedMemory> NativeToManagedMap = new();

        protected bool __ownsNativeInstance;

        internal static SpoutSharedMemory __CreateInstance(IntPtr native, bool skipVTables = false)
        {
            return new SpoutSharedMemory(native.ToPointer(), skipVTables);
        }

        internal static SpoutSharedMemory __CreateInstance(SpoutSharedMemory.__Internal native, bool skipVTables = false)
        {
            return new SpoutSharedMemory(native, skipVTables);
        }

        private static void* __CopyValue(SpoutSharedMemory.__Internal native)
        {
            IntPtr ret = Marshal.AllocHGlobal(sizeof(SpoutSharedMemory.__Internal));
            *(SpoutSharedMemory.__Internal*)ret = native;
            return ret.ToPointer();
        }

        private SpoutSharedMemory(SpoutSharedMemory.__Internal native, bool skipVTables = false)
            : this(__CopyValue(native), skipVTables)
        {
            __ownsNativeInstance = true;
            NativeToManagedMap[__Instance] = this;
        }

        protected SpoutSharedMemory(void* native, bool skipVTables = false)
        {
            if (native == null)
            {
                return;
            }

            __Instance = new IntPtr(native);
        }

        public SpoutSharedMemory()
        {
            __Instance = Marshal.AllocHGlobal(sizeof(SpoutSharedMemory.__Internal));
            __ownsNativeInstance = true;
            NativeToManagedMap[__Instance] = this;
            _ = __Internal.ctor(__Instance);
        }

        public SpoutSharedMemory(SpoutSharedMemory _0)
        {
            __Instance = Marshal.AllocHGlobal(sizeof(SpoutSharedMemory.__Internal));
            __ownsNativeInstance = true;
            NativeToManagedMap[__Instance] = this;
            *(SpoutSharedMemory.__Internal*)__Instance = *(SpoutSharedMemory.__Internal*)_0.__Instance;
        }

        ~SpoutSharedMemory()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (__Instance == IntPtr.Zero)
            {
                return;
            }

            _ = NativeToManagedMap.TryRemove(__Instance, out _);
            if (disposing)
            {
                __Internal.dtor(__Instance, 0);
            }

            if (__ownsNativeInstance)
            {
                Marshal.FreeHGlobal(__Instance);
            }

            __Instance = IntPtr.Zero;
        }

        public SpoutCreateResult Create(string name, int size)
        {
            SpoutCreateResult __ret = __Internal.Create(__Instance, name, size);
            return __ret;
        }

        public bool Open(string name)
        {
            bool __ret = __Internal.Open(__Instance, name);
            return __ret;
        }

        public void Close()
        {
            __Internal.Close(__Instance);
        }

        public void Unlock()
        {
            __Internal.Unlock(__Instance);
        }

        public void Debug()
        {
            __Internal.Debug(__Instance);
        }

        public sbyte* Lock
        {
            get
            {
                sbyte* __ret = __Internal.Lock(__Instance);
                return __ret;
            }
        }
    }

    public unsafe partial class SpoutMemoryShare : IDisposable
    {
        [StructLayout(LayoutKind.Explicit, Size = 16)]
        public partial struct __Internal
        {
            [FieldOffset(0)]
            internal IntPtr senderMem;

            [FieldOffset(8)]
            internal uint m_Width;

            [FieldOffset(12)]
            internal uint m_Height;

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "??0spoutMemoryShare@@QEAA@XZ")]
            internal static extern IntPtr ctor(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "??0spoutMemoryShare@@QEAA@AEBV0@@Z")]
            internal static extern IntPtr cctor(IntPtr __instance, IntPtr _0);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "??1spoutMemoryShare@@QEAA@XZ")]
            internal static extern void dtor(IntPtr __instance, int delete);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?CreateSenderMemory@spoutMemoryShare@@QEAA_NPEBDII@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool CreateSenderMemory(IntPtr __instance, [MarshalAs(UnmanagedType.LPUTF8Str)] string sendername, uint width, uint height);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?UpdateSenderMemorySize@spoutMemoryShare@@QEAA_NPEBDII@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool UpdateSenderMemorySize(IntPtr __instance, [MarshalAs(UnmanagedType.LPUTF8Str)] string sendername, uint width, uint height);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?OpenSenderMemory@spoutMemoryShare@@QEAA_NPEBD@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool OpenSenderMemory(IntPtr __instance, [MarshalAs(UnmanagedType.LPUTF8Str)] string sendername);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?CloseSenderMemory@spoutMemoryShare@@QEAAXXZ")]
            internal static extern void CloseSenderMemory(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GetSenderMemorySize@spoutMemoryShare@@QEAA_NAEAI0@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool GetSenderMemorySize(IntPtr __instance, uint* width, uint* height);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?UnlockSenderMemory@spoutMemoryShare@@QEAAXXZ")]
            internal static extern void UnlockSenderMemory(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?ReleaseSenderMemory@spoutMemoryShare@@QEAAXXZ")]
            internal static extern void ReleaseSenderMemory(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?LockSenderMemory@spoutMemoryShare@@QEAAPEAEXZ")]
            internal static extern byte* LockSenderMemory(IntPtr __instance);
        }

        public IntPtr __Instance { get; protected set; }

        internal static readonly System.Collections.Concurrent.ConcurrentDictionary<IntPtr, SpoutMemoryShare> NativeToManagedMap = new();

        protected bool __ownsNativeInstance;

        internal static SpoutMemoryShare __CreateInstance(IntPtr native, bool skipVTables = false)
        {
            return new SpoutMemoryShare(native.ToPointer(), skipVTables);
        }

        internal static SpoutMemoryShare __CreateInstance(SpoutMemoryShare.__Internal native, bool skipVTables = false)
        {
            return new SpoutMemoryShare(native, skipVTables);
        }

        private static void* __CopyValue(SpoutMemoryShare.__Internal native)
        {
            IntPtr ret = Marshal.AllocHGlobal(sizeof(SpoutMemoryShare.__Internal));
            *(SpoutMemoryShare.__Internal*)ret = native;
            return ret.ToPointer();
        }

        private SpoutMemoryShare(SpoutMemoryShare.__Internal native, bool skipVTables = false)
            : this(__CopyValue(native), skipVTables)
        {
            __ownsNativeInstance = true;
            NativeToManagedMap[__Instance] = this;
        }

        protected SpoutMemoryShare(void* native, bool skipVTables = false)
        {
            if (native == null)
            {
                return;
            }

            __Instance = new IntPtr(native);
        }

        public SpoutMemoryShare()
        {
            __Instance = Marshal.AllocHGlobal(sizeof(SpoutMemoryShare.__Internal));
            __ownsNativeInstance = true;
            NativeToManagedMap[__Instance] = this;
            _ = __Internal.ctor(__Instance);
        }

        public SpoutMemoryShare(SpoutMemoryShare _0)
        {
            __Instance = Marshal.AllocHGlobal(sizeof(SpoutMemoryShare.__Internal));
            __ownsNativeInstance = true;
            NativeToManagedMap[__Instance] = this;
            *(SpoutMemoryShare.__Internal*)__Instance = *(SpoutMemoryShare.__Internal*)_0.__Instance;
        }

        ~SpoutMemoryShare()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (__Instance == IntPtr.Zero)
            {
                return;
            }

            _ = NativeToManagedMap.TryRemove(__Instance, out _);
            if (disposing)
            {
                __Internal.dtor(__Instance, 0);
            }

            if (__ownsNativeInstance)
            {
                Marshal.FreeHGlobal(__Instance);
            }

            __Instance = IntPtr.Zero;
        }

        public bool CreateSenderMemory(string sendername, uint width, uint height)
        {
            bool __ret = __Internal.CreateSenderMemory(__Instance, sendername, width, height);
            return __ret;
        }

        public bool UpdateSenderMemorySize(string sendername, uint width, uint height)
        {
            bool __ret = __Internal.UpdateSenderMemorySize(__Instance, sendername, width, height);
            return __ret;
        }

        public bool OpenSenderMemory(string sendername)
        {
            bool __ret = __Internal.OpenSenderMemory(__Instance, sendername);
            return __ret;
        }

        public void CloseSenderMemory()
        {
            __Internal.CloseSenderMemory(__Instance);
        }

        public bool GetSenderMemorySize(ref uint width, ref uint height)
        {
            fixed (uint* __width0 = &width)
            {
                uint* __arg0 = __width0;
                fixed (uint* __height1 = &height)
                {
                    uint* __arg1 = __height1;
                    bool __ret = __Internal.GetSenderMemorySize(__Instance, __arg0, __arg1);
                    return __ret;
                }
            }
        }

        public void UnlockSenderMemory()
        {
            __Internal.UnlockSenderMemory(__Instance);
        }

        public void ReleaseSenderMemory()
        {
            __Internal.ReleaseSenderMemory(__Instance);
        }

        protected SpoutSharedMemory SenderMem
        {
            get
            {
                SpoutSharedMemory __result0 = ((SpoutMemoryShare.__Internal*)__Instance)->senderMem == IntPtr.Zero
                    ? null
                    : SpoutSharedMemory.NativeToManagedMap.ContainsKey(((SpoutMemoryShare.__Internal*)__Instance)->senderMem)
                    ? SpoutSharedMemory.NativeToManagedMap[((SpoutMemoryShare.__Internal*)__Instance)->senderMem]
                    : SpoutSharedMemory.__CreateInstance(((SpoutMemoryShare.__Internal*)__Instance)->senderMem);
                return __result0;
            }

            set => ((SpoutMemoryShare.__Internal*)__Instance)->senderMem = value is null ? IntPtr.Zero : value.__Instance;
        }

        protected uint MWidth
        {
            get => ((SpoutMemoryShare.__Internal*)__Instance)->m_Width;

            set => ((SpoutMemoryShare.__Internal*)__Instance)->m_Width = value;
        }

        protected uint MHeight
        {
            get => ((SpoutMemoryShare.__Internal*)__Instance)->m_Height;

            set => ((SpoutMemoryShare.__Internal*)__Instance)->m_Height = value;
        }

        public byte* LockSenderMemory
        {
            get
            {
                byte* __ret = __Internal.LockSenderMemory(__Instance);
                return __ret;
            }
        }
    }

    public unsafe partial class SharedTextureInfo : IDisposable
    {
        [StructLayout(LayoutKind.Explicit, Size = 280)]
        public partial struct __Internal
        {
            [FieldOffset(0)]
            internal uint shareHandle;

            [FieldOffset(4)]
            internal uint width;

            [FieldOffset(8)]
            internal uint height;

            [FieldOffset(12)]
            internal uint format;

            [FieldOffset(16)]
            internal uint usage;

            [FieldOffset(20)]
            internal fixed char description[128];

            [FieldOffset(276)]
            internal uint partnerId;

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "??0SharedTextureInfo@@QEAA@AEBU0@@Z")]
            internal static extern IntPtr cctor(IntPtr __instance, IntPtr _0);
        }

        public IntPtr __Instance { get; protected set; }

        internal static readonly System.Collections.Concurrent.ConcurrentDictionary<IntPtr, SharedTextureInfo> NativeToManagedMap = new();

        protected bool __ownsNativeInstance;

        internal static SharedTextureInfo __CreateInstance(IntPtr native, bool skipVTables = false)
        {
            return new SharedTextureInfo(native.ToPointer(), skipVTables);
        }

        internal static SharedTextureInfo __CreateInstance(SharedTextureInfo.__Internal native, bool skipVTables = false)
        {
            return new SharedTextureInfo(native, skipVTables);
        }

        private static void* __CopyValue(SharedTextureInfo.__Internal native)
        {
            IntPtr ret = Marshal.AllocHGlobal(sizeof(SharedTextureInfo.__Internal));
            *(SharedTextureInfo.__Internal*)ret = native;
            return ret.ToPointer();
        }

        private SharedTextureInfo(SharedTextureInfo.__Internal native, bool skipVTables = false)
            : this(__CopyValue(native), skipVTables)
        {
            __ownsNativeInstance = true;
            NativeToManagedMap[__Instance] = this;
        }

        protected SharedTextureInfo(void* native, bool skipVTables = false)
        {
            if (native == null)
            {
                return;
            }

            __Instance = new IntPtr(native);
        }

        public SharedTextureInfo(SharedTextureInfo _0)
        {
            __Instance = Marshal.AllocHGlobal(sizeof(SharedTextureInfo.__Internal));
            __ownsNativeInstance = true;
            NativeToManagedMap[__Instance] = this;
            *(SharedTextureInfo.__Internal*)__Instance = *(SharedTextureInfo.__Internal*)_0.__Instance;
        }

        public SharedTextureInfo()
        {
            __Instance = Marshal.AllocHGlobal(sizeof(SharedTextureInfo.__Internal));
            __ownsNativeInstance = true;
            NativeToManagedMap[__Instance] = this;
        }

        ~SharedTextureInfo()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (__Instance == IntPtr.Zero)
            {
                return;
            }

            _ = NativeToManagedMap.TryRemove(__Instance, out _);
            if (__ownsNativeInstance)
            {
                Marshal.FreeHGlobal(__Instance);
            }

            __Instance = IntPtr.Zero;
        }

        public uint ShareHandle
        {
            get => ((SharedTextureInfo.__Internal*)__Instance)->shareHandle;

            set => ((SharedTextureInfo.__Internal*)__Instance)->shareHandle = value;
        }

        public uint Width
        {
            get => ((SharedTextureInfo.__Internal*)__Instance)->width;

            set => ((SharedTextureInfo.__Internal*)__Instance)->width = value;
        }

        public uint Height
        {
            get => ((SharedTextureInfo.__Internal*)__Instance)->height;

            set => ((SharedTextureInfo.__Internal*)__Instance)->height = value;
        }

        public uint Format
        {
            get => ((SharedTextureInfo.__Internal*)__Instance)->format;

            set => ((SharedTextureInfo.__Internal*)__Instance)->format = value;
        }

        public uint Usage
        {
            get => ((SharedTextureInfo.__Internal*)__Instance)->usage;

            set => ((SharedTextureInfo.__Internal*)__Instance)->usage = value;
        }

        public char[] Description
        {
            get
            {
                char[] __value = null;
                if (((SharedTextureInfo.__Internal*)__Instance)->description != null)
                {
                    __value = new char[128];
                    for (int i = 0; i < 128; i++)
                    {
                        __value[i] = ((SharedTextureInfo.__Internal*)__Instance)->description[i];
                    }
                }
                return __value;
            }

            set
            {
                if (value != null)
                {
                    for (int i = 0; i < 128; i++)
                    {
                        ((SharedTextureInfo.__Internal*)__Instance)->description[i] = value[i];
                    }
                }
            }
        }

        public uint PartnerId
        {
            get => ((SharedTextureInfo.__Internal*)__Instance)->partnerId;

            set => ((SharedTextureInfo.__Internal*)__Instance)->partnerId = value;
        }
    }

    public unsafe partial class SpoutSenderNames : IDisposable
    {
        [StructLayout(LayoutKind.Explicit, Size = 112)]
        public partial struct __Internal
        {
            [FieldOffset(0)]
            internal SpoutSharedMemory.__Internal m_senderNames;

            [FieldOffset(48)]
            internal SpoutSharedMemory.__Internal m_activeSender;

            [FieldOffset(96)]
            internal IntPtr m_senders;

            [FieldOffset(104)]
            internal int m_MaxSenders;

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "??0spoutSenderNames@@QEAA@XZ")]
            internal static extern IntPtr ctor(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "??0spoutSenderNames@@QEAA@AEBV0@@Z")]
            internal static extern IntPtr cctor(IntPtr __instance, IntPtr _0);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "??1spoutSenderNames@@QEAA@XZ")]
            internal static extern void dtor(IntPtr __instance, int delete);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?RegisterSenderName@spoutSenderNames@@QEAA_NPEBD@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool RegisterSenderName(IntPtr __instance, [MarshalAs(UnmanagedType.LPUTF8Str)] string senderName);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?ReleaseSenderName@spoutSenderNames@@QEAA_NPEBD@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool ReleaseSenderName(IntPtr __instance, [MarshalAs(UnmanagedType.LPUTF8Str)] string senderName);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?FindSenderName@spoutSenderNames@@QEAA_NPEBD@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool FindSenderName(IntPtr __instance, [MarshalAs(UnmanagedType.LPUTF8Str)] string Sendername);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GetSenderNameInfo@spoutSenderNames@@QEAA_NHPEADHAEAI1AEAPEAX@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool GetSenderNameInfo(IntPtr __instance, int index, sbyte* sendername, int sendernameMaxSize, uint* width, uint* height, void** dxShareHandle);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GetSenderInfo@spoutSenderNames@@QEAA_NPEBDAEAI1AEAPEAXAEAK@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool GetSenderInfo(IntPtr __instance, [MarshalAs(UnmanagedType.LPUTF8Str)] string sendername, uint* width, uint* height, void** dxShareHandle, uint* dwFormat);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?SetSenderInfo@spoutSenderNames@@QEAA_NPEBDIIPEAXK@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool SetSenderInfo(IntPtr __instance, [MarshalAs(UnmanagedType.LPUTF8Str)] string sendername, uint width, uint height, IntPtr dxShareHandle, uint dwFormat);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?getSharedInfo@spoutSenderNames@@QEAA_NPEBDPEAUSharedTextureInfo@@@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool GetSharedInfo(IntPtr __instance, [MarshalAs(UnmanagedType.LPUTF8Str)] string SenderName, IntPtr info);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?setSharedInfo@spoutSenderNames@@QEAA_NPEBDPEAUSharedTextureInfo@@@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool SetSharedInfo(IntPtr __instance, [MarshalAs(UnmanagedType.LPUTF8Str)] string SenderName, IntPtr info);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?SetActiveSender@spoutSenderNames@@QEAA_NPEBD@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool SetActiveSender(IntPtr __instance, [MarshalAs(UnmanagedType.LPUTF8Str)] string Sendername);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GetActiveSender@spoutSenderNames@@QEAA_NQEAD@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool GetActiveSender(IntPtr __instance, sbyte[] Sendername);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GetActiveSenderInfo@spoutSenderNames@@QEAA_NPEAUSharedTextureInfo@@@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool GetActiveSenderInfo(IntPtr __instance, IntPtr info);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?FindActiveSender@spoutSenderNames@@QEAA_NQEADAEAI1AEAPEAXAEAK@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool FindActiveSender(IntPtr __instance, sbyte[] activename, uint* width, uint* height, void** hSharehandle, uint* dwFormat);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?CreateSender@spoutSenderNames@@QEAA_NPEBDIIPEAXK@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool CreateSender(IntPtr __instance, [MarshalAs(UnmanagedType.LPUTF8Str)] string sendername, uint width, uint height, IntPtr hSharehandle, uint dwFormat);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?UpdateSender@spoutSenderNames@@QEAA_NPEBDIIPEAXK@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool UpdateSender(IntPtr __instance, [MarshalAs(UnmanagedType.LPUTF8Str)] string sendername, uint width, uint height, IntPtr hSharehandle, uint dwFormat);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?CheckSender@spoutSenderNames@@QEAA_NPEBDAEAI1AEAPEAXAEAK@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool CheckSender(IntPtr __instance, [MarshalAs(UnmanagedType.LPUTF8Str)] string sendername, uint* width, uint* height, void** hSharehandle, uint* dwFormat);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?FindSender@spoutSenderNames@@QEAA_NPEADAEAI1AEAPEAXAEAK@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool FindSender(IntPtr __instance, sbyte* sendername, uint* width, uint* height, void** hSharehandle, uint* dwFormat);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?SenderDebug@spoutSenderNames@@QEAA_NPEBDH@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool SenderDebug(IntPtr __instance, [MarshalAs(UnmanagedType.LPUTF8Str)] string Sendername, int size);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?CreateSenderSet@spoutSenderNames@@IEAA_NXZ")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool CreateSenderSet(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?setActiveSenderName@spoutSenderNames@@IEAA_NPEBD@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool SetActiveSenderName(IntPtr __instance, [MarshalAs(UnmanagedType.LPUTF8Str)] string SenderName);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?getActiveSenderName@spoutSenderNames@@IEAA_NQEAD@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool GetActiveSenderName(IntPtr __instance, sbyte[] SenderName);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?cleanSenderSet@spoutSenderNames@@IEAAXXZ")]
            internal static extern void CleanSenderSet(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GetSenderCount@spoutSenderNames@@QEAAHXZ")]
            internal static extern int GetSenderCount(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GetMaxSenders@spoutSenderNames@@QEAAHXZ")]
            internal static extern int GetMaxSenders(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?SetMaxSenders@spoutSenderNames@@QEAAXH@Z")]
            internal static extern void SetMaxSenders(IntPtr __instance, int maxSenders);
        }

        public IntPtr __Instance { get; protected set; }

        internal static readonly System.Collections.Concurrent.ConcurrentDictionary<IntPtr, SpoutSenderNames> NativeToManagedMap = new();

        protected bool __ownsNativeInstance;

        internal static SpoutSenderNames __CreateInstance(IntPtr native, bool skipVTables = false)
        {
            return new SpoutSenderNames(native.ToPointer(), skipVTables);
        }

        internal static SpoutSenderNames __CreateInstance(SpoutSenderNames.__Internal native, bool skipVTables = false)
        {
            return new SpoutSenderNames(native, skipVTables);
        }

        private static void* __CopyValue(SpoutSenderNames.__Internal native)
        {
            IntPtr ret = Marshal.AllocHGlobal(sizeof(SpoutSenderNames.__Internal));
            *(SpoutSenderNames.__Internal*)ret = native;
            return ret.ToPointer();
        }

        private SpoutSenderNames(SpoutSenderNames.__Internal native, bool skipVTables = false)
            : this(__CopyValue(native), skipVTables)
        {
            __ownsNativeInstance = true;
            NativeToManagedMap[__Instance] = this;
        }

        protected SpoutSenderNames(void* native, bool skipVTables = false)
        {
            if (native == null)
            {
                return;
            }

            __Instance = new IntPtr(native);
        }

        public SpoutSenderNames()
        {
            __Instance = Marshal.AllocHGlobal(sizeof(SpoutSenderNames.__Internal));
            __ownsNativeInstance = true;
            NativeToManagedMap[__Instance] = this;
            _ = __Internal.ctor(__Instance);
        }

        public SpoutSenderNames(SpoutSenderNames _0)
        {
            __Instance = Marshal.AllocHGlobal(sizeof(SpoutSenderNames.__Internal));
            __ownsNativeInstance = true;
            NativeToManagedMap[__Instance] = this;
            *(SpoutSenderNames.__Internal*)__Instance = *(SpoutSenderNames.__Internal*)_0.__Instance;
        }

        ~SpoutSenderNames()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (__Instance == IntPtr.Zero)
            {
                return;
            }

            _ = NativeToManagedMap.TryRemove(__Instance, out _);
            if (disposing)
            {
                __Internal.dtor(__Instance, 0);
            }

            if (__ownsNativeInstance)
            {
                Marshal.FreeHGlobal(__Instance);
            }

            __Instance = IntPtr.Zero;
        }

        public bool RegisterSenderName(string senderName)
        {
            bool __ret = __Internal.RegisterSenderName(__Instance, senderName);
            return __ret;
        }

        public bool ReleaseSenderName(string senderName)
        {
            bool __ret = __Internal.ReleaseSenderName(__Instance, senderName);
            return __ret;
        }

        public bool FindSenderName(string Sendername)
        {
            bool __ret = __Internal.FindSenderName(__Instance, Sendername);
            return __ret;
        }

        public bool GetSenderNameInfo(int index, sbyte* sendername, int sendernameMaxSize, ref uint width, ref uint height, void** dxShareHandle)
        {
            fixed (uint* __width3 = &width)
            {
                uint* __arg3 = __width3;
                fixed (uint* __height4 = &height)
                {
                    uint* __arg4 = __height4;
                    bool __ret = __Internal.GetSenderNameInfo(__Instance, index, sendername, sendernameMaxSize, __arg3, __arg4, dxShareHandle);
                    return __ret;
                }
            }
        }

        public bool GetSenderInfo(string sendername, ref uint width, ref uint height, void** dxShareHandle, ref uint dwFormat)
        {
            fixed (uint* __width1 = &width)
            {
                uint* __arg1 = __width1;
                fixed (uint* __height2 = &height)
                {
                    uint* __arg2 = __height2;
                    fixed (uint* __dwFormat4 = &dwFormat)
                    {
                        uint* __arg4 = __dwFormat4;
                        bool __ret = __Internal.GetSenderInfo(__Instance, sendername, __arg1, __arg2, dxShareHandle, __arg4);
                        return __ret;
                    }
                }
            }
        }

        public bool SetSenderInfo(string sendername, uint width, uint height, IntPtr dxShareHandle, uint dwFormat)
        {
            bool __ret = __Internal.SetSenderInfo(__Instance, sendername, width, height, dxShareHandle, dwFormat);
            return __ret;
        }

        public bool GetSharedInfo(string SenderName, SharedTextureInfo info)
        {
            IntPtr __arg1 = info is null ? IntPtr.Zero : info.__Instance;
            bool __ret = __Internal.GetSharedInfo(__Instance, SenderName, __arg1);
            return __ret;
        }

        public bool SetSharedInfo(string SenderName, SharedTextureInfo info)
        {
            IntPtr __arg1 = info is null ? IntPtr.Zero : info.__Instance;
            bool __ret = __Internal.SetSharedInfo(__Instance, SenderName, __arg1);
            return __ret;
        }

        public bool SetActiveSender(string Sendername)
        {
            bool __ret = __Internal.SetActiveSender(__Instance, Sendername);
            return __ret;
        }

        public bool GetActiveSender(sbyte[] Sendername)
        {
            if (Sendername == null || Sendername.Length != 256)
            {
                throw new ArgumentOutOfRangeException("Sendername", "The dimensions of the provided array don't match the required size.");
            }

            bool __ret = __Internal.GetActiveSender(__Instance, Sendername);
            return __ret;
        }

        public bool GetActiveSenderInfo(SharedTextureInfo info)
        {
            IntPtr __arg0 = info is null ? IntPtr.Zero : info.__Instance;
            bool __ret = __Internal.GetActiveSenderInfo(__Instance, __arg0);
            return __ret;
        }

        public bool FindActiveSender(sbyte[] activename, ref uint width, ref uint height, void** hSharehandle, ref uint dwFormat)
        {
            if (activename == null || activename.Length != 256)
            {
                throw new ArgumentOutOfRangeException("activename", "The dimensions of the provided array don't match the required size.");
            }

            fixed (uint* __width1 = &width)
            {
                uint* __arg1 = __width1;
                fixed (uint* __height2 = &height)
                {
                    uint* __arg2 = __height2;
                    fixed (uint* __dwFormat4 = &dwFormat)
                    {
                        uint* __arg4 = __dwFormat4;
                        bool __ret = __Internal.FindActiveSender(__Instance, activename, __arg1, __arg2, hSharehandle, __arg4);
                        return __ret;
                    }
                }
            }
        }

        public bool CreateSender(string sendername, uint width, uint height, IntPtr hSharehandle, uint dwFormat)
        {
            bool __ret = __Internal.CreateSender(__Instance, sendername, width, height, hSharehandle, dwFormat);
            return __ret;
        }

        public bool UpdateSender(string sendername, uint width, uint height, IntPtr hSharehandle, uint dwFormat)
        {
            bool __ret = __Internal.UpdateSender(__Instance, sendername, width, height, hSharehandle, dwFormat);
            return __ret;
        }

        public bool CheckSender(string sendername, ref uint width, ref uint height, void** hSharehandle, ref uint dwFormat)
        {
            fixed (uint* __width1 = &width)
            {
                uint* __arg1 = __width1;
                fixed (uint* __height2 = &height)
                {
                    uint* __arg2 = __height2;
                    fixed (uint* __dwFormat4 = &dwFormat)
                    {
                        uint* __arg4 = __dwFormat4;
                        bool __ret = __Internal.CheckSender(__Instance, sendername, __arg1, __arg2, hSharehandle, __arg4);
                        return __ret;
                    }
                }
            }
        }

        public bool FindSender(sbyte* sendername, ref uint width, ref uint height, void** hSharehandle, ref uint dwFormat)
        {
            fixed (uint* __width1 = &width)
            {
                uint* __arg1 = __width1;
                fixed (uint* __height2 = &height)
                {
                    uint* __arg2 = __height2;
                    fixed (uint* __dwFormat4 = &dwFormat)
                    {
                        uint* __arg4 = __dwFormat4;
                        bool __ret = __Internal.FindSender(__Instance, sendername, __arg1, __arg2, hSharehandle, __arg4);
                        return __ret;
                    }
                }
            }
        }

        public bool SenderDebug(string Sendername, int size)
        {
            bool __ret = __Internal.SenderDebug(__Instance, Sendername, size);
            return __ret;
        }

        protected bool CreateSenderSet()
        {
            bool __ret = __Internal.CreateSenderSet(__Instance);
            return __ret;
        }

        protected bool SetActiveSenderName(string SenderName)
        {
            bool __ret = __Internal.SetActiveSenderName(__Instance, SenderName);
            return __ret;
        }

        protected bool GetActiveSenderName(sbyte[] SenderName)
        {
            if (SenderName == null || SenderName.Length != 256)
            {
                throw new ArgumentOutOfRangeException("SenderName", "The dimensions of the provided array don't match the required size.");
            }

            bool __ret = __Internal.GetActiveSenderName(__Instance, SenderName);
            return __ret;
        }

        protected void CleanSenderSet()
        {
            __Internal.CleanSenderSet(__Instance);
        }

        protected SpoutSharedMemory MSenderNames
        {
            get => SpoutSharedMemory.__CreateInstance(new IntPtr(&((SpoutSenderNames.__Internal*)__Instance)->m_senderNames));

            set
            {
                if (value is null)
                {
                    throw new ArgumentNullException("value", "Cannot be null because it is passed by value.");
                } ((SpoutSenderNames.__Internal*)__Instance)->m_senderNames = *(SpoutSharedMemory.__Internal*)value.__Instance;
            }
        }

        protected SpoutSharedMemory MActiveSender
        {
            get => SpoutSharedMemory.__CreateInstance(new IntPtr(&((SpoutSenderNames.__Internal*)__Instance)->m_activeSender));

            set
            {
                if (value is null)
                {
                    throw new ArgumentNullException("value", "Cannot be null because it is passed by value.");
                } ((SpoutSenderNames.__Internal*)__Instance)->m_activeSender = *(SpoutSharedMemory.__Internal*)value.__Instance;
            }
        }

        protected int MMaxSenders
        {
            get => ((SpoutSenderNames.__Internal*)__Instance)->m_MaxSenders;

            set => ((SpoutSenderNames.__Internal*)__Instance)->m_MaxSenders = value;
        }

        public int SenderCount
        {
            get
            {
                int __ret = __Internal.GetSenderCount(__Instance);
                return __ret;
            }
        }

        public int MaxSenders
        {
            get
            {
                int __ret = __Internal.GetMaxSenders(__Instance);
                return __ret;
            }

            set => __Internal.SetMaxSenders(__Instance, value);
        }
    }

    public unsafe partial class SpoutDirectX : IDisposable
    {
        [StructLayout(LayoutKind.Explicit, Size = 32)]
        public partial struct __Internal
        {
            [FieldOffset(0)]
            internal byte bUseAccessLocks;

            [FieldOffset(4)]
            internal int g_AdapterIndex;

            [FieldOffset(8)]
            internal IntPtr g_pAdapterDX11;

            [FieldOffset(16)]
            internal IntPtr g_pImmediateContext;

            [FieldOffset(24)]
            internal D3D_DRIVER_TYPE g_driverType;

            [FieldOffset(28)]
            internal D3D_FEATURE_LEVEL g_featureLevel;

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "??0spoutDirectX@@QEAA@XZ")]
            internal static extern IntPtr ctor(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "??0spoutDirectX@@QEAA@AEBV0@@Z")]
            internal static extern IntPtr cctor(IntPtr __instance, IntPtr _0);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "??1spoutDirectX@@QEAA@XZ")]
            internal static extern void dtor(IntPtr __instance, int delete);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GetAdapterName@spoutDirectX@@QEAA_NHPEADH@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool GetAdapterName(IntPtr __instance, int index, sbyte* adaptername, int maxchars);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?SetAdapter@spoutDirectX@@QEAA_NH@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool SetAdapter(IntPtr __instance, int index);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GetAdapterInfo@spoutDirectX@@QEAA_NPEAD0H@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool GetAdapterInfo(IntPtr __instance, sbyte* renderdescription, sbyte* displaydescription, int maxchars);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?FindNVIDIA@spoutDirectX@@QEAA_NAEAH@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool FindNVIDIA(IntPtr __instance, int* nAdapter);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?ReadDwordFromRegistry@spoutDirectX@@QEAA_NPEAKPEBD1@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool ReadDwordFromRegistry(IntPtr __instance, uint* pValue, [MarshalAs(UnmanagedType.LPUTF8Str)] string subkey, [MarshalAs(UnmanagedType.LPUTF8Str)] string valuename);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?WriteDwordToRegistry@spoutDirectX@@QEAA_NKPEBD0@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool WriteDwordToRegistry(IntPtr __instance, uint dwValue, [MarshalAs(UnmanagedType.LPUTF8Str)] string subkey, [MarshalAs(UnmanagedType.LPUTF8Str)] string valuename);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?CreateAccessMutex@spoutDirectX@@QEAA_NPEBDAEAPEAX@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool CreateAccessMutex(IntPtr __instance, [MarshalAs(UnmanagedType.LPUTF8Str)] string name, void** hAccessMutex);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?CloseAccessMutex@spoutDirectX@@QEAAXAEAPEAX@Z")]
            internal static extern void CloseAccessMutex(IntPtr __instance, void** hAccessMutex);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?CheckAccess@spoutDirectX@@QEAA_NPEAX@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool CheckAccess(IntPtr __instance, IntPtr hAccessMutex);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?AllowAccess@spoutDirectX@@QEAAXPEAX@Z")]
            internal static extern void AllowAccess(IntPtr __instance, IntPtr hAccessMutex);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GetNumAdapters@spoutDirectX@@QEAAHXZ")]
            internal static extern int GetNumAdapters(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GetAdapter@spoutDirectX@@QEAAHXZ")]
            internal static extern int GetAdapter(IntPtr __instance);
        }

        public IntPtr __Instance { get; protected set; }

        internal static readonly System.Collections.Concurrent.ConcurrentDictionary<IntPtr, SpoutDirectX> NativeToManagedMap = new();

        protected bool __ownsNativeInstance;

        internal static SpoutDirectX __CreateInstance(IntPtr native, bool skipVTables = false)
        {
            return new SpoutDirectX(native.ToPointer(), skipVTables);
        }

        internal static SpoutDirectX __CreateInstance(SpoutDirectX.__Internal native, bool skipVTables = false)
        {
            return new SpoutDirectX(native, skipVTables);
        }

        private static void* __CopyValue(SpoutDirectX.__Internal native)
        {
            IntPtr ret = Marshal.AllocHGlobal(sizeof(SpoutDirectX.__Internal));
            *(SpoutDirectX.__Internal*)ret = native;
            return ret.ToPointer();
        }

        private SpoutDirectX(SpoutDirectX.__Internal native, bool skipVTables = false)
            : this(__CopyValue(native), skipVTables)
        {
            __ownsNativeInstance = true;
            NativeToManagedMap[__Instance] = this;
        }

        protected SpoutDirectX(void* native, bool skipVTables = false)
        {
            if (native == null)
            {
                return;
            }

            __Instance = new IntPtr(native);
        }

        public SpoutDirectX()
        {
            __Instance = Marshal.AllocHGlobal(sizeof(SpoutDirectX.__Internal));
            __ownsNativeInstance = true;
            NativeToManagedMap[__Instance] = this;
            _ = __Internal.ctor(__Instance);
        }

        public SpoutDirectX(SpoutDirectX _0)
        {
            __Instance = Marshal.AllocHGlobal(sizeof(SpoutDirectX.__Internal));
            __ownsNativeInstance = true;
            NativeToManagedMap[__Instance] = this;
            *(SpoutDirectX.__Internal*)__Instance = *(SpoutDirectX.__Internal*)_0.__Instance;
        }

        ~SpoutDirectX()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (__Instance == IntPtr.Zero)
            {
                return;
            }

            _ = NativeToManagedMap.TryRemove(__Instance, out _);
            if (disposing)
            {
                __Internal.dtor(__Instance, 0);
            }

            if (__ownsNativeInstance)
            {
                Marshal.FreeHGlobal(__Instance);
            }

            __Instance = IntPtr.Zero;
        }

        public bool GetAdapterName(int index, sbyte* adaptername, int maxchars)
        {
            bool __ret = __Internal.GetAdapterName(__Instance, index, adaptername, maxchars);
            return __ret;
        }

        public bool SetAdapter(int index)
        {
            bool __ret = __Internal.SetAdapter(__Instance, index);
            return __ret;
        }

        public bool GetAdapterInfo(sbyte* renderdescription, sbyte* displaydescription, int maxchars)
        {
            bool __ret = __Internal.GetAdapterInfo(__Instance, renderdescription, displaydescription, maxchars);
            return __ret;
        }

        public bool FindNVIDIA(ref int nAdapter)
        {
            fixed (int* __nAdapter0 = &nAdapter)
            {
                int* __arg0 = __nAdapter0;
                bool __ret = __Internal.FindNVIDIA(__Instance, __arg0);
                return __ret;
            }
        }

        public bool ReadDwordFromRegistry(ref uint pValue, string subkey, string valuename)
        {
            fixed (uint* __pValue0 = &pValue)
            {
                uint* __arg0 = __pValue0;
                bool __ret = __Internal.ReadDwordFromRegistry(__Instance, __arg0, subkey, valuename);
                return __ret;
            }
        }

        public bool WriteDwordToRegistry(uint dwValue, string subkey, string valuename)
        {
            bool __ret = __Internal.WriteDwordToRegistry(__Instance, dwValue, subkey, valuename);
            return __ret;
        }

        public bool CreateAccessMutex(string name, void** hAccessMutex)
        {
            bool __ret = __Internal.CreateAccessMutex(__Instance, name, hAccessMutex);
            return __ret;
        }

        public void CloseAccessMutex(void** hAccessMutex)
        {
            __Internal.CloseAccessMutex(__Instance, hAccessMutex);
        }

        public bool CheckAccess(IntPtr hAccessMutex)
        {
            bool __ret = __Internal.CheckAccess(__Instance, hAccessMutex);
            return __ret;
        }

        public void AllowAccess(IntPtr hAccessMutex)
        {
            __Internal.AllowAccess(__Instance, hAccessMutex);
        }

        public bool BUseAccessLocks
        {
            get => ((SpoutDirectX.__Internal*)__Instance)->bUseAccessLocks != 0;

            set => ((SpoutDirectX.__Internal*)__Instance)->bUseAccessLocks = (byte)(value ? 1 : 0);
        }

        protected int GAdapterIndex
        {
            get => ((SpoutDirectX.__Internal*)__Instance)->g_AdapterIndex;

            set => ((SpoutDirectX.__Internal*)__Instance)->g_AdapterIndex = value;
        }

        public int NumAdapters
        {
            get
            {
                int __ret = __Internal.GetNumAdapters(__Instance);
                return __ret;
            }
        }

        public int Adapter
        {
            get
            {
                int __ret = __Internal.GetAdapter(__Instance);
                return __ret;
            }

            set => __Internal.SetAdapter(__Instance, value);
        }
    }

    public unsafe partial class SpoutCopy : IDisposable
    {
        [StructLayout(LayoutKind.Explicit, Size = 3)]
        public partial struct __Internal
        {
            [FieldOffset(0)]
            internal byte m_bSSE2;

            [FieldOffset(1)]
            internal byte m_bSSE3;

            [FieldOffset(2)]
            internal byte m_bSSSE3;

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "??0spoutCopy@@QEAA@XZ")]
            internal static extern IntPtr ctor(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "??0spoutCopy@@QEAA@AEBV0@@Z")]
            internal static extern IntPtr cctor(IntPtr __instance, IntPtr _0);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "??1spoutCopy@@QEAA@XZ")]
            internal static extern void dtor(IntPtr __instance, int delete);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?CopyPixels@spoutCopy@@QEAAXPEBEPEAEIII_N@Z")]
            internal static extern void CopyPixels(IntPtr __instance, byte* src, byte* dst, uint width, uint height, uint glFormat, bool bInvert);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?FlipBuffer@spoutCopy@@QEAA_NPEBEPEAEIII@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool FlipBuffer(IntPtr __instance, byte* src, byte* dst, uint width, uint height, uint glFormat);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?memcpy_sse2@spoutCopy@@QEAAXPEAX0_K@Z")]
            internal static extern void MemcpySse2(IntPtr __instance, IntPtr dst, IntPtr src, ulong size);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?rgba2bgra@spoutCopy@@QEAAXPEAX0II_N@Z")]
            internal static extern void Rgba2bgra(IntPtr __instance, IntPtr rgba_source, IntPtr bgra_dest, uint width, uint height, bool bInvert);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?bgra2rgba@spoutCopy@@QEAAXPEAX0II_N@Z")]
            internal static extern void Bgra2rgba(IntPtr __instance, IntPtr bgra_source, IntPtr rgba_dest, uint width, uint height, bool bInvert);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?rgba_bgra@spoutCopy@@QEAAXPEAX0II_N@Z")]
            internal static extern void RgbaBgra(IntPtr __instance, IntPtr rgba_source, IntPtr bgra_dest, uint width, uint height, bool bInvert);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?rgba_bgra_sse2@spoutCopy@@QEAAXPEAX0II_N@Z")]
            internal static extern void RgbaBgraSse2(IntPtr __instance, IntPtr rgba_source, IntPtr rgba_dest, uint width, uint height, bool bInvert);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?rgba_bgra_ssse3@spoutCopy@@QEAAXPEAX0II_N@Z")]
            internal static extern void RgbaBgraSsse3(IntPtr __instance, IntPtr rgba_source, IntPtr rgba_dest, uint width, uint height, bool bInvert);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?rgb2rgba@spoutCopy@@QEAAXPEAX0II_N@Z")]
            internal static extern void Rgb2rgba(IntPtr __instance, IntPtr rgb_source, IntPtr rgba_dest, uint width, uint height, bool bInvert);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?bgr2rgba@spoutCopy@@QEAAXPEAX0II_N@Z")]
            internal static extern void Bgr2rgba(IntPtr __instance, IntPtr bgr_source, IntPtr rgba_dest, uint width, uint height, bool bInvert);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?rgb2bgra@spoutCopy@@QEAAXPEAX0II_N@Z")]
            internal static extern void Rgb2bgra(IntPtr __instance, IntPtr rgb_source, IntPtr bgra_dest, uint width, uint height, bool bInvert);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?bgr2bgra@spoutCopy@@QEAAXPEAX0II_N@Z")]
            internal static extern void Bgr2bgra(IntPtr __instance, IntPtr bgr_source, IntPtr bgra_dest, uint width, uint height, bool bInvert);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?rgba2rgb@spoutCopy@@QEAAXPEAX0II_N@Z")]
            internal static extern void Rgba2rgb(IntPtr __instance, IntPtr rgba_source, IntPtr rgb_dest, uint width, uint height, bool bInvert);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?rgba2bgr@spoutCopy@@QEAAXPEAX0II_N@Z")]
            internal static extern void Rgba2bgr(IntPtr __instance, IntPtr rgba_source, IntPtr bgr_dest, uint width, uint height, bool bInvert);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?bgra2rgb@spoutCopy@@QEAAXPEAX0II_N@Z")]
            internal static extern void Bgra2rgb(IntPtr __instance, IntPtr bgra_source, IntPtr rgb_dest, uint width, uint height, bool bInvert);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?bgra2bgr@spoutCopy@@QEAAXPEAX0II_N@Z")]
            internal static extern void Bgra2bgr(IntPtr __instance, IntPtr bgra_source, IntPtr bgr_dest, uint width, uint height, bool bInvert);
        }

        public IntPtr __Instance { get; protected set; }

        internal static readonly System.Collections.Concurrent.ConcurrentDictionary<IntPtr, SpoutCopy> NativeToManagedMap = new();

        protected bool __ownsNativeInstance;

        internal static SpoutCopy __CreateInstance(IntPtr native, bool skipVTables = false)
        {
            return new SpoutCopy(native.ToPointer(), skipVTables);
        }

        internal static SpoutCopy __CreateInstance(SpoutCopy.__Internal native, bool skipVTables = false)
        {
            return new SpoutCopy(native, skipVTables);
        }

        private static void* __CopyValue(SpoutCopy.__Internal native)
        {
            IntPtr ret = Marshal.AllocHGlobal(sizeof(SpoutCopy.__Internal));
            *(SpoutCopy.__Internal*)ret = native;
            return ret.ToPointer();
        }

        private SpoutCopy(SpoutCopy.__Internal native, bool skipVTables = false)
            : this(__CopyValue(native), skipVTables)
        {
            __ownsNativeInstance = true;
            NativeToManagedMap[__Instance] = this;
        }

        protected SpoutCopy(void* native, bool skipVTables = false)
        {
            if (native == null)
            {
                return;
            }

            __Instance = new IntPtr(native);
        }

        public SpoutCopy()
        {
            __Instance = Marshal.AllocHGlobal(sizeof(SpoutCopy.__Internal));
            __ownsNativeInstance = true;
            NativeToManagedMap[__Instance] = this;
            _ = __Internal.ctor(__Instance);
        }

        public SpoutCopy(SpoutCopy _0)
        {
            __Instance = Marshal.AllocHGlobal(sizeof(SpoutCopy.__Internal));
            __ownsNativeInstance = true;
            NativeToManagedMap[__Instance] = this;
            *(SpoutCopy.__Internal*)__Instance = *(SpoutCopy.__Internal*)_0.__Instance;
        }

        ~SpoutCopy()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (__Instance == IntPtr.Zero)
            {
                return;
            }

            _ = NativeToManagedMap.TryRemove(__Instance, out _);
            if (disposing)
            {
                __Internal.dtor(__Instance, 0);
            }

            if (__ownsNativeInstance)
            {
                Marshal.FreeHGlobal(__Instance);
            }

            __Instance = IntPtr.Zero;
        }

        public void CopyPixels(byte* src, byte* dst, uint width, uint height, uint glFormat, bool bInvert)
        {
            __Internal.CopyPixels(__Instance, src, dst, width, height, glFormat, bInvert);
        }

        public bool FlipBuffer(byte* src, byte* dst, uint width, uint height, uint glFormat)
        {
            bool __ret = __Internal.FlipBuffer(__Instance, src, dst, width, height, glFormat);
            return __ret;
        }

        public void MemcpySse2(IntPtr dst, IntPtr src, ulong size)
        {
            __Internal.MemcpySse2(__Instance, dst, src, size);
        }

        public void Rgba2bgra(IntPtr rgba_source, IntPtr bgra_dest, uint width, uint height, bool bInvert)
        {
            __Internal.Rgba2bgra(__Instance, rgba_source, bgra_dest, width, height, bInvert);
        }

        public void Bgra2rgba(IntPtr bgra_source, IntPtr rgba_dest, uint width, uint height, bool bInvert)
        {
            __Internal.Bgra2rgba(__Instance, bgra_source, rgba_dest, width, height, bInvert);
        }

        public void RgbaBgra(IntPtr rgba_source, IntPtr bgra_dest, uint width, uint height, bool bInvert)
        {
            __Internal.RgbaBgra(__Instance, rgba_source, bgra_dest, width, height, bInvert);
        }

        public void RgbaBgraSse2(IntPtr rgba_source, IntPtr rgba_dest, uint width, uint height, bool bInvert)
        {
            __Internal.RgbaBgraSse2(__Instance, rgba_source, rgba_dest, width, height, bInvert);
        }

        public void RgbaBgraSsse3(IntPtr rgba_source, IntPtr rgba_dest, uint width, uint height, bool bInvert)
        {
            __Internal.RgbaBgraSsse3(__Instance, rgba_source, rgba_dest, width, height, bInvert);
        }

        public void Rgb2rgba(IntPtr rgb_source, IntPtr rgba_dest, uint width, uint height, bool bInvert)
        {
            __Internal.Rgb2rgba(__Instance, rgb_source, rgba_dest, width, height, bInvert);
        }

        public void Bgr2rgba(IntPtr bgr_source, IntPtr rgba_dest, uint width, uint height, bool bInvert)
        {
            __Internal.Bgr2rgba(__Instance, bgr_source, rgba_dest, width, height, bInvert);
        }

        public void Rgb2bgra(IntPtr rgb_source, IntPtr bgra_dest, uint width, uint height, bool bInvert)
        {
            __Internal.Rgb2bgra(__Instance, rgb_source, bgra_dest, width, height, bInvert);
        }

        public void Bgr2bgra(IntPtr bgr_source, IntPtr bgra_dest, uint width, uint height, bool bInvert)
        {
            __Internal.Bgr2bgra(__Instance, bgr_source, bgra_dest, width, height, bInvert);
        }

        public void Rgba2rgb(IntPtr rgba_source, IntPtr rgb_dest, uint width, uint height, bool bInvert)
        {
            __Internal.Rgba2rgb(__Instance, rgba_source, rgb_dest, width, height, bInvert);
        }

        public void Rgba2bgr(IntPtr rgba_source, IntPtr bgr_dest, uint width, uint height, bool bInvert)
        {
            __Internal.Rgba2bgr(__Instance, rgba_source, bgr_dest, width, height, bInvert);
        }

        public void Bgra2rgb(IntPtr bgra_source, IntPtr rgb_dest, uint width, uint height, bool bInvert)
        {
            __Internal.Bgra2rgb(__Instance, bgra_source, rgb_dest, width, height, bInvert);
        }

        public void Bgra2bgr(IntPtr bgra_source, IntPtr bgr_dest, uint width, uint height, bool bInvert)
        {
            __Internal.Bgra2bgr(__Instance, bgra_source, bgr_dest, width, height, bInvert);
        }
    }

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate IntPtr PFNWGLDXOPENDEVICENVPROC(IntPtr dxDevice);

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate int PFNWGLDXCLOSEDEVICENVPROC(IntPtr hDevice);

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate IntPtr PFNWGLDXREGISTEROBJECTNVPROC(IntPtr hDevice, IntPtr dxObject, uint name, uint type, uint access);

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate int PFNWGLDXUNREGISTEROBJECTNVPROC(IntPtr hDevice, IntPtr hObject);

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate int PFNWGLDXSETRESOURCESHAREHANDLENVPROC(IntPtr dxResource, IntPtr shareHandle);

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate int PFNWGLDXLOCKOBJECTSNVPROC(IntPtr hDevice, int count, void** hObjects);

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate int PFNWGLDXUNLOCKOBJECTSNVPROC(IntPtr hDevice, int count, void** hObjects);

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void GlBindFramebufferEXTPROC(uint target, uint framebuffer);

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void GlBindRenderbufferEXTPROC(uint target, uint renderbuffer);

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate uint GlCheckFramebufferStatusEXTPROC(uint target);

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void GlDeleteFramebuffersEXTPROC(int n, uint* framebuffers);

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void GlDeleteRenderBuffersEXTPROC(int n, uint* renderbuffers);

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void GlFramebufferRenderbufferEXTPROC(uint target, uint attachment, uint renderbuffertarget, uint renderbuffer);

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void GlFramebufferTexture1DEXTPROC(uint target, uint attachment, uint textarget, uint texture, int level);

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void GlFramebufferTexture2DEXTPROC(uint target, uint attachment, uint textarget, uint texture, int level);

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void GlFramebufferTexture3DEXTPROC(uint target, uint attachment, uint textarget, uint texture, int level, int zoffset);

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void GlGenFramebuffersEXTPROC(int n, uint* framebuffers);

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void GlGenRenderbuffersEXTPROC(int n, uint* renderbuffers);

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void GlGenerateMipmapEXTPROC(uint target);

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void GlGetFramebufferAttachmentParameterivEXTPROC(uint target, uint attachment, uint pname, int* @params);

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void GlGetRenderbufferParameterivEXTPROC(uint target, uint pname, int* @params);

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate byte GlIsFramebufferEXTPROC(uint framebuffer);

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate byte GlIsRenderbufferEXTPROC(uint renderbuffer);

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void GlRenderbufferStorageEXTPROC(uint target, uint internalformat, int width, int height);

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void GlBlitFramebufferEXTPROC(int srcX0, int srcY0, int srcX1, int srcY1, int dstX0, int dstY0, int dstX1, int dstY1, uint mask, uint filter);

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate int PFNWGLSWAPINTERVALEXTPROC(int interval);

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate int PFNWGLGETSWAPINTERVALEXTPROC();

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void GlGenBuffersPROC(int n, uint* buffers);

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void GlDeleteBuffersPROC(int n, uint* buffers);

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void GlBindBufferPROC(uint target, uint buffer);

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void GlBufferDataPROC(uint target, long size, IntPtr data, uint usage);

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate IntPtr GlMapBufferPROC(uint target, uint access);

    [SuppressUnmanagedCodeSecurity, UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void GlUnmapBufferPROC(uint target);

    public unsafe partial class SpoutGLextensions
    {
        public partial struct __Internal
        {
            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?InitializeGlew@@YA_NXZ")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool InitializeGlew();

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?loadGLextensions@@YAIXZ")]
            internal static extern uint LoadGLextensions();

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?loadInteropExtensions@@YA_NXZ")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool LoadInteropExtensions();

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?loadFBOextensions@@YA_NXZ")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool LoadFBOextensions();

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?loadBLITextension@@YA_NXZ")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool LoadBLITextension();

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?loadSwapExtensions@@YA_NXZ")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool LoadSwapExtensions();

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?loadPBOextensions@@YA_NXZ")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool LoadPBOextensions();
        }

        public static bool InitializeGlew()
        {
            bool __ret = __Internal.InitializeGlew();
            return __ret;
        }

        public static uint LoadGLextensions()
        {
            uint __ret = __Internal.LoadGLextensions();
            return __ret;
        }

        public static bool LoadInteropExtensions()
        {
            bool __ret = __Internal.LoadInteropExtensions();
            return __ret;
        }

        public static bool LoadFBOextensions()
        {
            bool __ret = __Internal.LoadFBOextensions();
            return __ret;
        }

        public static bool LoadBLITextension()
        {
            bool __ret = __Internal.LoadBLITextension();
            return __ret;
        }

        public static bool LoadSwapExtensions()
        {
            bool __ret = __Internal.LoadSwapExtensions();
            return __ret;
        }

        public static bool LoadPBOextensions()
        {
            bool __ret = __Internal.LoadPBOextensions();
            return __ret;
        }

        public static PFNWGLDXOPENDEVICENVPROC WglDXOpenDeviceNV
        {
            get
            {
                IntPtr* __ptr = (IntPtr*)SymbolResolver.Resolve("Libraries/Spout.dll", "?wglDXOpenDeviceNV@@3P6APEAXPEAX@ZEA");
                IntPtr __ptr0 = *__ptr;
                return __ptr0 == IntPtr.Zero ? null : (PFNWGLDXOPENDEVICENVPROC)Marshal.GetDelegateForFunctionPointer(__ptr0, typeof(PFNWGLDXOPENDEVICENVPROC));
            }

            set
            {
                IntPtr* __ptr = (IntPtr*)SymbolResolver.Resolve("Libraries/Spout.dll", "?wglDXOpenDeviceNV@@3P6APEAXPEAX@ZEA");
                *__ptr = value == null ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(value);
            }
        }

        public static PFNWGLDXCLOSEDEVICENVPROC WglDXCloseDeviceNV
        {
            get
            {
                IntPtr* __ptr = (IntPtr*)SymbolResolver.Resolve("Libraries/Spout.dll", "?wglDXCloseDeviceNV@@3P6AHPEAX@ZEA");
                IntPtr __ptr0 = *__ptr;
                return __ptr0 == IntPtr.Zero ? null : (PFNWGLDXCLOSEDEVICENVPROC)Marshal.GetDelegateForFunctionPointer(__ptr0, typeof(PFNWGLDXCLOSEDEVICENVPROC));
            }

            set
            {
                IntPtr* __ptr = (IntPtr*)SymbolResolver.Resolve("Libraries/Spout.dll", "?wglDXCloseDeviceNV@@3P6AHPEAX@ZEA");
                *__ptr = value == null ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(value);
            }
        }

        public static PFNWGLDXREGISTEROBJECTNVPROC WglDXRegisterObjectNV
        {
            get
            {
                IntPtr* __ptr = (IntPtr*)SymbolResolver.Resolve("Libraries/Spout.dll", "?wglDXRegisterObjectNV@@3P6APEAXPEAX0III@ZEA");
                IntPtr __ptr0 = *__ptr;
                return __ptr0 == IntPtr.Zero ? null : (PFNWGLDXREGISTEROBJECTNVPROC)Marshal.GetDelegateForFunctionPointer(__ptr0, typeof(PFNWGLDXREGISTEROBJECTNVPROC));
            }

            set
            {
                IntPtr* __ptr = (IntPtr*)SymbolResolver.Resolve("Libraries/Spout.dll", "?wglDXRegisterObjectNV@@3P6APEAXPEAX0III@ZEA");
                *__ptr = value == null ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(value);
            }
        }

        public static PFNWGLDXUNREGISTEROBJECTNVPROC WglDXUnregisterObjectNV
        {
            get
            {
                IntPtr* __ptr = (IntPtr*)SymbolResolver.Resolve("Libraries/Spout.dll", "?wglDXUnregisterObjectNV@@3P6AHPEAX0@ZEA");
                IntPtr __ptr0 = *__ptr;
                return __ptr0 == IntPtr.Zero ? null : (PFNWGLDXUNREGISTEROBJECTNVPROC)Marshal.GetDelegateForFunctionPointer(__ptr0, typeof(PFNWGLDXUNREGISTEROBJECTNVPROC));
            }

            set
            {
                IntPtr* __ptr = (IntPtr*)SymbolResolver.Resolve("Libraries/Spout.dll", "?wglDXUnregisterObjectNV@@3P6AHPEAX0@ZEA");
                *__ptr = value == null ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(value);
            }
        }

        public static PFNWGLDXSETRESOURCESHAREHANDLENVPROC WglDXSetResourceShareHandleNV
        {
            get
            {
                IntPtr* __ptr = (IntPtr*)SymbolResolver.Resolve("Libraries/Spout.dll", "?wglDXSetResourceShareHandleNV@@3P6AHPEAX0@ZEA");
                IntPtr __ptr0 = *__ptr;
                return __ptr0 == IntPtr.Zero ? null : (PFNWGLDXSETRESOURCESHAREHANDLENVPROC)Marshal.GetDelegateForFunctionPointer(__ptr0, typeof(PFNWGLDXSETRESOURCESHAREHANDLENVPROC));
            }

            set
            {
                IntPtr* __ptr = (IntPtr*)SymbolResolver.Resolve("Libraries/Spout.dll", "?wglDXSetResourceShareHandleNV@@3P6AHPEAX0@ZEA");
                *__ptr = value == null ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(value);
            }
        }

        public static PFNWGLDXLOCKOBJECTSNVPROC WglDXLockObjectsNV
        {
            get
            {
                IntPtr* __ptr = (IntPtr*)SymbolResolver.Resolve("Libraries/Spout.dll", "?wglDXLockObjectsNV@@3P6AHPEAXHPEAPEAX@ZEA");
                IntPtr __ptr0 = *__ptr;
                return __ptr0 == IntPtr.Zero ? null : (PFNWGLDXLOCKOBJECTSNVPROC)Marshal.GetDelegateForFunctionPointer(__ptr0, typeof(PFNWGLDXLOCKOBJECTSNVPROC));
            }

            set
            {
                IntPtr* __ptr = (IntPtr*)SymbolResolver.Resolve("Libraries/Spout.dll", "?wglDXLockObjectsNV@@3P6AHPEAXHPEAPEAX@ZEA");
                *__ptr = value == null ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(value);
            }
        }

        public static PFNWGLDXUNLOCKOBJECTSNVPROC WglDXUnlockObjectsNV
        {
            get
            {
                IntPtr* __ptr = (IntPtr*)SymbolResolver.Resolve("Libraries/Spout.dll", "?wglDXUnlockObjectsNV@@3P6AHPEAXHPEAPEAX@ZEA");
                IntPtr __ptr0 = *__ptr;
                return __ptr0 == IntPtr.Zero ? null : (PFNWGLDXUNLOCKOBJECTSNVPROC)Marshal.GetDelegateForFunctionPointer(__ptr0, typeof(PFNWGLDXUNLOCKOBJECTSNVPROC));
            }

            set
            {
                IntPtr* __ptr = (IntPtr*)SymbolResolver.Resolve("Libraries/Spout.dll", "?wglDXUnlockObjectsNV@@3P6AHPEAXHPEAPEAX@ZEA");
                *__ptr = value == null ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(value);
            }
        }

        public static GlBindFramebufferEXTPROC GlBindFramebufferEXT
        {
            get
            {
                IntPtr* __ptr = (IntPtr*)SymbolResolver.Resolve("Libraries/Spout.dll", "?glBindFramebufferEXT@@3P6AXII@ZEA");
                IntPtr __ptr0 = *__ptr;
                return __ptr0 == IntPtr.Zero ? null : (GlBindFramebufferEXTPROC)Marshal.GetDelegateForFunctionPointer(__ptr0, typeof(GlBindFramebufferEXTPROC));
            }

            set
            {
                IntPtr* __ptr = (IntPtr*)SymbolResolver.Resolve("Libraries/Spout.dll", "?glBindFramebufferEXT@@3P6AXII@ZEA");
                *__ptr = value == null ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(value);
            }
        }

        public static GlBindRenderbufferEXTPROC GlBindRenderbufferEXT
        {
            get
            {
                IntPtr* __ptr = (IntPtr*)SymbolResolver.Resolve("Libraries/Spout.dll", "?glBindRenderbufferEXT@@3P6AXII@ZEA");
                IntPtr __ptr0 = *__ptr;
                return __ptr0 == IntPtr.Zero ? null : (GlBindRenderbufferEXTPROC)Marshal.GetDelegateForFunctionPointer(__ptr0, typeof(GlBindRenderbufferEXTPROC));
            }

            set
            {
                IntPtr* __ptr = (IntPtr*)SymbolResolver.Resolve("Libraries/Spout.dll", "?glBindRenderbufferEXT@@3P6AXII@ZEA");
                *__ptr = value == null ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(value);
            }
        }

        public static GlCheckFramebufferStatusEXTPROC GlCheckFramebufferStatusEXT
        {
            get
            {
                IntPtr* __ptr = (IntPtr*)SymbolResolver.Resolve("Libraries/Spout.dll", "?glCheckFramebufferStatusEXT@@3P6AII@ZEA");
                IntPtr __ptr0 = *__ptr;
                return __ptr0 == IntPtr.Zero ? null : (GlCheckFramebufferStatusEXTPROC)Marshal.GetDelegateForFunctionPointer(__ptr0, typeof(GlCheckFramebufferStatusEXTPROC));
            }

            set
            {
                IntPtr* __ptr = (IntPtr*)SymbolResolver.Resolve("Libraries/Spout.dll", "?glCheckFramebufferStatusEXT@@3P6AII@ZEA");
                *__ptr = value == null ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(value);
            }
        }

        public static GlDeleteFramebuffersEXTPROC GlDeleteFramebuffersEXT
        {
            get
            {
                IntPtr* __ptr = (IntPtr*)SymbolResolver.Resolve("Libraries/Spout.dll", "?glDeleteFramebuffersEXT@@3P6AXHPEBI@ZEA");
                IntPtr __ptr0 = *__ptr;
                return __ptr0 == IntPtr.Zero ? null : (GlDeleteFramebuffersEXTPROC)Marshal.GetDelegateForFunctionPointer(__ptr0, typeof(GlDeleteFramebuffersEXTPROC));
            }

            set
            {
                IntPtr* __ptr = (IntPtr*)SymbolResolver.Resolve("Libraries/Spout.dll", "?glDeleteFramebuffersEXT@@3P6AXHPEBI@ZEA");
                *__ptr = value == null ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(value);
            }
        }

        public static GlDeleteRenderBuffersEXTPROC GlDeleteRenderBuffersEXT
        {
            get
            {
                IntPtr* __ptr = (IntPtr*)SymbolResolver.Resolve("Libraries/Spout.dll", "?glDeleteRenderBuffersEXT@@3P6AXHPEBI@ZEA");
                IntPtr __ptr0 = *__ptr;
                return __ptr0 == IntPtr.Zero ? null : (GlDeleteRenderBuffersEXTPROC)Marshal.GetDelegateForFunctionPointer(__ptr0, typeof(GlDeleteRenderBuffersEXTPROC));
            }

            set
            {
                IntPtr* __ptr = (IntPtr*)SymbolResolver.Resolve("Libraries/Spout.dll", "?glDeleteRenderBuffersEXT@@3P6AXHPEBI@ZEA");
                *__ptr = value == null ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(value);
            }
        }

        public static GlFramebufferRenderbufferEXTPROC GlFramebufferRenderbufferEXT
        {
            get
            {
                IntPtr* __ptr = (IntPtr*)SymbolResolver.Resolve("Libraries/Spout.dll", "?glFramebufferRenderbufferEXT@@3P6AXIIII@ZEA");
                IntPtr __ptr0 = *__ptr;
                return __ptr0 == IntPtr.Zero ? null : (GlFramebufferRenderbufferEXTPROC)Marshal.GetDelegateForFunctionPointer(__ptr0, typeof(GlFramebufferRenderbufferEXTPROC));
            }

            set
            {
                IntPtr* __ptr = (IntPtr*)SymbolResolver.Resolve("Libraries/Spout.dll", "?glFramebufferRenderbufferEXT@@3P6AXIIII@ZEA");
                *__ptr = value == null ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(value);
            }
        }

        public static GlFramebufferTexture1DEXTPROC GlFramebufferTexture1DEXT
        {
            get
            {
                IntPtr* __ptr = (IntPtr*)SymbolResolver.Resolve("Libraries/Spout.dll", "?glFramebufferTexture1DEXT@@3P6AXIIIIH@ZEA");
                IntPtr __ptr0 = *__ptr;
                return __ptr0 == IntPtr.Zero ? null : (GlFramebufferTexture1DEXTPROC)Marshal.GetDelegateForFunctionPointer(__ptr0, typeof(GlFramebufferTexture1DEXTPROC));
            }

            set
            {
                IntPtr* __ptr = (IntPtr*)SymbolResolver.Resolve("Libraries/Spout.dll", "?glFramebufferTexture1DEXT@@3P6AXIIIIH@ZEA");
                *__ptr = value == null ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(value);
            }
        }

        public static GlFramebufferTexture2DEXTPROC GlFramebufferTexture2DEXT
        {
            get
            {
                IntPtr* __ptr = (IntPtr*)SymbolResolver.Resolve("Libraries/Spout.dll", "?glFramebufferTexture2DEXT@@3P6AXIIIIH@ZEA");
                IntPtr __ptr0 = *__ptr;
                return __ptr0 == IntPtr.Zero ? null : (GlFramebufferTexture2DEXTPROC)Marshal.GetDelegateForFunctionPointer(__ptr0, typeof(GlFramebufferTexture2DEXTPROC));
            }

            set
            {
                IntPtr* __ptr = (IntPtr*)SymbolResolver.Resolve("Libraries/Spout.dll", "?glFramebufferTexture2DEXT@@3P6AXIIIIH@ZEA");
                *__ptr = value == null ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(value);
            }
        }

        public static GlFramebufferTexture3DEXTPROC GlFramebufferTexture3DEXT
        {
            get
            {
                IntPtr* __ptr = (IntPtr*)SymbolResolver.Resolve("Libraries/Spout.dll", "?glFramebufferTexture3DEXT@@3P6AXIIIIHH@ZEA");
                IntPtr __ptr0 = *__ptr;
                return __ptr0 == IntPtr.Zero ? null : (GlFramebufferTexture3DEXTPROC)Marshal.GetDelegateForFunctionPointer(__ptr0, typeof(GlFramebufferTexture3DEXTPROC));
            }

            set
            {
                IntPtr* __ptr = (IntPtr*)SymbolResolver.Resolve("Libraries/Spout.dll", "?glFramebufferTexture3DEXT@@3P6AXIIIIHH@ZEA");
                *__ptr = value == null ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(value);
            }
        }

        public static GlGenFramebuffersEXTPROC GlGenFramebuffersEXT
        {
            get
            {
                IntPtr* __ptr = (IntPtr*)SymbolResolver.Resolve("Libraries/Spout.dll", "?glGenFramebuffersEXT@@3P6AXHPEAI@ZEA");
                IntPtr __ptr0 = *__ptr;
                return __ptr0 == IntPtr.Zero ? null : (GlGenFramebuffersEXTPROC)Marshal.GetDelegateForFunctionPointer(__ptr0, typeof(GlGenFramebuffersEXTPROC));
            }

            set
            {
                IntPtr* __ptr = (IntPtr*)SymbolResolver.Resolve("Libraries/Spout.dll", "?glGenFramebuffersEXT@@3P6AXHPEAI@ZEA");
                *__ptr = value == null ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(value);
            }
        }

        public static GlGenRenderbuffersEXTPROC GlGenRenderbuffersEXT
        {
            get
            {
                IntPtr* __ptr = (IntPtr*)SymbolResolver.Resolve("Libraries/Spout.dll", "?glGenRenderbuffersEXT@@3P6AXHPEAI@ZEA");
                IntPtr __ptr0 = *__ptr;
                return __ptr0 == IntPtr.Zero ? null : (GlGenRenderbuffersEXTPROC)Marshal.GetDelegateForFunctionPointer(__ptr0, typeof(GlGenRenderbuffersEXTPROC));
            }

            set
            {
                IntPtr* __ptr = (IntPtr*)SymbolResolver.Resolve("Libraries/Spout.dll", "?glGenRenderbuffersEXT@@3P6AXHPEAI@ZEA");
                *__ptr = value == null ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(value);
            }
        }

        public static GlGenerateMipmapEXTPROC GlGenerateMipmapEXT
        {
            get
            {
                IntPtr* __ptr = (IntPtr*)SymbolResolver.Resolve("Libraries/Spout.dll", "?glGenerateMipmapEXT@@3P6AXI@ZEA");
                IntPtr __ptr0 = *__ptr;
                return __ptr0 == IntPtr.Zero ? null : (GlGenerateMipmapEXTPROC)Marshal.GetDelegateForFunctionPointer(__ptr0, typeof(GlGenerateMipmapEXTPROC));
            }

            set
            {
                IntPtr* __ptr = (IntPtr*)SymbolResolver.Resolve("Libraries/Spout.dll", "?glGenerateMipmapEXT@@3P6AXI@ZEA");
                *__ptr = value == null ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(value);
            }
        }

        public static GlGetFramebufferAttachmentParameterivEXTPROC GlGetFramebufferAttachmentParameterivEXT
        {
            get
            {
                IntPtr* __ptr = (IntPtr*)SymbolResolver.Resolve("Libraries/Spout.dll", "?glGetFramebufferAttachmentParameterivEXT@@3P6AXIIIPEAH@ZEA");
                IntPtr __ptr0 = *__ptr;
                return __ptr0 == IntPtr.Zero ? null : (GlGetFramebufferAttachmentParameterivEXTPROC)Marshal.GetDelegateForFunctionPointer(__ptr0, typeof(GlGetFramebufferAttachmentParameterivEXTPROC));
            }

            set
            {
                IntPtr* __ptr = (IntPtr*)SymbolResolver.Resolve("Libraries/Spout.dll", "?glGetFramebufferAttachmentParameterivEXT@@3P6AXIIIPEAH@ZEA");
                *__ptr = value == null ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(value);
            }
        }

        public static GlGetRenderbufferParameterivEXTPROC GlGetRenderbufferParameterivEXT
        {
            get
            {
                IntPtr* __ptr = (IntPtr*)SymbolResolver.Resolve("Libraries/Spout.dll", "?glGetRenderbufferParameterivEXT@@3P6AXIIPEAH@ZEA");
                IntPtr __ptr0 = *__ptr;
                return __ptr0 == IntPtr.Zero ? null : (GlGetRenderbufferParameterivEXTPROC)Marshal.GetDelegateForFunctionPointer(__ptr0, typeof(GlGetRenderbufferParameterivEXTPROC));
            }

            set
            {
                IntPtr* __ptr = (IntPtr*)SymbolResolver.Resolve("Libraries/Spout.dll", "?glGetRenderbufferParameterivEXT@@3P6AXIIPEAH@ZEA");
                *__ptr = value == null ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(value);
            }
        }

        public static GlIsFramebufferEXTPROC GlIsFramebufferEXT
        {
            get
            {
                IntPtr* __ptr = (IntPtr*)SymbolResolver.Resolve("Libraries/Spout.dll", "?glIsFramebufferEXT@@3P6AEI@ZEA");
                IntPtr __ptr0 = *__ptr;
                return __ptr0 == IntPtr.Zero ? null : (GlIsFramebufferEXTPROC)Marshal.GetDelegateForFunctionPointer(__ptr0, typeof(GlIsFramebufferEXTPROC));
            }

            set
            {
                IntPtr* __ptr = (IntPtr*)SymbolResolver.Resolve("Libraries/Spout.dll", "?glIsFramebufferEXT@@3P6AEI@ZEA");
                *__ptr = value == null ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(value);
            }
        }

        public static GlIsRenderbufferEXTPROC GlIsRenderbufferEXT
        {
            get
            {
                IntPtr* __ptr = (IntPtr*)SymbolResolver.Resolve("Libraries/Spout.dll", "?glIsRenderbufferEXT@@3P6AEI@ZEA");
                IntPtr __ptr0 = *__ptr;
                return __ptr0 == IntPtr.Zero ? null : (GlIsRenderbufferEXTPROC)Marshal.GetDelegateForFunctionPointer(__ptr0, typeof(GlIsRenderbufferEXTPROC));
            }

            set
            {
                IntPtr* __ptr = (IntPtr*)SymbolResolver.Resolve("Libraries/Spout.dll", "?glIsRenderbufferEXT@@3P6AEI@ZEA");
                *__ptr = value == null ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(value);
            }
        }

        public static GlRenderbufferStorageEXTPROC GlRenderbufferStorageEXT
        {
            get
            {
                IntPtr* __ptr = (IntPtr*)SymbolResolver.Resolve("Libraries/Spout.dll", "?glRenderbufferStorageEXT@@3P6AXIIHH@ZEA");
                IntPtr __ptr0 = *__ptr;
                return __ptr0 == IntPtr.Zero ? null : (GlRenderbufferStorageEXTPROC)Marshal.GetDelegateForFunctionPointer(__ptr0, typeof(GlRenderbufferStorageEXTPROC));
            }

            set
            {
                IntPtr* __ptr = (IntPtr*)SymbolResolver.Resolve("Libraries/Spout.dll", "?glRenderbufferStorageEXT@@3P6AXIIHH@ZEA");
                *__ptr = value == null ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(value);
            }
        }

        public static GlBlitFramebufferEXTPROC GlBlitFramebufferEXT
        {
            get
            {
                IntPtr* __ptr = (IntPtr*)SymbolResolver.Resolve("Libraries/Spout.dll", "?glBlitFramebufferEXT@@3P6AXHHHHHHHHII@ZEA");
                IntPtr __ptr0 = *__ptr;
                return __ptr0 == IntPtr.Zero ? null : (GlBlitFramebufferEXTPROC)Marshal.GetDelegateForFunctionPointer(__ptr0, typeof(GlBlitFramebufferEXTPROC));
            }

            set
            {
                IntPtr* __ptr = (IntPtr*)SymbolResolver.Resolve("Libraries/Spout.dll", "?glBlitFramebufferEXT@@3P6AXHHHHHHHHII@ZEA");
                *__ptr = value == null ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(value);
            }
        }

        public static PFNWGLSWAPINTERVALEXTPROC WglSwapIntervalEXT
        {
            get
            {
                IntPtr* __ptr = (IntPtr*)SymbolResolver.Resolve("Libraries/Spout.dll", "?wglSwapIntervalEXT@@3P6AHH@ZEA");
                IntPtr __ptr0 = *__ptr;
                return __ptr0 == IntPtr.Zero ? null : (PFNWGLSWAPINTERVALEXTPROC)Marshal.GetDelegateForFunctionPointer(__ptr0, typeof(PFNWGLSWAPINTERVALEXTPROC));
            }

            set
            {
                IntPtr* __ptr = (IntPtr*)SymbolResolver.Resolve("Libraries/Spout.dll", "?wglSwapIntervalEXT@@3P6AHH@ZEA");
                *__ptr = value == null ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(value);
            }
        }

        public static PFNWGLGETSWAPINTERVALEXTPROC WglGetSwapIntervalEXT
        {
            get
            {
                IntPtr* __ptr = (IntPtr*)SymbolResolver.Resolve("Libraries/Spout.dll", "?wglGetSwapIntervalEXT@@3P6AHXZEA");
                IntPtr __ptr0 = *__ptr;
                return __ptr0 == IntPtr.Zero ? null : (PFNWGLGETSWAPINTERVALEXTPROC)Marshal.GetDelegateForFunctionPointer(__ptr0, typeof(PFNWGLGETSWAPINTERVALEXTPROC));
            }

            set
            {
                IntPtr* __ptr = (IntPtr*)SymbolResolver.Resolve("Libraries/Spout.dll", "?wglGetSwapIntervalEXT@@3P6AHXZEA");
                *__ptr = value == null ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(value);
            }
        }

        public static GlGenBuffersPROC GlGenBuffersEXT
        {
            get
            {
                IntPtr* __ptr = (IntPtr*)SymbolResolver.Resolve("Libraries/Spout.dll", "?glGenBuffersEXT@@3P6AXHPEBI@ZEA");
                IntPtr __ptr0 = *__ptr;
                return __ptr0 == IntPtr.Zero ? null : (GlGenBuffersPROC)Marshal.GetDelegateForFunctionPointer(__ptr0, typeof(GlGenBuffersPROC));
            }

            set
            {
                IntPtr* __ptr = (IntPtr*)SymbolResolver.Resolve("Libraries/Spout.dll", "?glGenBuffersEXT@@3P6AXHPEBI@ZEA");
                *__ptr = value == null ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(value);
            }
        }

        public static GlDeleteBuffersPROC GlDeleteBuffersEXT
        {
            get
            {
                IntPtr* __ptr = (IntPtr*)SymbolResolver.Resolve("Libraries/Spout.dll", "?glDeleteBuffersEXT@@3P6AXHPEBI@ZEA");
                IntPtr __ptr0 = *__ptr;
                return __ptr0 == IntPtr.Zero ? null : (GlDeleteBuffersPROC)Marshal.GetDelegateForFunctionPointer(__ptr0, typeof(GlDeleteBuffersPROC));
            }

            set
            {
                IntPtr* __ptr = (IntPtr*)SymbolResolver.Resolve("Libraries/Spout.dll", "?glDeleteBuffersEXT@@3P6AXHPEBI@ZEA");
                *__ptr = value == null ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(value);
            }
        }

        public static GlBindBufferPROC GlBindBufferEXT
        {
            get
            {
                IntPtr* __ptr = (IntPtr*)SymbolResolver.Resolve("Libraries/Spout.dll", "?glBindBufferEXT@@3P6AXII@ZEA");
                IntPtr __ptr0 = *__ptr;
                return __ptr0 == IntPtr.Zero ? null : (GlBindBufferPROC)Marshal.GetDelegateForFunctionPointer(__ptr0, typeof(GlBindBufferPROC));
            }

            set
            {
                IntPtr* __ptr = (IntPtr*)SymbolResolver.Resolve("Libraries/Spout.dll", "?glBindBufferEXT@@3P6AXII@ZEA");
                *__ptr = value == null ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(value);
            }
        }

        public static GlBufferDataPROC GlBufferDataEXT
        {
            get
            {
                IntPtr* __ptr = (IntPtr*)SymbolResolver.Resolve("Libraries/Spout.dll", "?glBufferDataEXT@@3P6AXI_JPEBXI@ZEA");
                IntPtr __ptr0 = *__ptr;
                return __ptr0 == IntPtr.Zero ? null : (GlBufferDataPROC)Marshal.GetDelegateForFunctionPointer(__ptr0, typeof(GlBufferDataPROC));
            }

            set
            {
                IntPtr* __ptr = (IntPtr*)SymbolResolver.Resolve("Libraries/Spout.dll", "?glBufferDataEXT@@3P6AXI_JPEBXI@ZEA");
                *__ptr = value == null ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(value);
            }
        }

        public static GlMapBufferPROC GlMapBufferEXT
        {
            get
            {
                IntPtr* __ptr = (IntPtr*)SymbolResolver.Resolve("Libraries/Spout.dll", "?glMapBufferEXT@@3P6APEAXII@ZEA");
                IntPtr __ptr0 = *__ptr;
                return __ptr0 == IntPtr.Zero ? null : (GlMapBufferPROC)Marshal.GetDelegateForFunctionPointer(__ptr0, typeof(GlMapBufferPROC));
            }

            set
            {
                IntPtr* __ptr = (IntPtr*)SymbolResolver.Resolve("Libraries/Spout.dll", "?glMapBufferEXT@@3P6APEAXII@ZEA");
                *__ptr = value == null ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(value);
            }
        }

        public static GlUnmapBufferPROC GlUnmapBufferEXT
        {
            get
            {
                IntPtr* __ptr = (IntPtr*)SymbolResolver.Resolve("Libraries/Spout.dll", "?glUnmapBufferEXT@@3P6AXI@ZEA");
                IntPtr __ptr0 = *__ptr;
                return __ptr0 == IntPtr.Zero ? null : (GlUnmapBufferPROC)Marshal.GetDelegateForFunctionPointer(__ptr0, typeof(GlUnmapBufferPROC));
            }

            set
            {
                IntPtr* __ptr = (IntPtr*)SymbolResolver.Resolve("Libraries/Spout.dll", "?glUnmapBufferEXT@@3P6AXI@ZEA");
                *__ptr = value == null ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(value);
            }
        }
    }

    public unsafe partial class SpoutGLDXinterop : IDisposable
    {
        [StructLayout(LayoutKind.Explicit, Size = 664)]
        public partial struct __Internal
        {
            [FieldOffset(0)]
            internal SpoutSenderNames.__Internal senders;

            [FieldOffset(112)]
            internal SpoutDirectX.__Internal spoutdx;

            [FieldOffset(144)]
            internal SpoutCopy.__Internal spoutcopy;

            [FieldOffset(152)]
            internal SpoutMemoryShare.__Internal memoryshare;

            [FieldOffset(168)]
            internal byte m_bUseDX9;

            [FieldOffset(169)]
            internal byte m_bUseCPU;

            [FieldOffset(170)]
            internal byte m_bUseMemory;

            [FieldOffset(172)]
            internal D3DFORMAT DX9format;

            [FieldOffset(176)]
            internal DXGI_FORMAT DX11format;

            [FieldOffset(180)]
            internal uint m_glTexture;

            [FieldOffset(184)]
            internal uint m_fbo;

            [FieldOffset(192)]
            internal IntPtr m_pDevice;

            [FieldOffset(200)]
            internal IntPtr m_dxTexture;

            [FieldOffset(208)]
            internal IntPtr m_dxShareHandle;

            [FieldOffset(216)]
            internal IntPtr g_pd3dDevice;

            [FieldOffset(224)]
            internal IntPtr g_pSharedTexture;

            [FieldOffset(232)]
            internal byte m_bInitialized;

            [FieldOffset(233)]
            internal byte m_bExtensionsLoaded;

            [FieldOffset(236)]
            internal uint m_caps;

            [FieldOffset(240)]
            internal byte m_bFBOavailable;

            [FieldOffset(241)]
            internal byte m_bBLITavailable;

            [FieldOffset(242)]
            internal byte m_bPBOavailable;

            [FieldOffset(243)]
            internal byte m_bSWAPavailable;

            [FieldOffset(244)]
            internal byte m_bBGRAavailable;

            [FieldOffset(245)]
            internal byte m_bGLDXavailable;

            [FieldOffset(248)]
            internal IntPtr m_hWnd;

            [FieldOffset(256)]
            internal IntPtr m_hSharedMemory;

            [FieldOffset(264)]
            internal SharedTextureInfo.__Internal m_TextureInfo;

            [FieldOffset(544)]
            internal uint m_TexID;

            [FieldOffset(548)]
            internal uint m_TexWidth;

            [FieldOffset(552)]
            internal uint m_TexHeight;

            [FieldOffset(556)]
            internal fixed uint m_pbo[2];

            [FieldOffset(564)]
            internal int PboIndex;

            [FieldOffset(568)]
            internal int NextPboIndex;

            [FieldOffset(576)]
            internal IntPtr m_hdc;

            [FieldOffset(584)]
            internal IntPtr m_hwndButton;

            [FieldOffset(592)]
            internal IntPtr m_hRc;

            [FieldOffset(600)]
            internal IntPtr g_pImmediateContext;

            [FieldOffset(608)]
            internal D3D_DRIVER_TYPE g_driverType;

            [FieldOffset(612)]
            internal D3D_FEATURE_LEVEL g_featureLevel;

            [FieldOffset(616)]
            internal IntPtr g_pStagingTexture;

            [FieldOffset(624)]
            internal IntPtr m_pD3D;

            [FieldOffset(632)]
            internal IntPtr g_DX9surface;

            [FieldOffset(640)]
            internal IntPtr m_hInteropDevice;

            [FieldOffset(648)]
            internal IntPtr m_hInteropObject;

            [FieldOffset(656)]
            internal IntPtr m_hAccessMutex;

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "??0spoutGLDXinterop@@QEAA@XZ")]
            internal static extern IntPtr ctor(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "??0spoutGLDXinterop@@QEAA@AEBV0@@Z")]
            internal static extern IntPtr cctor(IntPtr __instance, IntPtr _0);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "??1spoutGLDXinterop@@QEAA@XZ")]
            internal static extern void dtor(IntPtr __instance, int delete);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?LoadGLextensions@spoutGLDXinterop@@QEAA_NXZ")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool LoadGLextensions(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?CleanupInterop@spoutGLDXinterop@@QEAAX_N@Z")]
            internal static extern void CleanupInterop(IntPtr __instance, bool bExit);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?getSharedInfo@spoutGLDXinterop@@QEAA_NPEADPEAUSharedTextureInfo@@@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool GetSharedInfo(IntPtr __instance, sbyte* sharedMemoryName, IntPtr info);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?setSharedInfo@spoutGLDXinterop@@QEAA_NPEADPEAUSharedTextureInfo@@@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool SetSharedInfo(IntPtr __instance, sbyte* sharedMemoryName, IntPtr info);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?WriteTexture@spoutGLDXinterop@@QEAA_NIIII_NI@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool WriteTexture(IntPtr __instance, uint TextureID, uint TextureTarget, uint width, uint height, bool bInvert, uint HostFBO);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?ReadTexture@spoutGLDXinterop@@QEAA_NIIII_NI@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool ReadTexture(IntPtr __instance, uint TextureID, uint TextureTarget, uint width, uint height, bool bInvert, uint HostFBO);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?WriteTexturePixels@spoutGLDXinterop@@QEAA_NPEBEIII_NI@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool WriteTexturePixels(IntPtr __instance, byte* pixels, uint width, uint height, uint glFormat, bool bInvert, uint HostFBO);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?ReadTexturePixels@spoutGLDXinterop@@QEAA_NPEAEIII_NI@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool ReadTexturePixels(IntPtr __instance, byte* pixels, uint width, uint height, uint glFormat, bool bInvert, uint HostFBO);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?DrawSharedTexture@spoutGLDXinterop@@QEAA_NMMM_NI@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool DrawSharedTexture(IntPtr __instance, float max_x, float max_y, float aspect, bool bInvert, uint HostFBO);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?DrawToSharedTexture@spoutGLDXinterop@@QEAA_NIIIIMMM_NI@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool DrawToSharedTexture(IntPtr __instance, uint TexID, uint TexTarget, uint width, uint height, float max_x, float max_y, float aspect, bool bInvert, uint HostFBO);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?BindSharedTexture@spoutGLDXinterop@@QEAA_NXZ")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool BindSharedTexture(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?UnloadTexturePixels@spoutGLDXinterop@@QEAA_NIIIIPEAEI_NI@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool UnloadTexturePixels(IntPtr __instance, uint TextureID, uint TextureTarget, uint width, uint height, byte* data, uint glFormat, bool bInvert, uint HostFBO);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?LoadTexturePixels@spoutGLDXinterop@@QEAA_NIIIIPEBEI_N@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool LoadTexturePixels(IntPtr __instance, uint TextureID, uint TextureTarget, uint width, uint height, byte* data, uint glFormat, bool bInvert);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GetDX9@spoutGLDXinterop@@QEAA_NXZ")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool GetDX9(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?SetDX9@spoutGLDXinterop@@QEAA_N_N@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool SetDX9(IntPtr __instance, bool bDX9);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?UseDX9@spoutGLDXinterop@@QEAA_N_N@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool UseDX9(IntPtr __instance, bool bDX9);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?SetCPUmode@spoutGLDXinterop@@QEAA_N_N@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool SetCPUmode(IntPtr __instance, bool bCPU);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?SetMemoryShareMode@spoutGLDXinterop@@QEAA_N_N@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool SetMemoryShareMode(IntPtr __instance, bool bMem);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?SetShareMode@spoutGLDXinterop@@QEAA_NH@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool SetShareMode(IntPtr __instance, int mode);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GetAdapterName@spoutGLDXinterop@@QEAA_NHPEADH@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool GetAdapterName(IntPtr __instance, int index, sbyte* adaptername, int maxchars);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?SetAdapter@spoutGLDXinterop@@QEAA_NH@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool SetAdapter(IntPtr __instance, int index);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GetHostPath@spoutGLDXinterop@@QEAA_NPEBDPEADH@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool GetHostPath(IntPtr __instance, [MarshalAs(UnmanagedType.LPUTF8Str)] string sendername, sbyte* hostpath, int maxchars);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?CreateDX9interop@spoutGLDXinterop@@QEAA_NIIK_N@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool CreateDX9interop(IntPtr __instance, uint width, uint height, uint dwFormat, bool bReceive);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?CleanupDX9@spoutGLDXinterop@@QEAAX_N@Z")]
            internal static extern void CleanupDX9(IntPtr __instance, bool bExit);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?CreateDX11interop@spoutGLDXinterop@@QEAA_NIIK_N@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool CreateDX11interop(IntPtr __instance, uint width, uint height, uint dwFormat, bool bReceive);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?OpenDirectX11@spoutGLDXinterop@@QEAA_NXZ")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool OpenDirectX11(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?CleanupDX11@spoutGLDXinterop@@QEAAX_N@Z")]
            internal static extern void CleanupDX11(IntPtr __instance, bool bExit);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?CleanupDirectX@spoutGLDXinterop@@QEAAX_N@Z")]
            internal static extern void CleanupDirectX(IntPtr __instance, bool bExit);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?LinkGLDXtextures@spoutGLDXinterop@@QEAAPEAXPEAX00I@Z")]
            internal static extern IntPtr LinkGLDXtextures(IntPtr __instance, IntPtr pDXdevice, IntPtr pSharedTexture, IntPtr dxShareHandle, uint glTextureID);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?LockInteropObject@spoutGLDXinterop@@QEAAJPEAXPEAPEAX@Z")]
            internal static extern int LockInteropObject(IntPtr __instance, IntPtr hDevice, void** hObject);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?UnlockInteropObject@spoutGLDXinterop@@QEAAJPEAXPEAPEAX@Z")]
            internal static extern int UnlockInteropObject(IntPtr __instance, IntPtr hDevice, void** hObject);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?SetVerticalSync@spoutGLDXinterop@@QEAA_N_N@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool SetVerticalSync(IntPtr __instance, bool bSync);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GetAdapterInfo@spoutGLDXinterop@@QEAA_NPEAD0000HAEA_N@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool GetAdapterInfo(IntPtr __instance, sbyte* renderadapter, sbyte* renderdescription, sbyte* renderversion, sbyte* displaydescription, sbyte* displayversion, int maxsize, bool* bUseDX9);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?CloseOpenGL@spoutGLDXinterop@@QEAA_NXZ")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool CloseOpenGL(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?CopyTexture@spoutGLDXinterop@@QEAA_NIIIIII_NI@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool CopyTexture(IntPtr __instance, uint SourceID, uint SourceTarget, uint DestID, uint DestTarget, uint width, uint height, bool bInvert, uint HostFBO);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?InitTexture@spoutGLDXinterop@@QEAAXAEAIIII@Z")]
            internal static extern void InitTexture(IntPtr __instance, uint* texID, uint GLformat, uint width, uint height);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?CheckOpenGLTexture@spoutGLDXinterop@@QEAAXAEAIIII00@Z")]
            internal static extern void CheckOpenGLTexture(IntPtr __instance, uint* texID, uint GLformat, uint newWidth, uint newHeight, uint* texWidth, uint* texHeight);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?SaveOpenGLstate@spoutGLDXinterop@@QEAAXII_N@Z")]
            internal static extern void SaveOpenGLstate(IntPtr __instance, uint width, uint height, bool bFitWindow);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?RestoreOpenGLstate@spoutGLDXinterop@@QEAAXXZ")]
            internal static extern void RestoreOpenGLstate(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GLerror@spoutGLDXinterop@@QEAAXXZ")]
            internal static extern void GLerror(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?PrintFBOstatus@spoutGLDXinterop@@QEAAXI@Z")]
            internal static extern void PrintFBOstatus(IntPtr __instance, uint status);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?getSharedTextureInfo@spoutGLDXinterop@@IEAA_NPEBD@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool GetSharedTextureInfo(IntPtr __instance, [MarshalAs(UnmanagedType.LPUTF8Str)] string sharedMemoryName);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?setSharedTextureInfo@spoutGLDXinterop@@IEAA_NPEBD@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool SetSharedTextureInfo(IntPtr __instance, [MarshalAs(UnmanagedType.LPUTF8Str)] string sharedMemoryName);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?WriteGLDXtexture@spoutGLDXinterop@@IEAA_NIIII_NI@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool WriteGLDXtexture(IntPtr __instance, uint TextureID, uint TextureTarget, uint width, uint height, bool bInvert, uint HostFBO);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?ReadGLDXtexture@spoutGLDXinterop@@IEAA_NIIII_NI@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool ReadGLDXtexture(IntPtr __instance, uint TextureID, uint TextureTarget, uint width, uint height, bool bInvert, uint HostFBO);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?WriteGLDXpixels@spoutGLDXinterop@@IEAA_NPEBEIII_NI@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool WriteGLDXpixels(IntPtr __instance, byte* pixels, uint width, uint height, uint glFormat, bool bInvert, uint HostFBO);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?ReadGLDXpixels@spoutGLDXinterop@@IEAA_NPEAEIII_NI@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool ReadGLDXpixels(IntPtr __instance, byte* pixels, uint width, uint height, uint glFormat, bool bInvert, uint HostFBO);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?DrawGLDXtexture@spoutGLDXinterop@@IEAA_NMMM_N@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool DrawGLDXtexture(IntPtr __instance, float max_x, float max_y, float aspect, bool bInvert);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?DrawToGLDXtexture@spoutGLDXinterop@@IEAA_NIIIIMMM_NI@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool DrawToGLDXtexture(IntPtr __instance, uint TexID, uint TexTarget, uint width, uint height, float max_x, float max_y, float aspect, bool bInvert, uint HostFBO);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?WriteDX11texture@spoutGLDXinterop@@IEAA_NIIII_NI@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool WriteDX11texture(IntPtr __instance, uint TextureID, uint TextureTarget, uint width, uint height, bool bInvert, uint HostFBO);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?ReadDX11texture@spoutGLDXinterop@@IEAA_NIIII_NI@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool ReadDX11texture(IntPtr __instance, uint TextureID, uint TextureTarget, uint width, uint height, bool bInvert, uint HostFBO);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?WriteDX11pixels@spoutGLDXinterop@@IEAA_NPEBEIII_N@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool WriteDX11pixels(IntPtr __instance, byte* pixels, uint width, uint height, uint glFormat, bool bInvert);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?ReadDX11pixels@spoutGLDXinterop@@IEAA_NPEAEIII_N@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool ReadDX11pixels(IntPtr __instance, byte* pixels, uint width, uint height, uint glFormat, bool bInvert);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?DrawDX11texture@spoutGLDXinterop@@IEAA_NMMM_NI@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool DrawDX11texture(IntPtr __instance, float max_x, float max_y, float aspect, bool bInvert, uint HostFBO);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?DrawToDX11texture@spoutGLDXinterop@@IEAA_NIIIIMMM_NI@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool DrawToDX11texture(IntPtr __instance, uint TextureID, uint TextureTarget, uint width, uint height, float max_x, float max_y, float aspect, bool bInvert, uint HostFBO);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?CheckStagingTexture@spoutGLDXinterop@@IEAA_NII@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool CheckStagingTexture(IntPtr __instance, uint width, uint height);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?FlushWait@spoutGLDXinterop@@IEAAXXZ")]
            internal static extern void FlushWait(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?WriteDX9texture@spoutGLDXinterop@@IEAA_NIIII_NI@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool WriteDX9texture(IntPtr __instance, uint TextureID, uint TextureTarget, uint width, uint height, bool bInvert, uint HostFBO);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?ReadDX9texture@spoutGLDXinterop@@IEAA_NIIII_NI@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool ReadDX9texture(IntPtr __instance, uint TextureID, uint TextureTarget, uint width, uint height, bool bInvert, uint HostFBO);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?WriteDX9pixels@spoutGLDXinterop@@IEAA_NPEBEIII_N@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool WriteDX9pixels(IntPtr __instance, byte* pixels, uint width, uint height, uint glFormat, bool bInvert);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?ReadDX9pixels@spoutGLDXinterop@@IEAA_NPEAEIII_N@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool ReadDX9pixels(IntPtr __instance, byte* pixels, uint width, uint height, uint glFormat, bool bInvert);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?DrawDX9texture@spoutGLDXinterop@@IEAA_NMMM_NI@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool DrawDX9texture(IntPtr __instance, float max_x, float max_y, float aspect, bool bInvert, uint HostFBO);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?DrawToDX9texture@spoutGLDXinterop@@IEAA_NIIIIMMM_NI@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool DrawToDX9texture(IntPtr __instance, uint TextureID, uint TextureTarget, uint width, uint height, float max_x, float max_y, float aspect, bool bInvert, uint HostFBO);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?CheckDX9surface@spoutGLDXinterop@@IEAA_NII@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool CheckDX9surface(IntPtr __instance, uint width, uint height);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?WriteMemory@spoutGLDXinterop@@IEAA_NIIII_NI@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool WriteMemory(IntPtr __instance, uint TexID, uint TextureTarget, uint width, uint height, bool bInvert, uint HostFBO);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?ReadMemory@spoutGLDXinterop@@IEAA_NIIII_NI@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool ReadMemory(IntPtr __instance, uint TexID, uint TextureTarget, uint width, uint height, bool bInvert, uint HostFBO);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?WriteMemoryPixels@spoutGLDXinterop@@IEAA_NPEBEIII_N@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool WriteMemoryPixels(IntPtr __instance, byte* pixels, uint width, uint height, uint glFormat, bool bInvert);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?ReadMemoryPixels@spoutGLDXinterop@@IEAA_NPEAEIII_N@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool ReadMemoryPixels(IntPtr __instance, byte* pixels, uint width, uint height, uint glFormat, bool bInvert);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?DrawSharedMemory@spoutGLDXinterop@@IEAA_NMMM_N@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool DrawSharedMemory(IntPtr __instance, float max_x, float max_y, float aspect, bool bInvert);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?DrawToSharedMemory@spoutGLDXinterop@@IEAA_NIIIIMMM_NI@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool DrawToSharedMemory(IntPtr __instance, uint TextureID, uint TextureTarget, uint width, uint height, float max_x, float max_y, float aspect, bool bInvert, uint HostFBO);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?OpenDeviceKey@spoutGLDXinterop@@IEAA_NPEBDHPEAD1@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool OpenDeviceKey(IntPtr __instance, [MarshalAs(UnmanagedType.LPUTF8Str)] string key, int maxsize, sbyte* description, sbyte* version);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?trim@spoutGLDXinterop@@IEAAXPEAD@Z")]
            internal static extern void Trim(IntPtr __instance, sbyte* s);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?UnBindSharedTexture@spoutGLDXinterop@@QEAA_NXZ")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool UnBindSharedTexture(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?isDX9@spoutGLDXinterop@@QEAA_NXZ")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool IsDX9(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GetCPUmode@spoutGLDXinterop@@QEAA_NXZ")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool GetCPUmode(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GetMemoryShareMode@spoutGLDXinterop@@QEAA_NXZ")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool GetMemoryShareMode(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GetShareMode@spoutGLDXinterop@@QEAAHXZ")]
            internal static extern int GetShareMode(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?IsBGRAavailable@spoutGLDXinterop@@QEAA_NXZ")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool IsBGRAavailable(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?IsPBOavailable@spoutGLDXinterop@@QEAA_NXZ")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool IsPBOavailable(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GetBufferMode@spoutGLDXinterop@@QEAA_NXZ")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool GetBufferMode(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?SetBufferMode@spoutGLDXinterop@@QEAAX_N@Z")]
            internal static extern void SetBufferMode(IntPtr __instance, bool bActive);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GetNumAdapters@spoutGLDXinterop@@QEAAHXZ")]
            internal static extern int GetNumAdapters(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GetAdapter@spoutGLDXinterop@@QEAAHXZ")]
            internal static extern int GetAdapter(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?DX11available@spoutGLDXinterop@@QEAA_NXZ")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool DX11available(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GLDXcompatible@spoutGLDXinterop@@QEAA_NXZ")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool GLDXcompatible(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?isOptimus@spoutGLDXinterop@@QEAA_NXZ")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool IsOptimus(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GetVerticalSync@spoutGLDXinterop@@QEAAHXZ")]
            internal static extern int GetVerticalSync(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GetSpoutVersion@spoutGLDXinterop@@QEAAKXZ")]
            internal static extern uint GetSpoutVersion(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?InitOpenGL@spoutGLDXinterop@@QEAA_NXZ")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool InitOpenGL(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GetGLtextureID@spoutGLDXinterop@@QEAAIXZ")]
            internal static extern uint GetGLtextureID(IntPtr __instance);
        }

        public IntPtr __Instance { get; protected set; }

        internal static readonly System.Collections.Concurrent.ConcurrentDictionary<IntPtr, SpoutGLDXinterop> NativeToManagedMap = new();

        protected bool __ownsNativeInstance;

        internal static SpoutGLDXinterop __CreateInstance(IntPtr native, bool skipVTables = false)
        {
            return new SpoutGLDXinterop(native.ToPointer(), skipVTables);
        }

        internal static SpoutGLDXinterop __CreateInstance(SpoutGLDXinterop.__Internal native, bool skipVTables = false)
        {
            return new SpoutGLDXinterop(native, skipVTables);
        }

        private static void* __CopyValue(SpoutGLDXinterop.__Internal native)
        {
            IntPtr ret = Marshal.AllocHGlobal(sizeof(SpoutGLDXinterop.__Internal));
            *(SpoutGLDXinterop.__Internal*)ret = native;
            return ret.ToPointer();
        }

        private SpoutGLDXinterop(SpoutGLDXinterop.__Internal native, bool skipVTables = false)
            : this(__CopyValue(native), skipVTables)
        {
            __ownsNativeInstance = true;
            NativeToManagedMap[__Instance] = this;
        }

        protected SpoutGLDXinterop(void* native, bool skipVTables = false)
        {
            if (native == null)
            {
                return;
            }

            __Instance = new IntPtr(native);
        }

        public SpoutGLDXinterop()
        {
            __Instance = Marshal.AllocHGlobal(sizeof(SpoutGLDXinterop.__Internal));
            __ownsNativeInstance = true;
            NativeToManagedMap[__Instance] = this;
            _ = __Internal.ctor(__Instance);
        }

        public SpoutGLDXinterop(SpoutGLDXinterop _0)
        {
            __Instance = Marshal.AllocHGlobal(sizeof(SpoutGLDXinterop.__Internal));
            __ownsNativeInstance = true;
            NativeToManagedMap[__Instance] = this;
            *(SpoutGLDXinterop.__Internal*)__Instance = *(SpoutGLDXinterop.__Internal*)_0.__Instance;
        }

        ~SpoutGLDXinterop()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (__Instance == IntPtr.Zero)
            {
                return;
            }

            _ = NativeToManagedMap.TryRemove(__Instance, out _);
            if (disposing)
            {
                __Internal.dtor(__Instance, 0);
            }

            if (__ownsNativeInstance)
            {
                Marshal.FreeHGlobal(__Instance);
            }

            __Instance = IntPtr.Zero;
        }

        public bool LoadGLextensions()
        {
            bool __ret = __Internal.LoadGLextensions(__Instance);
            return __ret;
        }

        public void CleanupInterop(bool bExit)
        {
            __Internal.CleanupInterop(__Instance, bExit);
        }

        public bool GetSharedInfo(sbyte* sharedMemoryName, SharedTextureInfo info)
        {
            IntPtr __arg1 = info is null ? IntPtr.Zero : info.__Instance;
            bool __ret = __Internal.GetSharedInfo(__Instance, sharedMemoryName, __arg1);
            return __ret;
        }

        public bool SetSharedInfo(sbyte* sharedMemoryName, SharedTextureInfo info)
        {
            IntPtr __arg1 = info is null ? IntPtr.Zero : info.__Instance;
            bool __ret = __Internal.SetSharedInfo(__Instance, sharedMemoryName, __arg1);
            return __ret;
        }

        public bool WriteTexture(uint TextureID, uint TextureTarget, uint width, uint height, bool bInvert, uint HostFBO)
        {
            bool __ret = __Internal.WriteTexture(__Instance, TextureID, TextureTarget, width, height, bInvert, HostFBO);
            return __ret;
        }

        public bool ReadTexture(uint TextureID, uint TextureTarget, uint width, uint height, bool bInvert, uint HostFBO)
        {
            bool __ret = __Internal.ReadTexture(__Instance, TextureID, TextureTarget, width, height, bInvert, HostFBO);
            return __ret;
        }

        public bool WriteTexturePixels(byte* pixels, uint width, uint height, uint glFormat, bool bInvert, uint HostFBO)
        {
            bool __ret = __Internal.WriteTexturePixels(__Instance, pixels, width, height, glFormat, bInvert, HostFBO);
            return __ret;
        }

        public bool ReadTexturePixels(byte* pixels, uint width, uint height, uint glFormat, bool bInvert, uint HostFBO)
        {
            bool __ret = __Internal.ReadTexturePixels(__Instance, pixels, width, height, glFormat, bInvert, HostFBO);
            return __ret;
        }

        public bool DrawSharedTexture(float max_x, float max_y, float aspect, bool bInvert, uint HostFBO)
        {
            bool __ret = __Internal.DrawSharedTexture(__Instance, max_x, max_y, aspect, bInvert, HostFBO);
            return __ret;
        }

        public bool DrawToSharedTexture(uint TexID, uint TexTarget, uint width, uint height, float max_x, float max_y, float aspect, bool bInvert, uint HostFBO)
        {
            bool __ret = __Internal.DrawToSharedTexture(__Instance, TexID, TexTarget, width, height, max_x, max_y, aspect, bInvert, HostFBO);
            return __ret;
        }

        public bool BindSharedTexture()
        {
            bool __ret = __Internal.BindSharedTexture(__Instance);
            return __ret;
        }

        public bool UnloadTexturePixels(uint TextureID, uint TextureTarget, uint width, uint height, byte* data, uint glFormat, bool bInvert, uint HostFBO)
        {
            bool __ret = __Internal.UnloadTexturePixels(__Instance, TextureID, TextureTarget, width, height, data, glFormat, bInvert, HostFBO);
            return __ret;
        }

        public bool LoadTexturePixels(uint TextureID, uint TextureTarget, uint width, uint height, byte* data, uint glFormat, bool bInvert)
        {
            bool __ret = __Internal.LoadTexturePixels(__Instance, TextureID, TextureTarget, width, height, data, glFormat, bInvert);
            return __ret;
        }

        public bool GetDX9()
        {
            bool __ret = __Internal.GetDX9(__Instance);
            return __ret;
        }

        public bool SetDX9(bool bDX9)
        {
            bool __ret = __Internal.SetDX9(__Instance, bDX9);
            return __ret;
        }

        public bool UseDX9(bool bDX9)
        {
            bool __ret = __Internal.UseDX9(__Instance, bDX9);
            return __ret;
        }

        public bool SetCPUmode(bool bCPU)
        {
            bool __ret = __Internal.SetCPUmode(__Instance, bCPU);
            return __ret;
        }

        public bool SetMemoryShareMode(bool bMem)
        {
            bool __ret = __Internal.SetMemoryShareMode(__Instance, bMem);
            return __ret;
        }

        public bool SetShareMode(int mode)
        {
            bool __ret = __Internal.SetShareMode(__Instance, mode);
            return __ret;
        }

        public bool GetAdapterName(int index, sbyte* adaptername, int maxchars)
        {
            bool __ret = __Internal.GetAdapterName(__Instance, index, adaptername, maxchars);
            return __ret;
        }

        public bool SetAdapter(int index)
        {
            bool __ret = __Internal.SetAdapter(__Instance, index);
            return __ret;
        }

        public bool GetHostPath(string sendername, sbyte* hostpath, int maxchars)
        {
            bool __ret = __Internal.GetHostPath(__Instance, sendername, hostpath, maxchars);
            return __ret;
        }

        public bool CreateDX9interop(uint width, uint height, uint dwFormat, bool bReceive)
        {
            bool __ret = __Internal.CreateDX9interop(__Instance, width, height, dwFormat, bReceive);
            return __ret;
        }

        public void CleanupDX9(bool bExit)
        {
            __Internal.CleanupDX9(__Instance, bExit);
        }

        public bool CreateDX11interop(uint width, uint height, uint dwFormat, bool bReceive)
        {
            bool __ret = __Internal.CreateDX11interop(__Instance, width, height, dwFormat, bReceive);
            return __ret;
        }

        public bool OpenDirectX11()
        {
            bool __ret = __Internal.OpenDirectX11(__Instance);
            return __ret;
        }

        public void CleanupDX11(bool bExit)
        {
            __Internal.CleanupDX11(__Instance, bExit);
        }

        public void CleanupDirectX(bool bExit)
        {
            __Internal.CleanupDirectX(__Instance, bExit);
        }

        public IntPtr LinkGLDXtextures(IntPtr pDXdevice, IntPtr pSharedTexture, IntPtr dxShareHandle, uint glTextureID)
        {
            IntPtr __ret = __Internal.LinkGLDXtextures(__Instance, pDXdevice, pSharedTexture, dxShareHandle, glTextureID);
            return __ret;
        }

        public int LockInteropObject(IntPtr hDevice, void** hObject)
        {
            int __ret = __Internal.LockInteropObject(__Instance, hDevice, hObject);
            return __ret;
        }

        public int UnlockInteropObject(IntPtr hDevice, void** hObject)
        {
            int __ret = __Internal.UnlockInteropObject(__Instance, hDevice, hObject);
            return __ret;
        }

        public bool SetVerticalSync(bool bSync)
        {
            bool __ret = __Internal.SetVerticalSync(__Instance, bSync);
            return __ret;
        }

        public bool GetAdapterInfo(sbyte* renderadapter, sbyte* renderdescription, sbyte* renderversion, sbyte* displaydescription, sbyte* displayversion, int maxsize, ref bool bUseDX9)
        {
            fixed (bool* __bUseDX96 = &bUseDX9)
            {
                bool* __arg6 = __bUseDX96;
                bool __ret = __Internal.GetAdapterInfo(__Instance, renderadapter, renderdescription, renderversion, displaydescription, displayversion, maxsize, __arg6);
                return __ret;
            }
        }

        public bool CloseOpenGL()
        {
            bool __ret = __Internal.CloseOpenGL(__Instance);
            return __ret;
        }

        public bool CopyTexture(uint SourceID, uint SourceTarget, uint DestID, uint DestTarget, uint width, uint height, bool bInvert, uint HostFBO)
        {
            bool __ret = __Internal.CopyTexture(__Instance, SourceID, SourceTarget, DestID, DestTarget, width, height, bInvert, HostFBO);
            return __ret;
        }

        public void InitTexture(ref uint texID, uint GLformat, uint width, uint height)
        {
            fixed (uint* __texID0 = &texID)
            {
                uint* __arg0 = __texID0;
                __Internal.InitTexture(__Instance, __arg0, GLformat, width, height);
            }
        }

        public void CheckOpenGLTexture(ref uint texID, uint GLformat, uint newWidth, uint newHeight, ref uint texWidth, ref uint texHeight)
        {
            fixed (uint* __texID0 = &texID)
            {
                uint* __arg0 = __texID0;
                fixed (uint* __texWidth4 = &texWidth)
                {
                    uint* __arg4 = __texWidth4;
                    fixed (uint* __texHeight5 = &texHeight)
                    {
                        uint* __arg5 = __texHeight5;
                        __Internal.CheckOpenGLTexture(__Instance, __arg0, GLformat, newWidth, newHeight, __arg4, __arg5);
                    }
                }
            }
        }

        public void SaveOpenGLstate(uint width, uint height, bool bFitWindow)
        {
            __Internal.SaveOpenGLstate(__Instance, width, height, bFitWindow);
        }

        public void RestoreOpenGLstate()
        {
            __Internal.RestoreOpenGLstate(__Instance);
        }

        public void GLerror()
        {
            __Internal.GLerror(__Instance);
        }

        public void PrintFBOstatus(uint status)
        {
            __Internal.PrintFBOstatus(__Instance, status);
        }

        protected bool GetSharedTextureInfo(string sharedMemoryName)
        {
            bool __ret = __Internal.GetSharedTextureInfo(__Instance, sharedMemoryName);
            return __ret;
        }

        protected bool SetSharedTextureInfo(string sharedMemoryName)
        {
            bool __ret = __Internal.SetSharedTextureInfo(__Instance, sharedMemoryName);
            return __ret;
        }

        protected bool WriteGLDXtexture(uint TextureID, uint TextureTarget, uint width, uint height, bool bInvert, uint HostFBO)
        {
            bool __ret = __Internal.WriteGLDXtexture(__Instance, TextureID, TextureTarget, width, height, bInvert, HostFBO);
            return __ret;
        }

        protected bool ReadGLDXtexture(uint TextureID, uint TextureTarget, uint width, uint height, bool bInvert, uint HostFBO)
        {
            bool __ret = __Internal.ReadGLDXtexture(__Instance, TextureID, TextureTarget, width, height, bInvert, HostFBO);
            return __ret;
        }

        protected bool WriteGLDXpixels(byte* pixels, uint width, uint height, uint glFormat, bool bInvert, uint HostFBO)
        {
            bool __ret = __Internal.WriteGLDXpixels(__Instance, pixels, width, height, glFormat, bInvert, HostFBO);
            return __ret;
        }

        protected bool ReadGLDXpixels(byte* pixels, uint width, uint height, uint glFormat, bool bInvert, uint HostFBO)
        {
            bool __ret = __Internal.ReadGLDXpixels(__Instance, pixels, width, height, glFormat, bInvert, HostFBO);
            return __ret;
        }

        protected bool DrawGLDXtexture(float max_x, float max_y, float aspect, bool bInvert)
        {
            bool __ret = __Internal.DrawGLDXtexture(__Instance, max_x, max_y, aspect, bInvert);
            return __ret;
        }

        protected bool DrawToGLDXtexture(uint TexID, uint TexTarget, uint width, uint height, float max_x, float max_y, float aspect, bool bInvert, uint HostFBO)
        {
            bool __ret = __Internal.DrawToGLDXtexture(__Instance, TexID, TexTarget, width, height, max_x, max_y, aspect, bInvert, HostFBO);
            return __ret;
        }

        protected bool WriteDX11texture(uint TextureID, uint TextureTarget, uint width, uint height, bool bInvert, uint HostFBO)
        {
            bool __ret = __Internal.WriteDX11texture(__Instance, TextureID, TextureTarget, width, height, bInvert, HostFBO);
            return __ret;
        }

        protected bool ReadDX11texture(uint TextureID, uint TextureTarget, uint width, uint height, bool bInvert, uint HostFBO)
        {
            bool __ret = __Internal.ReadDX11texture(__Instance, TextureID, TextureTarget, width, height, bInvert, HostFBO);
            return __ret;
        }

        protected bool WriteDX11pixels(byte* pixels, uint width, uint height, uint glFormat, bool bInvert)
        {
            bool __ret = __Internal.WriteDX11pixels(__Instance, pixels, width, height, glFormat, bInvert);
            return __ret;
        }

        protected bool ReadDX11pixels(byte* pixels, uint width, uint height, uint glFormat, bool bInvert)
        {
            bool __ret = __Internal.ReadDX11pixels(__Instance, pixels, width, height, glFormat, bInvert);
            return __ret;
        }

        protected bool DrawDX11texture(float max_x, float max_y, float aspect, bool bInvert, uint HostFBO)
        {
            bool __ret = __Internal.DrawDX11texture(__Instance, max_x, max_y, aspect, bInvert, HostFBO);
            return __ret;
        }

        protected bool DrawToDX11texture(uint TextureID, uint TextureTarget, uint width, uint height, float max_x, float max_y, float aspect, bool bInvert, uint HostFBO)
        {
            bool __ret = __Internal.DrawToDX11texture(__Instance, TextureID, TextureTarget, width, height, max_x, max_y, aspect, bInvert, HostFBO);
            return __ret;
        }

        protected bool CheckStagingTexture(uint width, uint height)
        {
            bool __ret = __Internal.CheckStagingTexture(__Instance, width, height);
            return __ret;
        }

        protected void FlushWait()
        {
            __Internal.FlushWait(__Instance);
        }

        protected bool WriteDX9texture(uint TextureID, uint TextureTarget, uint width, uint height, bool bInvert, uint HostFBO)
        {
            bool __ret = __Internal.WriteDX9texture(__Instance, TextureID, TextureTarget, width, height, bInvert, HostFBO);
            return __ret;
        }

        protected bool ReadDX9texture(uint TextureID, uint TextureTarget, uint width, uint height, bool bInvert, uint HostFBO)
        {
            bool __ret = __Internal.ReadDX9texture(__Instance, TextureID, TextureTarget, width, height, bInvert, HostFBO);
            return __ret;
        }

        protected bool WriteDX9pixels(byte* pixels, uint width, uint height, uint glFormat, bool bInvert)
        {
            bool __ret = __Internal.WriteDX9pixels(__Instance, pixels, width, height, glFormat, bInvert);
            return __ret;
        }

        protected bool ReadDX9pixels(byte* pixels, uint width, uint height, uint glFormat, bool bInvert)
        {
            bool __ret = __Internal.ReadDX9pixels(__Instance, pixels, width, height, glFormat, bInvert);
            return __ret;
        }

        protected bool DrawDX9texture(float max_x, float max_y, float aspect, bool bInvert, uint HostFBO)
        {
            bool __ret = __Internal.DrawDX9texture(__Instance, max_x, max_y, aspect, bInvert, HostFBO);
            return __ret;
        }

        protected bool DrawToDX9texture(uint TextureID, uint TextureTarget, uint width, uint height, float max_x, float max_y, float aspect, bool bInvert, uint HostFBO)
        {
            bool __ret = __Internal.DrawToDX9texture(__Instance, TextureID, TextureTarget, width, height, max_x, max_y, aspect, bInvert, HostFBO);
            return __ret;
        }

        protected bool CheckDX9surface(uint width, uint height)
        {
            bool __ret = __Internal.CheckDX9surface(__Instance, width, height);
            return __ret;
        }

        protected bool WriteMemory(uint TexID, uint TextureTarget, uint width, uint height, bool bInvert, uint HostFBO)
        {
            bool __ret = __Internal.WriteMemory(__Instance, TexID, TextureTarget, width, height, bInvert, HostFBO);
            return __ret;
        }

        protected bool ReadMemory(uint TexID, uint TextureTarget, uint width, uint height, bool bInvert, uint HostFBO)
        {
            bool __ret = __Internal.ReadMemory(__Instance, TexID, TextureTarget, width, height, bInvert, HostFBO);
            return __ret;
        }

        protected bool WriteMemoryPixels(byte* pixels, uint width, uint height, uint glFormat, bool bInvert)
        {
            bool __ret = __Internal.WriteMemoryPixels(__Instance, pixels, width, height, glFormat, bInvert);
            return __ret;
        }

        protected bool ReadMemoryPixels(byte* pixels, uint width, uint height, uint glFormat, bool bInvert)
        {
            bool __ret = __Internal.ReadMemoryPixels(__Instance, pixels, width, height, glFormat, bInvert);
            return __ret;
        }

        protected bool DrawSharedMemory(float max_x, float max_y, float aspect, bool bInvert)
        {
            bool __ret = __Internal.DrawSharedMemory(__Instance, max_x, max_y, aspect, bInvert);
            return __ret;
        }

        protected bool DrawToSharedMemory(uint TextureID, uint TextureTarget, uint width, uint height, float max_x, float max_y, float aspect, bool bInvert, uint HostFBO)
        {
            bool __ret = __Internal.DrawToSharedMemory(__Instance, TextureID, TextureTarget, width, height, max_x, max_y, aspect, bInvert, HostFBO);
            return __ret;
        }

        protected bool OpenDeviceKey(string key, int maxsize, sbyte* description, sbyte* version)
        {
            bool __ret = __Internal.OpenDeviceKey(__Instance, key, maxsize, description, version);
            return __ret;
        }

        protected void Trim(sbyte* s)
        {
            __Internal.Trim(__Instance, s);
        }

        public SpoutSenderNames Senders
        {
            get => SpoutSenderNames.__CreateInstance(new IntPtr(&((SpoutGLDXinterop.__Internal*)__Instance)->senders));

            set
            {
                if (value is null)
                {
                    throw new ArgumentNullException("value", "Cannot be null because it is passed by value.");
                } ((SpoutGLDXinterop.__Internal*)__Instance)->senders = *(SpoutSenderNames.__Internal*)value.__Instance;
            }
        }

        public SpoutDirectX Spoutdx
        {
            get => SpoutDirectX.__CreateInstance(new IntPtr(&((SpoutGLDXinterop.__Internal*)__Instance)->spoutdx));

            set
            {
                if (value is null)
                {
                    throw new ArgumentNullException("value", "Cannot be null because it is passed by value.");
                } ((SpoutGLDXinterop.__Internal*)__Instance)->spoutdx = *(SpoutDirectX.__Internal*)value.__Instance;
            }
        }

        public SpoutCopy Spoutcopy
        {
            get => SpoutCopy.__CreateInstance(new IntPtr(&((SpoutGLDXinterop.__Internal*)__Instance)->spoutcopy));

            set
            {
                if (value is null)
                {
                    throw new ArgumentNullException("value", "Cannot be null because it is passed by value.");
                } ((SpoutGLDXinterop.__Internal*)__Instance)->spoutcopy = *(SpoutCopy.__Internal*)value.__Instance;
            }
        }

        public SpoutMemoryShare Memoryshare
        {
            get => SpoutMemoryShare.__CreateInstance(new IntPtr(&((SpoutGLDXinterop.__Internal*)__Instance)->memoryshare));

            set
            {
                if (value is null)
                {
                    throw new ArgumentNullException("value", "Cannot be null because it is passed by value.");
                } ((SpoutGLDXinterop.__Internal*)__Instance)->memoryshare = *(SpoutMemoryShare.__Internal*)value.__Instance;
            }
        }

        public bool MBUseDX9
        {
            get => ((SpoutGLDXinterop.__Internal*)__Instance)->m_bUseDX9 != 0;

            set => ((SpoutGLDXinterop.__Internal*)__Instance)->m_bUseDX9 = (byte)(value ? 1 : 0);
        }

        public bool MBUseCPU
        {
            get => ((SpoutGLDXinterop.__Internal*)__Instance)->m_bUseCPU != 0;

            set => ((SpoutGLDXinterop.__Internal*)__Instance)->m_bUseCPU = (byte)(value ? 1 : 0);
        }

        public bool MBUseMemory
        {
            get => ((SpoutGLDXinterop.__Internal*)__Instance)->m_bUseMemory != 0;

            set => ((SpoutGLDXinterop.__Internal*)__Instance)->m_bUseMemory = (byte)(value ? 1 : 0);
        }

        public uint MGlTexture
        {
            get => ((SpoutGLDXinterop.__Internal*)__Instance)->m_glTexture;

            set => ((SpoutGLDXinterop.__Internal*)__Instance)->m_glTexture = value;
        }

        public uint MFbo
        {
            get => ((SpoutGLDXinterop.__Internal*)__Instance)->m_fbo;

            set => ((SpoutGLDXinterop.__Internal*)__Instance)->m_fbo = value;
        }

        public IntPtr MDxShareHandle
        {
            get => ((SpoutGLDXinterop.__Internal*)__Instance)->m_dxShareHandle;

            set => ((SpoutGLDXinterop.__Internal*)__Instance)->m_dxShareHandle = value;
        }

        protected bool MBInitialized
        {
            get => ((SpoutGLDXinterop.__Internal*)__Instance)->m_bInitialized != 0;

            set => ((SpoutGLDXinterop.__Internal*)__Instance)->m_bInitialized = (byte)(value ? 1 : 0);
        }

        protected bool MBExtensionsLoaded
        {
            get => ((SpoutGLDXinterop.__Internal*)__Instance)->m_bExtensionsLoaded != 0;

            set => ((SpoutGLDXinterop.__Internal*)__Instance)->m_bExtensionsLoaded = (byte)(value ? 1 : 0);
        }

        protected uint MCaps
        {
            get => ((SpoutGLDXinterop.__Internal*)__Instance)->m_caps;

            set => ((SpoutGLDXinterop.__Internal*)__Instance)->m_caps = value;
        }

        protected bool MBFBOavailable
        {
            get => ((SpoutGLDXinterop.__Internal*)__Instance)->m_bFBOavailable != 0;

            set => ((SpoutGLDXinterop.__Internal*)__Instance)->m_bFBOavailable = (byte)(value ? 1 : 0);
        }

        protected bool MBBLITavailable
        {
            get => ((SpoutGLDXinterop.__Internal*)__Instance)->m_bBLITavailable != 0;

            set => ((SpoutGLDXinterop.__Internal*)__Instance)->m_bBLITavailable = (byte)(value ? 1 : 0);
        }

        protected bool MBPBOavailable
        {
            get => ((SpoutGLDXinterop.__Internal*)__Instance)->m_bPBOavailable != 0;

            set => ((SpoutGLDXinterop.__Internal*)__Instance)->m_bPBOavailable = (byte)(value ? 1 : 0);
        }

        protected bool MBSWAPavailable
        {
            get => ((SpoutGLDXinterop.__Internal*)__Instance)->m_bSWAPavailable != 0;

            set => ((SpoutGLDXinterop.__Internal*)__Instance)->m_bSWAPavailable = (byte)(value ? 1 : 0);
        }

        protected bool MBBGRAavailable
        {
            get => ((SpoutGLDXinterop.__Internal*)__Instance)->m_bBGRAavailable != 0;

            set => ((SpoutGLDXinterop.__Internal*)__Instance)->m_bBGRAavailable = (byte)(value ? 1 : 0);
        }

        protected bool MBGLDXavailable
        {
            get => ((SpoutGLDXinterop.__Internal*)__Instance)->m_bGLDXavailable != 0;

            set => ((SpoutGLDXinterop.__Internal*)__Instance)->m_bGLDXavailable = (byte)(value ? 1 : 0);
        }

        protected IntPtr MHSharedMemory
        {
            get => ((SpoutGLDXinterop.__Internal*)__Instance)->m_hSharedMemory;

            set => ((SpoutGLDXinterop.__Internal*)__Instance)->m_hSharedMemory = value;
        }

        protected SharedTextureInfo MTextureInfo
        {
            get => SharedTextureInfo.__CreateInstance(new IntPtr(&((SpoutGLDXinterop.__Internal*)__Instance)->m_TextureInfo));

            set
            {
                if (value is null)
                {
                    throw new ArgumentNullException("value", "Cannot be null because it is passed by value.");
                } ((SpoutGLDXinterop.__Internal*)__Instance)->m_TextureInfo = *(SharedTextureInfo.__Internal*)value.__Instance;
            }
        }

        protected uint MTexID
        {
            get => ((SpoutGLDXinterop.__Internal*)__Instance)->m_TexID;

            set => ((SpoutGLDXinterop.__Internal*)__Instance)->m_TexID = value;
        }

        protected uint MTexWidth
        {
            get => ((SpoutGLDXinterop.__Internal*)__Instance)->m_TexWidth;

            set => ((SpoutGLDXinterop.__Internal*)__Instance)->m_TexWidth = value;
        }

        protected uint MTexHeight
        {
            get => ((SpoutGLDXinterop.__Internal*)__Instance)->m_TexHeight;

            set => ((SpoutGLDXinterop.__Internal*)__Instance)->m_TexHeight = value;
        }

        protected uint[] MPbo
        {
            get
            {
                uint[] __value = null;
                if (((SpoutGLDXinterop.__Internal*)__Instance)->m_pbo != null)
                {
                    __value = new uint[2];
                    for (int i = 0; i < 2; i++)
                    {
                        __value[i] = ((SpoutGLDXinterop.__Internal*)__Instance)->m_pbo[i];
                    }
                }
                return __value;
            }

            set
            {
                if (value != null)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        ((SpoutGLDXinterop.__Internal*)__Instance)->m_pbo[i] = value[i];
                    }
                }
            }
        }

        protected int PboIndex
        {
            get => ((SpoutGLDXinterop.__Internal*)__Instance)->PboIndex;

            set => ((SpoutGLDXinterop.__Internal*)__Instance)->PboIndex = value;
        }

        protected int NextPboIndex
        {
            get => ((SpoutGLDXinterop.__Internal*)__Instance)->NextPboIndex;

            set => ((SpoutGLDXinterop.__Internal*)__Instance)->NextPboIndex = value;
        }

        protected IntPtr MHInteropDevice
        {
            get => ((SpoutGLDXinterop.__Internal*)__Instance)->m_hInteropDevice;

            set => ((SpoutGLDXinterop.__Internal*)__Instance)->m_hInteropDevice = value;
        }

        protected IntPtr MHInteropObject
        {
            get => ((SpoutGLDXinterop.__Internal*)__Instance)->m_hInteropObject;

            set => ((SpoutGLDXinterop.__Internal*)__Instance)->m_hInteropObject = value;
        }

        protected IntPtr MHAccessMutex
        {
            get => ((SpoutGLDXinterop.__Internal*)__Instance)->m_hAccessMutex;

            set => ((SpoutGLDXinterop.__Internal*)__Instance)->m_hAccessMutex = value;
        }

        public bool UnBindSharedTexture
        {
            get
            {
                bool __ret = __Internal.UnBindSharedTexture(__Instance);
                return __ret;
            }
        }

        public bool DX9
        {
            get
            {
                bool __ret = __Internal.IsDX9(__Instance);
                return __ret;
            }

            set => __Internal.SetDX9(__Instance, value);
        }

        public bool CPUmode
        {
            get
            {
                bool __ret = __Internal.GetCPUmode(__Instance);
                return __ret;
            }

            set => __Internal.SetCPUmode(__Instance, value);
        }

        public bool MemoryShareMode
        {
            get
            {
                bool __ret = __Internal.GetMemoryShareMode(__Instance);
                return __ret;
            }

            set => __Internal.SetMemoryShareMode(__Instance, value);
        }

        public int ShareMode
        {
            get
            {
                int __ret = __Internal.GetShareMode(__Instance);
                return __ret;
            }

            set => __Internal.SetShareMode(__Instance, value);
        }

        public bool IsBGRAavailable
        {
            get
            {
                bool __ret = __Internal.IsBGRAavailable(__Instance);
                return __ret;
            }
        }

        public bool IsPBOavailable
        {
            get
            {
                bool __ret = __Internal.IsPBOavailable(__Instance);
                return __ret;
            }
        }

        public bool BufferMode
        {
            get
            {
                bool __ret = __Internal.GetBufferMode(__Instance);
                return __ret;
            }

            set => __Internal.SetBufferMode(__Instance, value);
        }

        public int NumAdapters
        {
            get
            {
                int __ret = __Internal.GetNumAdapters(__Instance);
                return __ret;
            }
        }

        public int Adapter
        {
            get
            {
                int __ret = __Internal.GetAdapter(__Instance);
                return __ret;
            }

            set => __Internal.SetAdapter(__Instance, value);
        }

        public bool DX11available
        {
            get
            {
                bool __ret = __Internal.DX11available(__Instance);
                return __ret;
            }
        }

        public bool GLDXcompatible
        {
            get
            {
                bool __ret = __Internal.GLDXcompatible(__Instance);
                return __ret;
            }
        }

        public bool IsOptimus
        {
            get
            {
                bool __ret = __Internal.IsOptimus(__Instance);
                return __ret;
            }
        }

        public int VerticalSync
        {
            get
            {
                int __ret = __Internal.GetVerticalSync(__Instance);
                return __ret;
            }
        }

        public uint SpoutVersion
        {
            get
            {
                uint __ret = __Internal.GetSpoutVersion(__Instance);
                return __ret;
            }
        }

        public bool InitOpenGL
        {
            get
            {
                bool __ret = __Internal.InitOpenGL(__Instance);
                return __ret;
            }
        }

        public uint GLtextureID
        {
            get
            {
                uint __ret = __Internal.GetGLtextureID(__Instance);
                return __ret;
            }
        }
    }

    public unsafe partial class Spout : IDisposable
    {
        [StructLayout(LayoutKind.Explicit, Size = 1336)]
        public partial struct __Internal
        {
            [FieldOffset(0)]
            internal SpoutGLDXinterop.__Internal interop;

            [FieldOffset(664)]
            internal fixed sbyte g_SharedMemoryName[256];

            [FieldOffset(920)]
            internal fixed sbyte UserSenderName[256];

            [FieldOffset(1176)]
            internal uint g_Width;

            [FieldOffset(1180)]
            internal uint g_Height;

            [FieldOffset(1184)]
            internal IntPtr g_ShareHandle;

            [FieldOffset(1192)]
            internal uint g_Format;

            [FieldOffset(1196)]
            internal uint g_TexID;

            [FieldOffset(1200)]
            internal IntPtr g_hWnd;

            [FieldOffset(1208)]
            internal byte bGLDXcompatible;

            [FieldOffset(1209)]
            internal byte bMemoryShareInitOK;

            [FieldOffset(1210)]
            internal byte bDxInitOK;

            [FieldOffset(1211)]
            internal byte bUseCPU;

            [FieldOffset(1212)]
            internal byte bMemory;

            [FieldOffset(1213)]
            internal byte bInitialized;

            [FieldOffset(1214)]
            internal byte bIsSending;

            [FieldOffset(1215)]
            internal byte bIsReceiving;

            [FieldOffset(1216)]
            internal byte bChangeRequested;

            [FieldOffset(1217)]
            internal byte bSpoutPanelOpened;

            [FieldOffset(1218)]
            internal byte bSpoutPanelActive;

            [FieldOffset(1219)]
            internal byte bUseActive;

            [FieldOffset(1224)]
            internal SHELLEXECUTEINFOA.__Internal m_ShExecInfo;

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "??0Spout@@QEAA@XZ")]
            internal static extern IntPtr ctor(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "??0Spout@@QEAA@AEBV0@@Z")]
            internal static extern IntPtr cctor(IntPtr __instance, IntPtr _0);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "??1Spout@@QEAA@XZ")]
            internal static extern void dtor(IntPtr __instance, int delete);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?CreateSender@Spout@@QEAA_NPEBDIIK@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool CreateSender(IntPtr __instance, [MarshalAs(UnmanagedType.LPUTF8Str)] string Sendername, uint width, uint height, uint dwFormat);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?UpdateSender@Spout@@QEAA_NPEBDII@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool UpdateSender(IntPtr __instance, [MarshalAs(UnmanagedType.LPUTF8Str)] string Sendername, uint width, uint height);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?ReleaseSender@Spout@@QEAAXK@Z")]
            internal static extern void ReleaseSender(IntPtr __instance, uint dwMsec);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?CreateReceiver@Spout@@QEAA_NPEADAEAI1_N@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool CreateReceiver(IntPtr __instance, sbyte* Sendername, uint* width, uint* height, bool bUseActive);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?ReleaseReceiver@Spout@@QEAAXXZ")]
            internal static extern void ReleaseReceiver(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?CheckReceiver@Spout@@QEAA_NPEADAEAI1AEA_N@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool CheckReceiver(IntPtr __instance, sbyte* Sendername, uint* width, uint* height, bool* bConnected);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GetImageSize@Spout@@QEAA_NPEADAEAI1AEA_N@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool GetImageSize(IntPtr __instance, sbyte* sendername, uint* width, uint* height, bool* mMemoryMode);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?SendTexture@Spout@@QEAA_NIIII_NI@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool SendTexture(IntPtr __instance, uint TextureID, uint TextureTarget, uint width, uint height, bool bInvert, uint HostFBO);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?SendImage@Spout@@QEAA_NPEBEIII_NI@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool SendImage(IntPtr __instance, byte* pixels, uint width, uint height, uint glFormat, bool bInvert, uint HostFBO);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?ReceiveTexture@Spout@@QEAA_NPEADAEAI1II_NI@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool ReceiveTexture(IntPtr __instance, sbyte* Sendername, uint* width, uint* height, uint TextureID, uint TextureTarget, bool bInvert, uint HostFBO);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?ReceiveImage@Spout@@QEAA_NPEADAEAI1PEAEI_NI@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool ReceiveImage(IntPtr __instance, sbyte* Sendername, uint* width, uint* height, byte* pixels, uint glFormat, bool bInvert, uint HostFBO);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?DrawSharedTexture@Spout@@QEAA_NMMM_NI@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool DrawSharedTexture(IntPtr __instance, float max_x, float max_y, float aspect, bool bInvert, uint HostFBO);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?DrawToSharedTexture@Spout@@QEAA_NIIIIMMM_NI@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool DrawToSharedTexture(IntPtr __instance, uint TextureID, uint TextureTarget, uint width, uint height, float max_x, float max_y, float aspect, bool bInvert, uint HostFBO);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?BindSharedTexture@Spout@@QEAA_NXZ")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool BindSharedTexture(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GetSenderName@Spout@@QEAA_NHPEADH@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool GetSenderName(IntPtr __instance, int index, sbyte* sendername, int MaxSize);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GetSenderInfo@Spout@@QEAA_NPEBDAEAI1AEAPEAXAEAK@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool GetSenderInfo(IntPtr __instance, [MarshalAs(UnmanagedType.LPUTF8Str)] string sendername, uint* width, uint* height, void** dxShareHandle, uint* dwFormat);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GetActiveSender@Spout@@QEAA_NPEAD@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool GetActiveSender(IntPtr __instance, sbyte* Sendername);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?SetActiveSender@Spout@@QEAA_NPEBD@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool SetActiveSender(IntPtr __instance, [MarshalAs(UnmanagedType.LPUTF8Str)] string Sendername);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?SetDX9@Spout@@QEAA_N_N@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool SetDX9(IntPtr __instance, bool bDX9);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?SetMemoryShareMode@Spout@@QEAA_N_N@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool SetMemoryShareMode(IntPtr __instance, bool bMem);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?SetCPUmode@Spout@@QEAA_N_N@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool SetCPUmode(IntPtr __instance, bool bCPU);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?SetShareMode@Spout@@QEAA_NH@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool SetShareMode(IntPtr __instance, int mode);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GetSpoutSenderName@Spout@@QEAA_NPEADH@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool GetSpoutSenderName(IntPtr __instance, sbyte* sendername, int maxchars);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GetAdapterName@Spout@@QEAA_NHPEADH@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool GetAdapterName(IntPtr __instance, int index, sbyte* adaptername, int maxchars);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?SetAdapter@Spout@@QEAA_NH@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool SetAdapter(IntPtr __instance, int index);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GetHostPath@Spout@@QEAA_NPEBDPEADH@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool GetHostPath(IntPtr __instance, [MarshalAs(UnmanagedType.LPUTF8Str)] string sendername, sbyte* hostpath, int maxchars);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?SetVerticalSync@Spout@@QEAA_N_N@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool SetVerticalSync(IntPtr __instance, bool bSync);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?SelectSenderPanel@Spout@@QEAA_NPEBD@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool SelectSenderPanel(IntPtr __instance, [MarshalAs(UnmanagedType.LPUTF8Str)] string message);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?CheckSpoutPanel@Spout@@QEAA_NXZ")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool CheckSpoutPanel(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?OpenSpout@Spout@@QEAA_NXZ")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool OpenSpout(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?WritePathToRegistry@Spout@@QEAA_NPEBD00@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool WritePathToRegistry(IntPtr __instance, [MarshalAs(UnmanagedType.LPUTF8Str)] string filepath, [MarshalAs(UnmanagedType.LPUTF8Str)] string subkey, [MarshalAs(UnmanagedType.LPUTF8Str)] string valuename);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?ReadPathFromRegistry@Spout@@QEAA_NPEADPEBD1@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool ReadPathFromRegistry(IntPtr __instance, sbyte* filepath, [MarshalAs(UnmanagedType.LPUTF8Str)] string subkey, [MarshalAs(UnmanagedType.LPUTF8Str)] string valuename);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?RemovePathFromRegistry@Spout@@QEAA_NPEBD0@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool RemovePathFromRegistry(IntPtr __instance, [MarshalAs(UnmanagedType.LPUTF8Str)] string subkey, [MarshalAs(UnmanagedType.LPUTF8Str)] string valuename);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?UseAccessLocks@Spout@@QEAAX_N@Z")]
            internal static extern void UseAccessLocks(IntPtr __instance, bool bUseLocks);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?SpoutCleanUp@Spout@@QEAAX_N@Z")]
            internal static extern void SpoutCleanUp(IntPtr __instance, bool bExit);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?CleanSenders@Spout@@QEAAXXZ")]
            internal static extern void CleanSenders(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?OpenReceiver@Spout@@IEAA_NPEADAEAI1@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool OpenReceiver(IntPtr __instance, sbyte* name, uint* width, uint* height);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?InitMemoryShare@Spout@@IEAA_N_N@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool InitMemoryShare(IntPtr __instance, bool bReceiver);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?FindFileVersion@Spout@@IEAA_NPEBDAEAK1@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool FindFileVersion(IntPtr __instance, [MarshalAs(UnmanagedType.LPUTF8Str)] string filepath, uint* versMS, uint* versLS);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?UnBindSharedTexture@Spout@@QEAA_NXZ")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool UnBindSharedTexture(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GetSenderCount@Spout@@QEAAHXZ")]
            internal static extern int GetSenderCount(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GetDX9@Spout@@QEAA_NXZ")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool GetDX9(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GetMemoryShareMode@Spout@@QEAA_NXZ")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool GetMemoryShareMode(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GetCPUmode@Spout@@QEAA_NXZ")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool GetCPUmode(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GetShareMode@Spout@@QEAAHXZ")]
            internal static extern int GetShareMode(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GetMaxSenders@Spout@@QEAAHXZ")]
            internal static extern int GetMaxSenders(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?SetMaxSenders@Spout@@QEAAXH@Z")]
            internal static extern void SetMaxSenders(IntPtr __instance, int maxSenders);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?IsSpoutInitialized@Spout@@QEAA_NXZ")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool IsSpoutInitialized(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?IsBGRAavailable@Spout@@QEAA_NXZ")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool IsBGRAavailable(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?IsPBOavailable@Spout@@QEAA_NXZ")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool IsPBOavailable(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GetBufferMode@Spout@@QEAA_NXZ")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool GetBufferMode(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?SetBufferMode@Spout@@QEAAX_N@Z")]
            internal static extern void SetBufferMode(IntPtr __instance, bool bActive);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GetNumAdapters@Spout@@QEAAHXZ")]
            internal static extern int GetNumAdapters(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GetAdapter@Spout@@QEAAHXZ")]
            internal static extern int GetAdapter(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GetVerticalSync@Spout@@QEAAHXZ")]
            internal static extern int GetVerticalSync(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?ReportMemory@Spout@@QEAAHXZ")]
            internal static extern int ReportMemory(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GLDXcompatible@Spout@@IEAA_NXZ")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool GLDXcompatible(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?ReleaseMemoryShare@Spout@@IEAA_NXZ")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool ReleaseMemoryShare(IntPtr __instance);
        }

        public IntPtr __Instance { get; protected set; }

        internal static readonly System.Collections.Concurrent.ConcurrentDictionary<IntPtr, Spout> NativeToManagedMap = new();

        protected bool __ownsNativeInstance;

        internal static Spout __CreateInstance(IntPtr native, bool skipVTables = false)
        {
            return new Spout(native.ToPointer(), skipVTables);
        }

        internal static Spout __CreateInstance(Spout.__Internal native, bool skipVTables = false)
        {
            return new Spout(native, skipVTables);
        }

        private static void* __CopyValue(Spout.__Internal native)
        {
            IntPtr ret = Marshal.AllocHGlobal(sizeof(Spout.__Internal));
            *(Spout.__Internal*)ret = native;
            return ret.ToPointer();
        }

        private Spout(Spout.__Internal native, bool skipVTables = false)
            : this(__CopyValue(native), skipVTables)
        {
            __ownsNativeInstance = true;
            NativeToManagedMap[__Instance] = this;
        }

        protected Spout(void* native, bool skipVTables = false)
        {
            if (native == null)
            {
                return;
            }

            __Instance = new IntPtr(native);
        }

        public Spout()
        {
            __Instance = Marshal.AllocHGlobal(sizeof(Spout.__Internal));
            __ownsNativeInstance = true;
            NativeToManagedMap[__Instance] = this;
            _ = __Internal.ctor(__Instance);
        }

        public Spout(Spout _0)
        {
            __Instance = Marshal.AllocHGlobal(sizeof(Spout.__Internal));
            __ownsNativeInstance = true;
            NativeToManagedMap[__Instance] = this;
            *(Spout.__Internal*)__Instance = *(Spout.__Internal*)_0.__Instance;
        }

        ~Spout()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (__Instance == IntPtr.Zero)
            {
                return;
            }

            _ = NativeToManagedMap.TryRemove(__Instance, out _);
            if (disposing)
            {
                __Internal.dtor(__Instance, 0);
            }

            if (__ownsNativeInstance)
            {
                Marshal.FreeHGlobal(__Instance);
            }

            __Instance = IntPtr.Zero;
        }

        public bool CreateSender(string Sendername, uint width, uint height, uint dwFormat)
        {
            bool __ret = __Internal.CreateSender(__Instance, Sendername, width, height, dwFormat);
            return __ret;
        }

        public bool UpdateSender(string Sendername, uint width, uint height)
        {
            bool __ret = __Internal.UpdateSender(__Instance, Sendername, width, height);
            return __ret;
        }

        public void ReleaseSender(uint dwMsec)
        {
            __Internal.ReleaseSender(__Instance, dwMsec);
        }

        public bool CreateReceiver(sbyte* Sendername, ref uint width, ref uint height, bool bUseActive)
        {
            fixed (uint* __width1 = &width)
            {
                uint* __arg1 = __width1;
                fixed (uint* __height2 = &height)
                {
                    uint* __arg2 = __height2;
                    bool __ret = __Internal.CreateReceiver(__Instance, Sendername, __arg1, __arg2, bUseActive);
                    return __ret;
                }
            }
        }

        public void ReleaseReceiver()
        {
            __Internal.ReleaseReceiver(__Instance);
        }

        public bool CheckReceiver(sbyte* Sendername, ref uint width, ref uint height, ref bool bConnected)
        {
            fixed (uint* __width1 = &width)
            {
                uint* __arg1 = __width1;
                fixed (uint* __height2 = &height)
                {
                    uint* __arg2 = __height2;
                    fixed (bool* __bConnected3 = &bConnected)
                    {
                        bool* __arg3 = __bConnected3;
                        bool __ret = __Internal.CheckReceiver(__Instance, Sendername, __arg1, __arg2, __arg3);
                        return __ret;
                    }
                }
            }
        }

        public bool GetImageSize(sbyte* sendername, ref uint width, ref uint height, ref bool mMemoryMode)
        {
            fixed (uint* __width1 = &width)
            {
                uint* __arg1 = __width1;
                fixed (uint* __height2 = &height)
                {
                    uint* __arg2 = __height2;
                    fixed (bool* __mMemoryMode3 = &mMemoryMode)
                    {
                        bool* __arg3 = __mMemoryMode3;
                        bool __ret = __Internal.GetImageSize(__Instance, sendername, __arg1, __arg2, __arg3);
                        return __ret;
                    }
                }
            }
        }

        public bool SendTexture(uint TextureID, uint TextureTarget, uint width, uint height, bool bInvert, uint HostFBO)
        {
            bool __ret = __Internal.SendTexture(__Instance, TextureID, TextureTarget, width, height, bInvert, HostFBO);
            return __ret;
        }

        public bool SendImage(byte* pixels, uint width, uint height, uint glFormat, bool bInvert, uint HostFBO)
        {
            bool __ret = __Internal.SendImage(__Instance, pixels, width, height, glFormat, bInvert, HostFBO);
            return __ret;
        }

        public bool ReceiveTexture(sbyte* Sendername, ref uint width, ref uint height, uint TextureID, uint TextureTarget, bool bInvert, uint HostFBO)
        {
            fixed (uint* __width1 = &width)
            {
                uint* __arg1 = __width1;
                fixed (uint* __height2 = &height)
                {
                    uint* __arg2 = __height2;
                    bool __ret = __Internal.ReceiveTexture(__Instance, Sendername, __arg1, __arg2, TextureID, TextureTarget, bInvert, HostFBO);
                    return __ret;
                }
            }
        }

        public bool ReceiveImage(sbyte* Sendername, ref uint width, ref uint height, byte* pixels, uint glFormat, bool bInvert, uint HostFBO)
        {
            fixed (uint* __width1 = &width)
            {
                uint* __arg1 = __width1;
                fixed (uint* __height2 = &height)
                {
                    uint* __arg2 = __height2;
                    bool __ret = __Internal.ReceiveImage(__Instance, Sendername, __arg1, __arg2, pixels, glFormat, bInvert, HostFBO);
                    return __ret;
                }
            }
        }

        public bool DrawSharedTexture(float max_x, float max_y, float aspect, bool bInvert, uint HostFBO)
        {
            bool __ret = __Internal.DrawSharedTexture(__Instance, max_x, max_y, aspect, bInvert, HostFBO);
            return __ret;
        }

        public bool DrawToSharedTexture(uint TextureID, uint TextureTarget, uint width, uint height, float max_x, float max_y, float aspect, bool bInvert, uint HostFBO)
        {
            bool __ret = __Internal.DrawToSharedTexture(__Instance, TextureID, TextureTarget, width, height, max_x, max_y, aspect, bInvert, HostFBO);
            return __ret;
        }

        public bool BindSharedTexture()
        {
            bool __ret = __Internal.BindSharedTexture(__Instance);
            return __ret;
        }

        public bool GetSenderName(int index, sbyte* sendername, int MaxSize)
        {
            bool __ret = __Internal.GetSenderName(__Instance, index, sendername, MaxSize);
            return __ret;
        }

        public bool GetSenderInfo(string sendername, ref uint width, ref uint height, void** dxShareHandle, ref uint dwFormat)
        {
            fixed (uint* __width1 = &width)
            {
                uint* __arg1 = __width1;
                fixed (uint* __height2 = &height)
                {
                    uint* __arg2 = __height2;
                    fixed (uint* __dwFormat4 = &dwFormat)
                    {
                        uint* __arg4 = __dwFormat4;
                        bool __ret = __Internal.GetSenderInfo(__Instance, sendername, __arg1, __arg2, dxShareHandle, __arg4);
                        return __ret;
                    }
                }
            }
        }

        public bool GetActiveSender(sbyte* Sendername)
        {
            bool __ret = __Internal.GetActiveSender(__Instance, Sendername);
            return __ret;
        }

        public bool SetActiveSender(string Sendername)
        {
            bool __ret = __Internal.SetActiveSender(__Instance, Sendername);
            return __ret;
        }

        public bool SetDX9(bool bDX9)
        {
            bool __ret = __Internal.SetDX9(__Instance, bDX9);
            return __ret;
        }

        public bool SetMemoryShareMode(bool bMem)
        {
            bool __ret = __Internal.SetMemoryShareMode(__Instance, bMem);
            return __ret;
        }

        public bool SetCPUmode(bool bCPU)
        {
            bool __ret = __Internal.SetCPUmode(__Instance, bCPU);
            return __ret;
        }

        public bool SetShareMode(int mode)
        {
            bool __ret = __Internal.SetShareMode(__Instance, mode);
            return __ret;
        }

        public bool GetSpoutSenderName(sbyte* sendername, int maxchars)
        {
            bool __ret = __Internal.GetSpoutSenderName(__Instance, sendername, maxchars);
            return __ret;
        }

        public bool GetAdapterName(int index, sbyte* adaptername, int maxchars)
        {
            bool __ret = __Internal.GetAdapterName(__Instance, index, adaptername, maxchars);
            return __ret;
        }

        public bool SetAdapter(int index)
        {
            bool __ret = __Internal.SetAdapter(__Instance, index);
            return __ret;
        }

        public bool GetHostPath(string sendername, sbyte* hostpath, int maxchars)
        {
            bool __ret = __Internal.GetHostPath(__Instance, sendername, hostpath, maxchars);
            return __ret;
        }

        public bool SetVerticalSync(bool bSync)
        {
            bool __ret = __Internal.SetVerticalSync(__Instance, bSync);
            return __ret;
        }

        public bool SelectSenderPanel(string message)
        {
            bool __ret = __Internal.SelectSenderPanel(__Instance, message);
            return __ret;
        }

        public bool CheckSpoutPanel()
        {
            bool __ret = __Internal.CheckSpoutPanel(__Instance);
            return __ret;
        }

        public bool OpenSpout()
        {
            bool __ret = __Internal.OpenSpout(__Instance);
            return __ret;
        }

        public bool WritePathToRegistry(string filepath, string subkey, string valuename)
        {
            bool __ret = __Internal.WritePathToRegistry(__Instance, filepath, subkey, valuename);
            return __ret;
        }

        public bool ReadPathFromRegistry(sbyte* filepath, string subkey, string valuename)
        {
            bool __ret = __Internal.ReadPathFromRegistry(__Instance, filepath, subkey, valuename);
            return __ret;
        }

        public bool RemovePathFromRegistry(string subkey, string valuename)
        {
            bool __ret = __Internal.RemovePathFromRegistry(__Instance, subkey, valuename);
            return __ret;
        }

        public void UseAccessLocks(bool bUseLocks)
        {
            __Internal.UseAccessLocks(__Instance, bUseLocks);
        }

        public void SpoutCleanUp(bool bExit)
        {
            __Internal.SpoutCleanUp(__Instance, bExit);
        }

        public void CleanSenders()
        {
            __Internal.CleanSenders(__Instance);
        }

        protected bool OpenReceiver(sbyte* name, ref uint width, ref uint height)
        {
            fixed (uint* __width1 = &width)
            {
                uint* __arg1 = __width1;
                fixed (uint* __height2 = &height)
                {
                    uint* __arg2 = __height2;
                    bool __ret = __Internal.OpenReceiver(__Instance, name, __arg1, __arg2);
                    return __ret;
                }
            }
        }

        protected bool InitMemoryShare(bool bReceiver)
        {
            bool __ret = __Internal.InitMemoryShare(__Instance, bReceiver);
            return __ret;
        }

        protected bool FindFileVersion(string filepath, ref uint versMS, ref uint versLS)
        {
            fixed (uint* __versMS1 = &versMS)
            {
                uint* __arg1 = __versMS1;
                fixed (uint* __versLS2 = &versLS)
                {
                    uint* __arg2 = __versLS2;
                    bool __ret = __Internal.FindFileVersion(__Instance, filepath, __arg1, __arg2);
                    return __ret;
                }
            }
        }

        public SpoutGLDXinterop Interop
        {
            get => SpoutGLDXinterop.__CreateInstance(new IntPtr(&((Spout.__Internal*)__Instance)->interop));

            set
            {
                if (value is null)
                {
                    throw new ArgumentNullException("value", "Cannot be null because it is passed by value.");
                } ((Spout.__Internal*)__Instance)->interop = *(SpoutGLDXinterop.__Internal*)value.__Instance;
            }
        }

        protected sbyte[] GSharedMemoryName
        {
            get
            {
                sbyte[] __value = null;
                if (((Spout.__Internal*)__Instance)->g_SharedMemoryName != null)
                {
                    __value = new sbyte[256];
                    for (int i = 0; i < 256; i++)
                    {
                        __value[i] = ((Spout.__Internal*)__Instance)->g_SharedMemoryName[i];
                    }
                }
                return __value;
            }

            set
            {
                if (value != null)
                {
                    for (int i = 0; i < 256; i++)
                    {
                        ((Spout.__Internal*)__Instance)->g_SharedMemoryName[i] = value[i];
                    }
                }
            }
        }

        protected sbyte[] UserSenderName
        {
            get
            {
                sbyte[] __value = null;
                if (((Spout.__Internal*)__Instance)->UserSenderName != null)
                {
                    __value = new sbyte[256];
                    for (int i = 0; i < 256; i++)
                    {
                        __value[i] = ((Spout.__Internal*)__Instance)->UserSenderName[i];
                    }
                }
                return __value;
            }

            set
            {
                if (value != null)
                {
                    for (int i = 0; i < 256; i++)
                    {
                        ((Spout.__Internal*)__Instance)->UserSenderName[i] = value[i];
                    }
                }
            }
        }

        protected uint GWidth
        {
            get => ((Spout.__Internal*)__Instance)->g_Width;

            set => ((Spout.__Internal*)__Instance)->g_Width = value;
        }

        protected uint GHeight
        {
            get => ((Spout.__Internal*)__Instance)->g_Height;

            set => ((Spout.__Internal*)__Instance)->g_Height = value;
        }

        protected IntPtr GShareHandle
        {
            get => ((Spout.__Internal*)__Instance)->g_ShareHandle;

            set => ((Spout.__Internal*)__Instance)->g_ShareHandle = value;
        }

        protected uint GFormat
        {
            get => ((Spout.__Internal*)__Instance)->g_Format;

            set => ((Spout.__Internal*)__Instance)->g_Format = value;
        }

        protected uint GTexID
        {
            get => ((Spout.__Internal*)__Instance)->g_TexID;

            set => ((Spout.__Internal*)__Instance)->g_TexID = value;
        }

        protected bool BGLDXcompatible
        {
            get => ((Spout.__Internal*)__Instance)->bGLDXcompatible != 0;

            set => ((Spout.__Internal*)__Instance)->bGLDXcompatible = (byte)(value ? 1 : 0);
        }

        protected bool BMemoryShareInitOK
        {
            get => ((Spout.__Internal*)__Instance)->bMemoryShareInitOK != 0;

            set => ((Spout.__Internal*)__Instance)->bMemoryShareInitOK = (byte)(value ? 1 : 0);
        }

        protected bool BDxInitOK
        {
            get => ((Spout.__Internal*)__Instance)->bDxInitOK != 0;

            set => ((Spout.__Internal*)__Instance)->bDxInitOK = (byte)(value ? 1 : 0);
        }

        protected bool BUseCPU
        {
            get => ((Spout.__Internal*)__Instance)->bUseCPU != 0;

            set => ((Spout.__Internal*)__Instance)->bUseCPU = (byte)(value ? 1 : 0);
        }

        protected bool BMemory
        {
            get => ((Spout.__Internal*)__Instance)->bMemory != 0;

            set => ((Spout.__Internal*)__Instance)->bMemory = (byte)(value ? 1 : 0);
        }

        protected bool BInitialized
        {
            get => ((Spout.__Internal*)__Instance)->bInitialized != 0;

            set => ((Spout.__Internal*)__Instance)->bInitialized = (byte)(value ? 1 : 0);
        }

        protected bool BIsSending
        {
            get => ((Spout.__Internal*)__Instance)->bIsSending != 0;

            set => ((Spout.__Internal*)__Instance)->bIsSending = (byte)(value ? 1 : 0);
        }

        protected bool BIsReceiving
        {
            get => ((Spout.__Internal*)__Instance)->bIsReceiving != 0;

            set => ((Spout.__Internal*)__Instance)->bIsReceiving = (byte)(value ? 1 : 0);
        }

        protected bool BChangeRequested
        {
            get => ((Spout.__Internal*)__Instance)->bChangeRequested != 0;

            set => ((Spout.__Internal*)__Instance)->bChangeRequested = (byte)(value ? 1 : 0);
        }

        protected bool BSpoutPanelOpened
        {
            get => ((Spout.__Internal*)__Instance)->bSpoutPanelOpened != 0;

            set => ((Spout.__Internal*)__Instance)->bSpoutPanelOpened = (byte)(value ? 1 : 0);
        }

        protected bool BSpoutPanelActive
        {
            get => ((Spout.__Internal*)__Instance)->bSpoutPanelActive != 0;

            set => ((Spout.__Internal*)__Instance)->bSpoutPanelActive = (byte)(value ? 1 : 0);
        }

        protected bool BUseActive
        {
            get => ((Spout.__Internal*)__Instance)->bUseActive != 0;

            set => ((Spout.__Internal*)__Instance)->bUseActive = (byte)(value ? 1 : 0);
        }

        public bool UnBindSharedTexture
        {
            get
            {
                bool __ret = __Internal.UnBindSharedTexture(__Instance);
                return __ret;
            }
        }

        public int SenderCount
        {
            get
            {
                int __ret = __Internal.GetSenderCount(__Instance);
                return __ret;
            }
        }

        public bool DX9
        {
            get
            {
                bool __ret = __Internal.GetDX9(__Instance);
                return __ret;
            }

            set => __Internal.SetDX9(__Instance, value);
        }

        public bool MemoryShareMode
        {
            get
            {
                bool __ret = __Internal.GetMemoryShareMode(__Instance);
                return __ret;
            }

            set => __Internal.SetMemoryShareMode(__Instance, value);
        }

        public bool CPUmode
        {
            get
            {
                bool __ret = __Internal.GetCPUmode(__Instance);
                return __ret;
            }

            set => __Internal.SetCPUmode(__Instance, value);
        }

        public int ShareMode
        {
            get
            {
                int __ret = __Internal.GetShareMode(__Instance);
                return __ret;
            }

            set => __Internal.SetShareMode(__Instance, value);
        }

        public int MaxSenders
        {
            get
            {
                int __ret = __Internal.GetMaxSenders(__Instance);
                return __ret;
            }

            set => __Internal.SetMaxSenders(__Instance, value);
        }

        public bool IsSpoutInitialized
        {
            get
            {
                bool __ret = __Internal.IsSpoutInitialized(__Instance);
                return __ret;
            }
        }

        public bool IsBGRAavailable
        {
            get
            {
                bool __ret = __Internal.IsBGRAavailable(__Instance);
                return __ret;
            }
        }

        public bool IsPBOavailable
        {
            get
            {
                bool __ret = __Internal.IsPBOavailable(__Instance);
                return __ret;
            }
        }

        public bool BufferMode
        {
            get
            {
                bool __ret = __Internal.GetBufferMode(__Instance);
                return __ret;
            }

            set => __Internal.SetBufferMode(__Instance, value);
        }

        public int NumAdapters
        {
            get
            {
                int __ret = __Internal.GetNumAdapters(__Instance);
                return __ret;
            }
        }

        public int Adapter
        {
            get
            {
                int __ret = __Internal.GetAdapter(__Instance);
                return __ret;
            }

            set => __Internal.SetAdapter(__Instance, value);
        }

        public int VerticalSync
        {
            get
            {
                int __ret = __Internal.GetVerticalSync(__Instance);
                return __ret;
            }
        }

        public int ReportMemory
        {
            get
            {
                int __ret = __Internal.ReportMemory(__Instance);
                return __ret;
            }
        }

        protected bool GLDXcompatible
        {
            get
            {
                bool __ret = __Internal.GLDXcompatible(__Instance);
                return __ret;
            }
        }

        protected bool ReleaseMemoryShare
        {
            get
            {
                bool __ret = __Internal.ReleaseMemoryShare(__Instance);
                return __ret;
            }
        }
    }

    public unsafe partial class SpoutSender : IDisposable
    {
        [StructLayout(LayoutKind.Explicit, Size = 1336)]
        public partial struct __Internal
        {
            [FieldOffset(0)]
            internal Spout.__Internal spout;

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "??0SpoutSender@@QEAA@XZ")]
            internal static extern IntPtr ctor(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "??0SpoutSender@@QEAA@AEBV0@@Z")]
            internal static extern IntPtr cctor(IntPtr __instance, IntPtr _0);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "??1SpoutSender@@QEAA@XZ")]
            internal static extern void dtor(IntPtr __instance, int delete);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?CreateSender@SpoutSender@@QEAA_NPEBDIIK@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool CreateSender(IntPtr __instance, [MarshalAs(UnmanagedType.LPUTF8Str)] string Sendername, uint width, uint height, uint dwFormat);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?UpdateSender@SpoutSender@@QEAA_NPEBDII@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool UpdateSender(IntPtr __instance, [MarshalAs(UnmanagedType.LPUTF8Str)] string Sendername, uint width, uint height);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?ReleaseSender@SpoutSender@@QEAAXK@Z")]
            internal static extern void ReleaseSender(IntPtr __instance, uint dwMsec);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?SendImage@SpoutSender@@QEAA_NPEBEIII_NI@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool SendImage(IntPtr __instance, byte* pixels, uint width, uint height, uint glFormat, bool bInvert, uint HostFBO);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?SendTexture@SpoutSender@@QEAA_NIIII_NI@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool SendTexture(IntPtr __instance, uint TextureID, uint TextureTarget, uint width, uint height, bool bInvert, uint HostFBO);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?DrawToSharedTexture@SpoutSender@@QEAA_NIIIIMMM_NI@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool DrawToSharedTexture(IntPtr __instance, uint TextureID, uint TextureTarget, uint width, uint height, float max_x, float max_y, float aspect, bool bInvert, uint HostFBO);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?SelectSenderPanel@SpoutSender@@QEAA_NPEBD@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool SelectSenderPanel(IntPtr __instance, [MarshalAs(UnmanagedType.LPUTF8Str)] string message);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?SetDX9@SpoutSender@@QEAA_N_N@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool SetDX9(IntPtr __instance, bool bDX9);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?SetMemoryShareMode@SpoutSender@@QEAA_N_N@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool SetMemoryShareMode(IntPtr __instance, bool bMem);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?SetCPUmode@SpoutSender@@QEAA_N_N@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool SetCPUmode(IntPtr __instance, bool bCPU);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?SetShareMode@SpoutSender@@QEAA_NH@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool SetShareMode(IntPtr __instance, int mode);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GetAdapterName@SpoutSender@@QEAA_NHPEADH@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool GetAdapterName(IntPtr __instance, int index, sbyte* adaptername, int maxchars);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?SetAdapter@SpoutSender@@QEAA_NH@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool SetAdapter(IntPtr __instance, int index);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GetHostPath@SpoutSender@@QEAA_NPEBDPEADH@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool GetHostPath(IntPtr __instance, [MarshalAs(UnmanagedType.LPUTF8Str)] string sendername, sbyte* hostpath, int maxchars);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?SetVerticalSync@SpoutSender@@QEAA_N_N@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool SetVerticalSync(IntPtr __instance, bool bSync);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?SenderDebug@SpoutSender@@QEAA_NPEADH@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool SenderDebug(IntPtr __instance, sbyte* Sendername, int size);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GetDX9@SpoutSender@@QEAA_NXZ")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool GetDX9(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GetMemoryShareMode@SpoutSender@@QEAA_NXZ")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool GetMemoryShareMode(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GetCPUmode@SpoutSender@@QEAA_NXZ")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool GetCPUmode(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GetShareMode@SpoutSender@@QEAAHXZ")]
            internal static extern int GetShareMode(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GetBufferMode@SpoutSender@@QEAA_NXZ")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool GetBufferMode(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?SetBufferMode@SpoutSender@@QEAAX_N@Z")]
            internal static extern void SetBufferMode(IntPtr __instance, bool bActive);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GetDX9compatible@SpoutSender@@QEAA_NXZ")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool GetDX9compatible(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?SetDX9compatible@SpoutSender@@QEAAX_N@Z")]
            internal static extern void SetDX9compatible(IntPtr __instance, bool bCompatible);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GetNumAdapters@SpoutSender@@QEAAHXZ")]
            internal static extern int GetNumAdapters(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GetAdapter@SpoutSender@@QEAAHXZ")]
            internal static extern int GetAdapter(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GetVerticalSync@SpoutSender@@QEAAHXZ")]
            internal static extern int GetVerticalSync(IntPtr __instance);
        }

        public IntPtr __Instance { get; protected set; }

        internal static readonly System.Collections.Concurrent.ConcurrentDictionary<IntPtr, SpoutSender> NativeToManagedMap = new();

        protected bool __ownsNativeInstance;

        internal static SpoutSender __CreateInstance(IntPtr native, bool skipVTables = false)
        {
            return new SpoutSender(native.ToPointer(), skipVTables);
        }

        internal static SpoutSender __CreateInstance(SpoutSender.__Internal native, bool skipVTables = false)
        {
            return new SpoutSender(native, skipVTables);
        }

        private static void* __CopyValue(SpoutSender.__Internal native)
        {
            IntPtr ret = Marshal.AllocHGlobal(sizeof(SpoutSender.__Internal));
            *(SpoutSender.__Internal*)ret = native;
            return ret.ToPointer();
        }

        private SpoutSender(SpoutSender.__Internal native, bool skipVTables = false)
            : this(__CopyValue(native), skipVTables)
        {
            __ownsNativeInstance = true;
            NativeToManagedMap[__Instance] = this;
        }

        protected SpoutSender(void* native, bool skipVTables = false)
        {
            if (native == null)
            {
                return;
            }

            __Instance = new IntPtr(native);
        }

        public SpoutSender()
        {
            __Instance = Marshal.AllocHGlobal(sizeof(SpoutSender.__Internal));
            __ownsNativeInstance = true;
            NativeToManagedMap[__Instance] = this;
            _ = __Internal.ctor(__Instance);
        }

        public SpoutSender(SpoutSender _0)
        {
            __Instance = Marshal.AllocHGlobal(sizeof(SpoutSender.__Internal));
            __ownsNativeInstance = true;
            NativeToManagedMap[__Instance] = this;
            *(SpoutSender.__Internal*)__Instance = *(SpoutSender.__Internal*)_0.__Instance;
        }

        ~SpoutSender()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (__Instance == IntPtr.Zero)
            {
                return;
            }

            _ = NativeToManagedMap.TryRemove(__Instance, out _);
            if (disposing)
            {
                __Internal.dtor(__Instance, 0);
            }

            if (__ownsNativeInstance)
            {
                Marshal.FreeHGlobal(__Instance);
            }

            __Instance = IntPtr.Zero;
        }

        public bool CreateSender(string Sendername, uint width, uint height, uint dwFormat)
        {
            bool __ret = __Internal.CreateSender(__Instance, Sendername, width, height, dwFormat);
            return __ret;
        }

        public bool UpdateSender(string Sendername, uint width, uint height)
        {
            bool __ret = __Internal.UpdateSender(__Instance, Sendername, width, height);
            return __ret;
        }

        public void ReleaseSender(uint dwMsec)
        {
            __Internal.ReleaseSender(__Instance, dwMsec);
        }

        public bool SendImage(byte* pixels, uint width, uint height, uint glFormat, bool bInvert, uint HostFBO)
        {
            bool __ret = __Internal.SendImage(__Instance, pixels, width, height, glFormat, bInvert, HostFBO);
            return __ret;
        }

        public bool SendTexture(uint TextureID, uint TextureTarget, uint width, uint height, bool bInvert, uint HostFBO)
        {
            bool __ret = __Internal.SendTexture(__Instance, TextureID, TextureTarget, width, height, bInvert, HostFBO);
            return __ret;
        }

        public bool DrawToSharedTexture(uint TextureID, uint TextureTarget, uint width, uint height, float max_x, float max_y, float aspect, bool bInvert, uint HostFBO)
        {
            bool __ret = __Internal.DrawToSharedTexture(__Instance, TextureID, TextureTarget, width, height, max_x, max_y, aspect, bInvert, HostFBO);
            return __ret;
        }

        public bool SelectSenderPanel(string message)
        {
            bool __ret = __Internal.SelectSenderPanel(__Instance, message);
            return __ret;
        }

        public bool SetDX9(bool bDX9)
        {
            bool __ret = __Internal.SetDX9(__Instance, bDX9);
            return __ret;
        }

        public bool SetMemoryShareMode(bool bMem)
        {
            bool __ret = __Internal.SetMemoryShareMode(__Instance, bMem);
            return __ret;
        }

        public bool SetCPUmode(bool bCPU)
        {
            bool __ret = __Internal.SetCPUmode(__Instance, bCPU);
            return __ret;
        }

        public bool SetShareMode(int mode)
        {
            bool __ret = __Internal.SetShareMode(__Instance, mode);
            return __ret;
        }

        public bool GetAdapterName(int index, sbyte* adaptername, int maxchars)
        {
            bool __ret = __Internal.GetAdapterName(__Instance, index, adaptername, maxchars);
            return __ret;
        }

        public bool SetAdapter(int index)
        {
            bool __ret = __Internal.SetAdapter(__Instance, index);
            return __ret;
        }

        public bool GetHostPath(string sendername, sbyte* hostpath, int maxchars)
        {
            bool __ret = __Internal.GetHostPath(__Instance, sendername, hostpath, maxchars);
            return __ret;
        }

        public bool SetVerticalSync(bool bSync)
        {
            bool __ret = __Internal.SetVerticalSync(__Instance, bSync);
            return __ret;
        }

        public bool SenderDebug(sbyte* Sendername, int size)
        {
            bool __ret = __Internal.SenderDebug(__Instance, Sendername, size);
            return __ret;
        }

        public Spout Spout
        {
            get => Spout.__CreateInstance(new IntPtr(&((SpoutSender.__Internal*)__Instance)->spout));

            set
            {
                if (value is null)
                {
                    throw new ArgumentNullException("value", "Cannot be null because it is passed by value.");
                } ((SpoutSender.__Internal*)__Instance)->spout = *(Spout.__Internal*)value.__Instance;
            }
        }

        public bool DX9
        {
            get
            {
                bool __ret = __Internal.GetDX9(__Instance);
                return __ret;
            }

            set => __Internal.SetDX9(__Instance, value);
        }

        public bool MemoryShareMode
        {
            get
            {
                bool __ret = __Internal.GetMemoryShareMode(__Instance);
                return __ret;
            }

            set => __Internal.SetMemoryShareMode(__Instance, value);
        }

        public bool CPUmode
        {
            get
            {
                bool __ret = __Internal.GetCPUmode(__Instance);
                return __ret;
            }

            set => __Internal.SetCPUmode(__Instance, value);
        }

        public int ShareMode
        {
            get
            {
                int __ret = __Internal.GetShareMode(__Instance);
                return __ret;
            }

            set => __Internal.SetShareMode(__Instance, value);
        }

        public bool BufferMode
        {
            get
            {
                bool __ret = __Internal.GetBufferMode(__Instance);
                return __ret;
            }

            set => __Internal.SetBufferMode(__Instance, value);
        }

        public bool DX9compatible
        {
            get
            {
                bool __ret = __Internal.GetDX9compatible(__Instance);
                return __ret;
            }

            set => __Internal.SetDX9compatible(__Instance, value);
        }

        public int NumAdapters
        {
            get
            {
                int __ret = __Internal.GetNumAdapters(__Instance);
                return __ret;
            }
        }

        public int Adapter
        {
            get
            {
                int __ret = __Internal.GetAdapter(__Instance);
                return __ret;
            }

            set => __Internal.SetAdapter(__Instance, value);
        }

        public int VerticalSync
        {
            get
            {
                int __ret = __Internal.GetVerticalSync(__Instance);
                return __ret;
            }
        }
    }

    public unsafe partial class SpoutReceiver : IDisposable
    {
        [StructLayout(LayoutKind.Explicit, Size = 1336)]
        public partial struct __Internal
        {
            [FieldOffset(0)]
            internal Spout.__Internal spout;

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "??0SpoutReceiver@@QEAA@XZ")]
            internal static extern IntPtr ctor(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "??0SpoutReceiver@@QEAA@AEBV0@@Z")]
            internal static extern IntPtr cctor(IntPtr __instance, IntPtr _0);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "??1SpoutReceiver@@QEAA@XZ")]
            internal static extern void dtor(IntPtr __instance, int delete);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?CreateReceiver@SpoutReceiver@@QEAA_NPEADAEAI1_N@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool CreateReceiver(IntPtr __instance, sbyte* Sendername, uint* width, uint* height, bool bUseActive);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?ReceiveTexture@SpoutReceiver@@QEAA_NPEADAEAI1II_NI@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool ReceiveTexture(IntPtr __instance, sbyte* Sendername, uint* width, uint* height, uint TextureID, uint TextureTarget, bool bInvert, uint HostFBO);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?ReceiveImage@SpoutReceiver@@QEAA_NPEADAEAI1PEAEI_NI@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool ReceiveImage(IntPtr __instance, sbyte* Sendername, uint* width, uint* height, byte* pixels, uint glFormat, bool bInvert, uint HostFBO);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?CheckReceiver@SpoutReceiver@@QEAA_NPEADAEAI1AEA_N@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool CheckReceiver(IntPtr __instance, sbyte* Sendername, uint* width, uint* height, bool* bConnected);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GetImageSize@SpoutReceiver@@QEAA_NPEADAEAI1AEA_N@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool GetImageSize(IntPtr __instance, sbyte* Sendername, uint* width, uint* height, bool* bMemoryMode);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?ReleaseReceiver@SpoutReceiver@@QEAAXXZ")]
            internal static extern void ReleaseReceiver(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?BindSharedTexture@SpoutReceiver@@QEAA_NXZ")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool BindSharedTexture(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?DrawSharedTexture@SpoutReceiver@@QEAA_NMMM_NI@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool DrawSharedTexture(IntPtr __instance, float max_x, float max_y, float aspect, bool bInvert, uint HostFBO);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GetSenderName@SpoutReceiver@@QEAA_NHPEADH@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool GetSenderName(IntPtr __instance, int index, sbyte* Sendername, int MaxSize);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GetSenderInfo@SpoutReceiver@@QEAA_NPEBDAEAI1AEAPEAXAEAK@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool GetSenderInfo(IntPtr __instance, [MarshalAs(UnmanagedType.LPUTF8Str)] string Sendername, uint* width, uint* height, void** dxShareHandle, uint* dwFormat);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GetActiveSender@SpoutReceiver@@QEAA_NPEAD@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool GetActiveSender(IntPtr __instance, sbyte* Sendername);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?SetActiveSender@SpoutReceiver@@QEAA_NPEBD@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool SetActiveSender(IntPtr __instance, [MarshalAs(UnmanagedType.LPUTF8Str)] string Sendername);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?SelectSenderPanel@SpoutReceiver@@QEAA_NPEBD@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool SelectSenderPanel(IntPtr __instance, [MarshalAs(UnmanagedType.LPUTF8Str)] string message);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?SetDX9@SpoutReceiver@@QEAA_N_N@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool SetDX9(IntPtr __instance, bool bDX9);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?SetMemoryShareMode@SpoutReceiver@@QEAA_N_N@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool SetMemoryShareMode(IntPtr __instance, bool bMem);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?SetCPUmode@SpoutReceiver@@QEAA_N_N@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool SetCPUmode(IntPtr __instance, bool bCPU);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?SetShareMode@SpoutReceiver@@QEAA_NH@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool SetShareMode(IntPtr __instance, int mode);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GetAdapterName@SpoutReceiver@@QEAA_NHPEADH@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool GetAdapterName(IntPtr __instance, int index, sbyte* adaptername, int maxchars);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?SetAdapter@SpoutReceiver@@QEAA_NH@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool SetAdapter(IntPtr __instance, int index);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GetHostPath@SpoutReceiver@@QEAA_NPEBDPEADH@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool GetHostPath(IntPtr __instance, [MarshalAs(UnmanagedType.LPUTF8Str)] string sendername, sbyte* hostpath, int maxchars);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?SetVerticalSync@SpoutReceiver@@QEAA_N_N@Z")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool SetVerticalSync(IntPtr __instance, bool bSync);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?UnBindSharedTexture@SpoutReceiver@@QEAA_NXZ")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool UnBindSharedTexture(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GetSenderCount@SpoutReceiver@@QEAAHXZ")]
            internal static extern int GetSenderCount(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GetDX9@SpoutReceiver@@QEAA_NXZ")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool GetDX9(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GetMemoryShareMode@SpoutReceiver@@QEAA_NXZ")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool GetMemoryShareMode(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GetCPUmode@SpoutReceiver@@QEAA_NXZ")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool GetCPUmode(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GetShareMode@SpoutReceiver@@QEAAHXZ")]
            internal static extern int GetShareMode(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GetBufferMode@SpoutReceiver@@QEAA_NXZ")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool GetBufferMode(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?SetBufferMode@SpoutReceiver@@QEAAX_N@Z")]
            internal static extern void SetBufferMode(IntPtr __instance, bool bActive);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GetDX9compatible@SpoutReceiver@@QEAA_NXZ")]
            [return: MarshalAs(UnmanagedType.I1)]
            internal static extern bool GetDX9compatible(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?SetDX9compatible@SpoutReceiver@@QEAAX_N@Z")]
            internal static extern void SetDX9compatible(IntPtr __instance, bool bCompatible);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GetNumAdapters@SpoutReceiver@@QEAAHXZ")]
            internal static extern int GetNumAdapters(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GetAdapter@SpoutReceiver@@QEAAHXZ")]
            internal static extern int GetAdapter(IntPtr __instance);

            [SuppressUnmanagedCodeSecurity]
            [DllImport("Libraries/Spout.dll", CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "?GetVerticalSync@SpoutReceiver@@QEAAHXZ")]
            internal static extern int GetVerticalSync(IntPtr __instance);
        }

        public IntPtr __Instance { get; protected set; }

        internal static readonly System.Collections.Concurrent.ConcurrentDictionary<IntPtr, SpoutReceiver> NativeToManagedMap = new();

        protected bool __ownsNativeInstance;

        internal static SpoutReceiver __CreateInstance(IntPtr native, bool skipVTables = false)
        {
            return new SpoutReceiver(native.ToPointer(), skipVTables);
        }

        internal static SpoutReceiver __CreateInstance(SpoutReceiver.__Internal native, bool skipVTables = false)
        {
            return new SpoutReceiver(native, skipVTables);
        }

        private static void* __CopyValue(SpoutReceiver.__Internal native)
        {
            IntPtr ret = Marshal.AllocHGlobal(sizeof(SpoutReceiver.__Internal));
            *(SpoutReceiver.__Internal*)ret = native;
            return ret.ToPointer();
        }

        private SpoutReceiver(SpoutReceiver.__Internal native, bool skipVTables = false)
            : this(__CopyValue(native), skipVTables)
        {
            __ownsNativeInstance = true;
            NativeToManagedMap[__Instance] = this;
        }

        protected SpoutReceiver(void* native, bool skipVTables = false)
        {
            if (native == null)
            {
                return;
            }

            __Instance = new IntPtr(native);
        }

        public SpoutReceiver()
        {
            __Instance = Marshal.AllocHGlobal(sizeof(SpoutReceiver.__Internal));
            __ownsNativeInstance = true;
            NativeToManagedMap[__Instance] = this;
            _ = __Internal.ctor(__Instance);
        }

        public SpoutReceiver(SpoutReceiver _0)
        {
            __Instance = Marshal.AllocHGlobal(sizeof(SpoutReceiver.__Internal));
            __ownsNativeInstance = true;
            NativeToManagedMap[__Instance] = this;
            *(SpoutReceiver.__Internal*)__Instance = *(SpoutReceiver.__Internal*)_0.__Instance;
        }

        ~SpoutReceiver()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (__Instance == IntPtr.Zero)
            {
                return;
            }

            _ = NativeToManagedMap.TryRemove(__Instance, out _);
            if (disposing)
            {
                __Internal.dtor(__Instance, 0);
            }

            if (__ownsNativeInstance)
            {
                Marshal.FreeHGlobal(__Instance);
            }

            __Instance = IntPtr.Zero;
        }

        public bool CreateReceiver(sbyte* Sendername, ref uint width, ref uint height, bool bUseActive)
        {
            fixed (uint* __width1 = &width)
            {
                uint* __arg1 = __width1;
                fixed (uint* __height2 = &height)
                {
                    uint* __arg2 = __height2;
                    bool __ret = __Internal.CreateReceiver(__Instance, Sendername, __arg1, __arg2, bUseActive);
                    return __ret;
                }
            }
        }

        public bool ReceiveTexture(sbyte* Sendername, ref uint width, ref uint height, uint TextureID, uint TextureTarget, bool bInvert, uint HostFBO)
        {
            fixed (uint* __width1 = &width)
            {
                uint* __arg1 = __width1;
                fixed (uint* __height2 = &height)
                {
                    uint* __arg2 = __height2;
                    bool __ret = __Internal.ReceiveTexture(__Instance, Sendername, __arg1, __arg2, TextureID, TextureTarget, bInvert, HostFBO);
                    return __ret;
                }
            }
        }

        public bool ReceiveImage(sbyte* Sendername, ref uint width, ref uint height, byte* pixels, uint glFormat, bool bInvert, uint HostFBO)
        {
            fixed (uint* __width1 = &width)
            {
                uint* __arg1 = __width1;
                fixed (uint* __height2 = &height)
                {
                    uint* __arg2 = __height2;
                    bool __ret = __Internal.ReceiveImage(__Instance, Sendername, __arg1, __arg2, pixels, glFormat, bInvert, HostFBO);
                    return __ret;
                }
            }
        }

        public bool CheckReceiver(sbyte* Sendername, ref uint width, ref uint height, ref bool bConnected)
        {
            fixed (uint* __width1 = &width)
            {
                uint* __arg1 = __width1;
                fixed (uint* __height2 = &height)
                {
                    uint* __arg2 = __height2;
                    fixed (bool* __bConnected3 = &bConnected)
                    {
                        bool* __arg3 = __bConnected3;
                        bool __ret = __Internal.CheckReceiver(__Instance, Sendername, __arg1, __arg2, __arg3);
                        return __ret;
                    }
                }
            }
        }

        public bool GetImageSize(sbyte* Sendername, ref uint width, ref uint height, ref bool bMemoryMode)
        {
            fixed (uint* __width1 = &width)
            {
                uint* __arg1 = __width1;
                fixed (uint* __height2 = &height)
                {
                    uint* __arg2 = __height2;
                    fixed (bool* __bMemoryMode3 = &bMemoryMode)
                    {
                        bool* __arg3 = __bMemoryMode3;
                        bool __ret = __Internal.GetImageSize(__Instance, Sendername, __arg1, __arg2, __arg3);
                        return __ret;
                    }
                }
            }
        }

        public void ReleaseReceiver()
        {
            __Internal.ReleaseReceiver(__Instance);
        }

        public bool BindSharedTexture()
        {
            bool __ret = __Internal.BindSharedTexture(__Instance);
            return __ret;
        }

        public bool DrawSharedTexture(float max_x, float max_y, float aspect, bool bInvert, uint HostFBO)
        {
            bool __ret = __Internal.DrawSharedTexture(__Instance, max_x, max_y, aspect, bInvert, HostFBO);
            return __ret;
        }

        public bool GetSenderName(int index, sbyte* Sendername, int MaxSize)
        {
            bool __ret = __Internal.GetSenderName(__Instance, index, Sendername, MaxSize);
            return __ret;
        }

        public bool GetSenderInfo(string Sendername, ref uint width, ref uint height, void** dxShareHandle, ref uint dwFormat)
        {
            fixed (uint* __width1 = &width)
            {
                uint* __arg1 = __width1;
                fixed (uint* __height2 = &height)
                {
                    uint* __arg2 = __height2;
                    fixed (uint* __dwFormat4 = &dwFormat)
                    {
                        uint* __arg4 = __dwFormat4;
                        bool __ret = __Internal.GetSenderInfo(__Instance, Sendername, __arg1, __arg2, dxShareHandle, __arg4);
                        return __ret;
                    }
                }
            }
        }

        public bool GetActiveSender(sbyte* Sendername)
        {
            bool __ret = __Internal.GetActiveSender(__Instance, Sendername);
            return __ret;
        }

        public bool SetActiveSender(string Sendername)
        {
            bool __ret = __Internal.SetActiveSender(__Instance, Sendername);
            return __ret;
        }

        public bool SelectSenderPanel(string message)
        {
            bool __ret = __Internal.SelectSenderPanel(__Instance, message);
            return __ret;
        }

        public bool SetDX9(bool bDX9)
        {
            bool __ret = __Internal.SetDX9(__Instance, bDX9);
            return __ret;
        }

        public bool SetMemoryShareMode(bool bMem)
        {
            bool __ret = __Internal.SetMemoryShareMode(__Instance, bMem);
            return __ret;
        }

        public bool SetCPUmode(bool bCPU)
        {
            bool __ret = __Internal.SetCPUmode(__Instance, bCPU);
            return __ret;
        }

        public bool SetShareMode(int mode)
        {
            bool __ret = __Internal.SetShareMode(__Instance, mode);
            return __ret;
        }

        public bool GetAdapterName(int index, sbyte* adaptername, int maxchars)
        {
            bool __ret = __Internal.GetAdapterName(__Instance, index, adaptername, maxchars);
            return __ret;
        }

        public bool SetAdapter(int index)
        {
            bool __ret = __Internal.SetAdapter(__Instance, index);
            return __ret;
        }

        public bool GetHostPath(string sendername, sbyte* hostpath, int maxchars)
        {
            bool __ret = __Internal.GetHostPath(__Instance, sendername, hostpath, maxchars);
            return __ret;
        }

        public bool SetVerticalSync(bool bSync)
        {
            bool __ret = __Internal.SetVerticalSync(__Instance, bSync);
            return __ret;
        }

        public Spout Spout
        {
            get => Spout.__CreateInstance(new IntPtr(&((SpoutReceiver.__Internal*)__Instance)->spout));

            set
            {
                if (value is null)
                {
                    throw new ArgumentNullException("value", "Cannot be null because it is passed by value.");
                } ((SpoutReceiver.__Internal*)__Instance)->spout = *(Spout.__Internal*)value.__Instance;
            }
        }

        public bool UnBindSharedTexture
        {
            get
            {
                bool __ret = __Internal.UnBindSharedTexture(__Instance);
                return __ret;
            }
        }

        public int SenderCount
        {
            get
            {
                int __ret = __Internal.GetSenderCount(__Instance);
                return __ret;
            }
        }

        public bool DX9
        {
            get
            {
                bool __ret = __Internal.GetDX9(__Instance);
                return __ret;
            }

            set => __Internal.SetDX9(__Instance, value);
        }

        public bool MemoryShareMode
        {
            get
            {
                bool __ret = __Internal.GetMemoryShareMode(__Instance);
                return __ret;
            }

            set => __Internal.SetMemoryShareMode(__Instance, value);
        }

        public bool CPUmode
        {
            get
            {
                bool __ret = __Internal.GetCPUmode(__Instance);
                return __ret;
            }

            set => __Internal.SetCPUmode(__Instance, value);
        }

        public int ShareMode
        {
            get
            {
                int __ret = __Internal.GetShareMode(__Instance);
                return __ret;
            }

            set => __Internal.SetShareMode(__Instance, value);
        }

        public bool BufferMode
        {
            get
            {
                bool __ret = __Internal.GetBufferMode(__Instance);
                return __ret;
            }

            set => __Internal.SetBufferMode(__Instance, value);
        }

        public bool DX9compatible
        {
            get
            {
                bool __ret = __Internal.GetDX9compatible(__Instance);
                return __ret;
            }

            set => __Internal.SetDX9compatible(__Instance, value);
        }

        public int NumAdapters
        {
            get
            {
                int __ret = __Internal.GetNumAdapters(__Instance);
                return __ret;
            }
        }

        public int Adapter
        {
            get
            {
                int __ret = __Internal.GetAdapter(__Instance);
                return __ret;
            }

            set => __Internal.SetAdapter(__Instance, value);
        }

        public int VerticalSync
        {
            get
            {
                int __ret = __Internal.GetVerticalSync(__Instance);
                return __ret;
            }
        }
    }
}

#pragma warning restore IDE1006
#pragma warning restore IDE0060
#pragma warning restore CA2101
#pragma warning restore CA1507