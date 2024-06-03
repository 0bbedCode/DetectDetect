using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using P = System.IntPtr;

namespace DetectDetect.Native.Runtime {
    public class Win32Memory<TRet, TObj> {
        #region Names
        public static readonly string RNAME = "ReadProcessMemory";
        public static readonly string WNAME = "WriteProcessMemory";
        #endregion

        #region Delegates
        public delegate bool rpm_1(P hProcess, TObj baseObj, out TRet obj, int length, out P lpNum);
        public delegate bool rpm_2(P hProcess, TObj baseObj, TRet obj, int length, out P lpNum);
        public delegate bool wpm_1(P hProcess, TObj baseObj, ref TRet obj, int length, out P lpNum);

        public delegate bool rpm_1_ref(P hProcess, ref TObj baseObj, out TRet obj, int length, out P lpNum);
        public delegate bool rpm_2_ref(P hProcess, ref TObj baseObj, TRet obj, int length, out P lpNum);
        public delegate bool wpm_1_ref(P hProcess, ref TObj baseObj, ref TRet obj, int length, out P lpNum);
        #endregion

        #region Delegate Instances
        private static readonly rpm_1 _rpm_1 = Win32.KERNEL32.BindDelegate<rpm_1>(RNAME);
        private static readonly rpm_2 _rpm_2 = Win32.KERNEL32.BindDelegate<rpm_2>(RNAME);

        private static readonly wpm_1 _wpm_1 = Win32.KERNEL32.BindDelegate<wpm_1>(WNAME);    //Ref
        private static readonly rpm_2 _wpm_2 = Win32.KERNEL32.BindDelegate<rpm_2>(WNAME);    //In

        //Extensions using REF

        private static readonly rpm_1_ref _rpm_1_ref = Win32.KERNEL32.BindDelegate<rpm_1_ref>(RNAME);
        private static readonly rpm_2_ref _rpm_2_ref = Win32.KERNEL32.BindDelegate<rpm_2_ref>(RNAME);

        private static readonly wpm_1_ref _wpm_1_ref = Win32.KERNEL32.BindDelegate<wpm_1_ref>(WNAME);
        private static readonly rpm_2_ref _wpm_2_ref = Win32.KERNEL32.BindDelegate<rpm_2_ref>(WNAME);
        #endregion

        public static TRet ReadEx(TObj baseobj, int length = -1) => ReadProcessEx(Win32.CurrentProcess, baseobj, length);
        public static TRet ReadProcessEx(P hProcess, TObj baseobj, int length = -1) {
            _rpm_1(hProcess, baseobj, out TRet obj, length == -1 ? Marshal.SizeOf(typeof(TRet)) : length, out var lpNum);
            return obj;
        }

        public static TRet ReadEx(ref TObj baseobj, int length = -1) => ReadProcessEx(Win32.CurrentProcess, ref baseobj, length);
        public static TRet ReadProcessEx(P hProcess, ref TObj baseobj, int length = -1) {
            _rpm_1_ref(hProcess, ref baseobj, out TRet obj, length == -1 ? Marshal.SizeOf(typeof(TRet)) : length, out var lpNum);
            return obj;
        }

        public static bool Read(TObj baseobj, TRet obj, int length = -1) => ReadProcess(Win32.CurrentProcess, baseobj, obj, length);
        public static bool ReadProcess(P hProcess, TObj baseobj, TRet obj, int length = -1)
            => _rpm_2(hProcess, baseobj, obj, length == -1 ? Marshal.SizeOf(typeof(TRet)) : length, out var lpNum);

        public static bool Read(ref TObj baseobj, TRet obj, int length = -1) => ReadProcess(Win32.CurrentProcess, baseobj, obj, length);
        public static bool ReadProcess(P hProcess, ref TObj baseobj, TRet obj, int length = -1)
            => _rpm_2_ref(hProcess, ref baseobj, obj, length == -1 ? Marshal.SizeOf(typeof(TRet)) : length, out var lpNum);

        public static bool WriteEx(TObj baseobj, ref TRet obj, int length = -1) => WriteProcessEx(Win32.CurrentProcess, baseobj, ref obj, length);
        public static bool WriteProcessEx(P hProcess, TObj baseobj, ref TRet obj, int length = -1)
            => _wpm_1(hProcess, baseobj, ref obj, length == -1 ? Marshal.SizeOf(typeof(TRet)) : length, out var lpNum);

        public static bool WriteEx(ref TObj baseobj, ref TRet obj, int length = -1) => WriteProcessEx(Win32.CurrentProcess, ref baseobj, ref obj, length);
        public static bool WriteProcessEx(P hProcess, ref TObj baseobj, ref TRet obj, int length = -1)
            => _wpm_1_ref(hProcess, ref baseobj, ref obj, length == -1 ? Marshal.SizeOf(typeof(TRet)) : length, out var lpNum);



        public static bool Write(TObj baseobj, TRet obj, int length = -1) => WriteProcess(Win32.CurrentProcess, baseobj, obj, length);
        public static bool WriteProcess(P hProcess, TObj baseobj, TRet obj, int length = -1)
            => _wpm_2(hProcess, baseobj, obj, length == -1 ? Marshal.SizeOf(typeof(TRet)) : length, out var lpNum);

        public static bool Write(ref TObj baseobj, TRet obj, int length = -1) => WriteProcess(Win32.CurrentProcess, ref baseobj, obj, length);
        public static bool WriteProcess(P hProcess, ref TObj baseobj, TRet obj, int length = -1)
            => _wpm_2_ref(hProcess, ref baseobj, obj, length == -1 ? Marshal.SizeOf(typeof(TRet)) : length, out var lpNum);
    }
}
