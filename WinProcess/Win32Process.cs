using DetectDetect.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DetectDetect.WinProcess {
    internal unsafe class Win32Process {
        internal delegate IntPtr snapshot_del(int flags, int processId);
        internal delegate bool nextfirst_del(IntPtr hSnapshot, IntPtr structure);

        internal static readonly snapshot_del CreateSnapshot = Win32.KERNEL32.BindDelegate<snapshot_del>("CreateToolhelp32Snapshot");
        internal static readonly nextfirst_del FirstProcess = Win32.KERNEL32.BindDelegate<nextfirst_del>("Process32First");
        internal static readonly nextfirst_del NextProcess = Win32.KERNEL32.BindDelegate<nextfirst_del>("Process32Next");

        internal static IEnumerable<PROCESSENTRY32> EnumProcessesSnapshot() {
            var hSnapshot = CreateSnapshot(0x00000002, 0);
            if (!hSnapshot.IsValid())
                yield break;

            var entry = new PROCESSENTRY32();
            entry.dwSize = (uint)Marshal.SizeOf(typeof(PROCESSENTRY32));

            if (FirstProcess(hSnapshot, entry.Address)) {
                do {
                    yield return entry;
                } while (NextProcess(hSnapshot, entry.Address));
            }
        }

        internal static uint[] EnumProcessIDs() {
            var arraySize = 1024;
            var arrayBytesSize = arraySize * sizeof(uint);
            var processIds = new uint[arraySize];
            var bytesCopied = 0;


            var ret = Win32.PCONTORL.Invoke<bool>("EnumProcesses", processIds, arrayBytesSize, (IntPtr)(&bytesCopied));
            if (bytesCopied == 0 || !ret)
                return null;

            var numIdsCopied = bytesCopied >> 2;//get actual amount of ids copied
            if (numIdsCopied < 0)
                return null;

            var ids = new uint[numIdsCopied];
            Array.Copy(processIds, ids, numIdsCopied);
            return ids;
        }
    }
}
