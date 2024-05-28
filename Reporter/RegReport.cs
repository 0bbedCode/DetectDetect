using DetectDetect.Reggy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectDetect.Reporter {
    internal class RegReport {
        internal static void WriteRegReport(ReportWriter report) {
            if(report != null) {
                report.WriteTagHeader("[ACPI]").NewLine(2);
                using(var key = new RegKey("HKLM\\HARDWARE\\ACPI")) {
                    foreach (var k in key.EnumKeys()) {
                        List<string> sNames = k.GetSubkeyNames();
                        if (sNames.Count() > 0) {
                            report.Write($"[{k.Name}][{sNames.First()}]").NewLine();
                        }
                    }
                }

                report.WriteTagHeader("[Services]").NewLine(2);
                using(var key = new RegKey("HKLM\\SYSTEM\\ControlSet001\\Services")) {
                    foreach(var k in key.GetSubkeyNames()) {
                        report.Write(k).NewLine();
                    }
                }

                report.WriteTagHeader("[Software]").NewLine(2);
                using(var key = new RegKey("HKLM\\SOFTWARE")) {
                    foreach(var k in key.GetSubkeyNames()) {
                        report.Write(k).NewLine();
                    }
                }

                report.WriteTagHeader("[Descriton\\System]").NewLine(2);
                using (var key = new RegKey("HKLM\\HARDWARE\\DESCRIPTION\\System")) {
                    foreach (var k in key.GetAllValues()) {
                        report.Write($"[{k.Key}][{k.Value}]").NewLine();
                    }
                }

                report.WriteTagHeader("[Descriton\\System\\BIOS]").NewLine(2);
                using (var key = new RegKey("HKLM\\HARDWARE\\DESCRIPTION\\System\\BIOS")) {
                    foreach (var k in key.GetAllValues()) {
                        report.Write($"[{k.Key}][{k.Value}]").NewLine();
                    }
                }

                report.WriteTagHeader("[Descriton\\System\\CentralProcessor]").NewLine(2);
                using (var key = new RegKey("HKLM\\HARDWARE\\DESCRIPTION\\System\\CentralProcessor")) {
                    int index = 0;
                    foreach(var k in key.EnumKeys()) {
                        report.WriteTagHeader($"Core [{index}]").NewLine(2);
                        index++;
                        foreach (var p in k.GetAllValues()) {
                            report.Write($"[{p.Key}][{p.Value}]").NewLine();
                        }
                    }
                }

                report.WriteTagHeader("[CurrentVersion]");
                using(var key = new RegKey("HKLM\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion")) {
                    foreach (var k in key.GetAllValues()) {
                        report.Write($"[{k.Key}][{k.Value}]").NewLine();
                    }
                }

                //Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
                report.WriteTagHeader("[Scsi]").NewLine(2);
                using (var key = new RegKey("HKLM\\HARDWARE\\DEVICEMAP\\Scsi\\Scsi Port 0\\Scsi Bus 0")) {
                    foreach (var k in key.EnumKeys()) {
                        if(k.Name.StartsWith("Target")) {
                            using(var sKey = k.OpenSubkey("Logical Unit Id 0")) {
                                if(sKey.Exists()) {
                                    report.WriteTagHeader($"[{k.Name}]").NewLine();
                                    foreach (var p in sKey.GetAllValues()) {
                                        report.Write($"[{p.Key}][{p.Value}]").NewLine();
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
