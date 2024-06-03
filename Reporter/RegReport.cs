using DetectDetect.Reggy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectDetect.Reporter {
    internal class RegReport {
        internal static void WriteRegReport(ReportWriter report, StringBuilder sb = null) {
            if(report != null) {
                report.WriteTagHeader("[ACPI]", sb).NewLine(2, sb);
                using(var key = new RegKey("HKLM\\HARDWARE\\ACPI")) {
                    foreach (var k in key.EnumKeys()) {
                        List<string> sNames = k.GetSubkeyNames();
                        if (sNames.Count() > 0) {
                            report.Write($"[{k.Name}][{sNames.First()}]", sb).NewLine(sb);
                        }
                    }
                }

                report.WriteTagHeader("[Services]", sb).NewLine(2, sb);
                using(var key = new RegKey("HKLM\\SYSTEM\\ControlSet001\\Services")) {
                    foreach(var k in key.GetSubkeyNames()) {
                        report.Write(k, sb).NewLine(sb);
                    }
                }

                report.WriteTagHeader("[Software]", sb).NewLine(2, sb);
                using(var key = new RegKey("HKLM\\SOFTWARE")) {
                    foreach(var k in key.GetSubkeyNames()) {
                        report.Write(k, sb).NewLine(sb);
                    }
                }

                report.WriteTagHeader("[Descriton\\System]", sb).NewLine(2, sb);
                using (var key = new RegKey("HKLM\\HARDWARE\\DESCRIPTION\\System")) {
                    foreach (var k in key.GetAllValues()) {
                        report.Write($"[{k.Key}][{k.Value}]", sb).NewLine(sb);
                    }
                }

                report.WriteTagHeader("[Descriton\\System\\BIOS]", sb).NewLine(2, sb);
                using (var key = new RegKey("HKLM\\HARDWARE\\DESCRIPTION\\System\\BIOS")) {
                    foreach (var k in key.GetAllValues()) {
                        report.Write($"[{k.Key}][{k.Value}]", sb).NewLine(sb);
                    }
                }

                report.WriteTagHeader("[Descriton\\System\\CentralProcessor]", sb).NewLine(2, sb);
                using (var key = new RegKey("HKLM\\HARDWARE\\DESCRIPTION\\System\\CentralProcessor")) {
                    int index = 0;
                    foreach(var k in key.EnumKeys()) {
                        report.WriteTagHeader($"Core [{index}]", sb).NewLine(2, sb);
                        index++;
                        foreach (var p in k.GetAllValues()) {
                            report.Write($"[{p.Key}][{p.Value}]", sb).NewLine(sb);
                        }
                    }
                }

                report.WriteTagHeader("[CurrentVersion]", sb).NewLine(2, sb);
                using (var key = new RegKey("HKLM\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion")) {
                    foreach (var k in key.GetAllValues()) {
                        report.Write($"[{k.Key}][{k.Value}]", sb).NewLine(sb);
                    }
                }

                //Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
                report.WriteTagHeader("[Scsi]", sb).NewLine(2, sb);
                using (var key = new RegKey("HKLM\\HARDWARE\\DEVICEMAP\\Scsi\\Scsi Port 0\\Scsi Bus 0")) {
                    foreach (var k in key.EnumKeys()) {
                        if(k.Name.StartsWith("Target")) {
                            using(var sKey = k.OpenSubkey("Logical Unit Id 0")) {
                                if(sKey.Exists()) {
                                    report.WriteTagHeader($"[{k.Name}]", sb).NewLine(sb);
                                    foreach (var p in sKey.GetAllValues()) {
                                        report.Write($"[{p.Key}][{p.Value}]", sb).NewLine(sb);
                                    }
                                }
                            }
                        }
                    }
                }
                //HARDWARE\DEVICEMAP\Scsi\Scsi Port 0\Scsi Bus 0
            }
        }
    }
}
