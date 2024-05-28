using DetectDetect.WinProcess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectDetect.Reporter {
    internal unsafe class ProcessReport {
        internal static void WriteProcesses(ReportWriter writer) {
            try {
                foreach(var p in Win32Process.EnumProcessesSnapshot()) {
                    writer.Write($"[Name][{StrUtils.FixedCharToString(p.szExeFile, PROCESSENTRY32.MAX_PATH)}]").NewLine()
                        .Write($"[ThreadID][{p.th32ModuleID}]").NewLine()
                        .Write($"[ParentProcessID][{p.th32ParentProcessID}]").NewLine(2);
                }

                writer.NewLine(2).WriteTagHeader("PIDS");
                foreach (var p in Win32Process.EnumProcessIDs())
                    writer.Write($"{p}").NewLine();
            } catch { }
        }
    }
}
