using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DetectDetect.Native {
    public unsafe class Win32 {
        public delegate void _setlast(int err);
        public delegate int _getlast();
        public static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);
        public static readonly HandleRef NullHandleRef = new HandleRef(null, IntPtr.Zero);


        public static readonly Library USER32 = new Library("User32.dll");
        public static readonly Library KERNEL32 = new Library("Kernel32.dll");
        public static readonly Library ADVAPI32 = new Library("Advapi32");
        public static readonly Library PSAPI = new Library("Psapi.dll");
        public static readonly Library NTDll = new Library("NTDll.dll");

        internal static readonly Library PCONTORL = new Library(FindLibrary("Kernel32.dll", "Psapi.dll", "EnumProcessModulesEx"));
        public static readonly Library Msvcrt = new Library("Msvcrt.dll");

        public static readonly IntPtr CurrentProcess = KERNEL32.Invoke<IntPtr>("GetCurrentProcess");

        public static string FindLibrary(string lib1, string lib2, string function) {
            if (IntPtrExtensions.IsValid(GetFunctionEx(lib1, function)))
                return lib1;
            if (IntPtrExtensions.IsValid(GetFunctionEx(lib2, function)))
                return lib2;
            return string.Empty;
        }

        public static IntPtr GetFunction(IntPtr hModule, string functionName)
            => KERNEL32.Invoke<IntPtr>("GetProcAddress", false, CharSet.Ansi, hModule, functionName);

        public static IntPtr GetLibrary(string libraryName) {
            var hMod = KERNEL32.Invoke<IntPtr>("GetModuleHandle", libraryName);
            if (!IntPtrExtensions.IsValid(hMod))
                return KERNEL32.Invoke<IntPtr>("LoadLibrary", libraryName);

            return hMod;
        }

        public static IntPtr GetFunctionEx(string moduleName, string functionName) => GetFunctionEx(GetLibrary(moduleName), functionName);
        public static IntPtr GetFunctionEx(IntPtr hModule, string functionName, bool symbolSearch = true) {
            var hFunc = GetFunction(hModule, functionName);
            if (!IntPtrExtensions.IsValid(hFunc) && symbolSearch) {
                functionName = PInvokeUtils.CleanName(functionName);
                var syms = new List<char> { 'A', 'W' };

                if (syms.Contains(functionName.Last())) {
                    syms.Remove(functionName.Last());
                    syms.Add('\0');
                    functionName = functionName.TrimEnd(functionName.Last());
                }

                foreach (var itm in syms) {
                    hFunc = GetFunction(hModule, functionName + itm);
                    if (IntPtrExtensions.IsValid(hFunc))
                        return hFunc;
                }
            }

            return hFunc;
        }

        public static IntPtr DuplicateHandle(IntPtr handle, uint access = 0x00000002) {
            var hOut = IntPtr.Zero;
            KERNEL32.Invoke<bool>(
                "DuplicateHandle",
                CurrentProcess, handle, CurrentProcess, (IntPtr)(&hOut), 0, false, access);
            return hOut;
        }

        public static void CloseHandle(IntPtr handle)
            => KERNEL32.Invoke<bool>("CloseHandle", handle);

        public static _setlast SetLastError = KERNEL32.BindDelegate<_setlast>("SetLastError", false);
        public static _getlast GetLastError = KERNEL32.BindDelegate<_getlast>("GetLastError", false);


    }
}
