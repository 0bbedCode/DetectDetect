using DetectDetect.WMICore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DetectDetect.Reporter {
    internal class SecurityReport {
        public static void WriteSecurity(ReportWriter writer, StringBuilder sb = null) {
            var nameSpaces = new string[] { "SecurityCenter", "SecurityCenter2" };
            var classNames = new string[] { "AntiVirusProduct", "AntiSpywareProduct", "FirewallProduct" };
            foreach (var nameSpace in nameSpaces) {
                foreach(var className in classNames) {
                    try {
                        var wmiData = new ManagementObjectSearcher(@"root\" + nameSpace, "SELECT * FROM " + className);
                        var data = wmiData.Get();
                        writer.WriteTagHeader(className, sb).NewLine(sb);
                        foreach (ManagementObject av in data) 
                            WriteFields(av, writer, sb);
                    } catch { }
                }
            }
        }

        private static void WriteFields(ManagementObject obj, ReportWriter writer, StringBuilder sb = null) {
            try {
                writer
                    .Write($"[DisplayName][{obj.ToString("displayName")}]", sb).NewLine(sb)
                    .Write($"[CompanyName][{obj.ToString("companyName")}]", sb).NewLine(sb)
                    .Write($"[Version][{obj.ToString("versionNumber")}]", sb).NewLine(sb)
                    .Write($"[Enabled][{obj.ToString("productEnabled")}]", sb).NewLine(sb)
                    .Write($"[State][{obj.ToString("productState")}]", sb).NewLine(sb)
                    .NewLine(2, sb);
            } catch { }
        }
    }
}
