using DetectDetect.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectDetect.Reporter {
    internal class FileReport {
        internal static string Env(Environment.SpecialFolder specialFolder) => Environment.GetFolderPath(specialFolder);
        internal static void WriteFileReport(ReportWriter reporter) {
            foreach (var usb in FileUtils.GetUSBDrives())
                reporter.Write($"[USB][{usb}]").NewLine();

            try {
                var paths = FileUtils.GetDrives();
                foreach (var path in paths) {
                    try {
                        if (Directory.Exists(path)) {
                            reporter.WriteTagHeader(path).NewLine(2);
                            foreach (var d in Directory.GetDirectories(path))
                                reporter.Write(d).NewLine();
                            foreach (var f in Directory.GetFiles(path))
                                reporter.Write(f).NewLine();
                        }
                    } catch { }
                }
            } catch { }
            try {
                var paths = new string[] {
                    Env(Environment.SpecialFolder.ProgramFiles),
                    Env(Environment.SpecialFolder.ProgramFilesX86),
                    Env(Environment.SpecialFolder.CommonProgramFiles),
                    Env(Environment.SpecialFolder.CommonProgramFilesX86), 
                    Env(Environment.SpecialFolder.LocalApplicationData), 
                    Env(Environment.SpecialFolder.ApplicationData), 
                    Env(Environment.SpecialFolder.System), 
                    Env(Environment.SpecialFolder.MyDocuments),
                    Env(Environment.SpecialFolder.CommonStartup),
                    Env(Environment.SpecialFolder.Startup),
                    Env(Environment.SpecialFolder.Recent),
                    $"{Env(Environment.SpecialFolder.System)}\\drivers", 
                    Path.GetTempPath() };

                foreach(var path in paths) {
                    try {
                        if (Directory.Exists(path)) {
                            reporter.WriteTagHeader(path).NewLine(2);
                            if(!path.EndsWith("drivers", StringComparison.OrdinalIgnoreCase) && !path.EndsWith("system32", StringComparison.OrdinalIgnoreCase))
                                foreach (var d in Directory.GetDirectories(path))
                                    reporter.Write(d).NewLine();
                            foreach (var f in Directory.GetFiles(path))
                                reporter.Write(f).NewLine();
                        }
                    } catch { }
                }
            } catch { }
        }

        
    }
}
