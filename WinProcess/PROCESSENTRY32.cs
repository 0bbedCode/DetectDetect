using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DetectDetect.WinProcess {
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    internal unsafe struct PROCESSENTRY32 {
        internal const int MAX_PATH = 260;
        internal uint dwSize;

        /// <summary>
        /// Depricated
        /// </summary>
        internal uint cntUsage;
        internal uint th32ProcessIDEnumProcessesSnapshot;

        /// <summary>
        /// Depricated
        /// </summary>
        internal IntPtr th32DefaultHeapID;

        /// <summary>
        /// Depricated
        /// </summary>
        internal uint th32ModuleID;
        internal uint cntThreads;
        internal uint th32ParentProcessID;
        internal int pcPriClassBase;

        /// <summary>
        /// Depricated
        /// </summary>
        internal uint dwFlags;

        //[MarshalAs(UnmanagedType.LPWStr, SizeConst = 3)]
        internal fixed char szExeFile[MAX_PATH];

        //[MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_PATH)] public string szExeFile;

        internal IntPtr Address {
            get {
                fixed (PROCESSENTRY32* ptr = &this) {
                    return (IntPtr)(ptr);
                }
            }
        }
    }
}
