using DetectDetect.Native;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DetectDetect.Info {
    internal class Sys {
        internal static string Username => Environment.UserName;
        internal static string Domain => Environment.UserDomainName;
        internal static string MachineName => Environment.MachineName;
        internal static int ProcessorCount => Environment.ProcessorCount;
        internal static string Wallpaper => WallpaperUtils.GetWallpaper();

        internal static string GetCurrentFileName() => Path.GetFileName(Assembly.GetExecutingAssembly().Location);
        internal static string GetSystemDirectory() {
            var spth = string.Empty;
            for (int i = 0; i < 2 && string.IsNullOrEmpty(spth); i++) {
                switch (i) {
                    case 0: spth = Environment.SystemDirectory; break;
                    case 1: spth = Environment.GetFolderPath(Environment.SpecialFolder.System); break;
                }
            } return spth;
        }

        internal static string GetWindowsDirectory() {
            var wdir = string.Empty;
            for (int i = 0; i < 4 && string.IsNullOrEmpty(wdir); i++) {
                switch (i) {
                    case 0:
                        wdir = Interaction.Environ("windir");
                        wdir = string.IsNullOrEmpty(wdir) ? Interaction.Environ("systemroot") : wdir;
                        break;
                    case 1:
                        var strBuilder = new StringBuilder(255);
                        Win32.KERNEL32.Invoke<int>("GetWindowsDirectory", strBuilder, strBuilder.Capacity);
                        wdir = strBuilder.ToString();
                        break;
                    case 3:
                        //wdir = CurrentVersion.TryGetValueStr("PathName");
                        //wdir = string.IsNullOrEmpty(wdir) ? CurrentVersion.TryGetValueStr("SystemRoot") : wdir;
                        break;
                }
            } return wdir;
        }

    }
}
