using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace DetectDetect.Utils {
    internal class FileUtils {
        public static byte[] ZipFolderToByteArray(string folderPath) {
            using (MemoryStream memoryStream = new MemoryStream()) {
                using (ZipArchive archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true)) {
                    DirectoryInfo directoryInfo = new DirectoryInfo(folderPath);
                    foreach (FileInfo file in directoryInfo.GetFiles("*", SearchOption.AllDirectories)) {
                        try {
                            string entryName = file.FullName.Substring(directoryInfo.FullName.Length + 1);
                            ZipArchiveEntry entry = archive.CreateEntry(entryName);

                            using (Stream entryStream = entry.Open())
                            using (FileStream fileStream = file.OpenRead()) {
                                fileStream.CopyTo(entryStream);
                            }
                        } catch(Exception e) { Console.WriteLine("ew=" + e.Message + " f=" + file); }
                    }
                }
                return memoryStream.ToArray();
            }
        }

        public static List<string> GetDrives() {
            List<string> letters = new List<string>();
            foreach(var d in DriveInfo.GetDrives()) {
                if(d.IsReady) {
                    letters.Add(d.Name);
                }
            } return letters;
        }

        public static List<string> GetUSBDrives() {
            List<string> usbDrives = new List<string>();
            // Query for all connected USB drives
            try {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive WHERE InterfaceType='USB'");
                foreach (ManagementObject queryObj in searcher.Get()) {
                    // Get the drive letters associated with the USB drive
                    ManagementObjectSearcher partitionSearcher = new ManagementObjectSearcher(
                        "ASSOCIATORS OF {Win32_DiskDrive.DeviceID='" + queryObj["DeviceID"] + "'} WHERE AssocClass=Win32_DiskDriveToDiskPartition");
                    foreach (ManagementObject partition in partitionSearcher.Get()) {
                        ManagementObjectSearcher logicalSearcher = new ManagementObjectSearcher(
                            "ASSOCIATORS OF {Win32_DiskPartition.DeviceID='" + partition["DeviceID"] + "'} WHERE AssocClass=Win32_LogicalDiskToPartition");
                        foreach (ManagementObject logical in logicalSearcher.Get()) {
                            usbDrives.Add(logical["DeviceID"].ToString());
                        }
                    }
                }
            } catch { }
            return usbDrives;
        }
    }
}
