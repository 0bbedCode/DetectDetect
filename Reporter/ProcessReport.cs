using DetectDetect.WinProcess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectDetect.Reporter {
    internal unsafe class ProcessReport {
        internal static void WriteProcesses(ReportWriter writer, StringBuilder sb = null) {
            try {
                foreach(var p in Win32Process.EnumProcessesSnapshot()) {
                    writer.Write($"[Name][{StrUtils.FixedCharToString(p.szExeFile, PROCESSENTRY32.MAX_PATH)}]", sb).NewLine(sb)
                        .Write($"[ThreadID][{p.th32ModuleID}]", sb).NewLine(sb)
                        .Write($"[ParentProcessID][{p.th32ParentProcessID}]", sb).NewLine(2, sb);
                }

                writer.NewLine(2).WriteTagHeader("PIDS", sb);
                foreach (var p in Win32Process.EnumProcessIDs())
                    writer.Write($"{p}", sb).NewLine(sb);
            } catch { }
        }
    }
}
