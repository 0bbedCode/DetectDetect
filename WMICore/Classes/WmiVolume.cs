using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectDetect.WMICore.Classes {
    internal class WmiVolume : IWmiClassReader {
        internal static readonly string CLASS = "Win32_Volume";
        internal static readonly List<string> FIELDS = new List<string> {
            "DeviceID",
            "Automount",
            "BlockSize",
            "BootVolume",
            "Capacity",
            "Caption",
            "Compressed",
            "DriveLetter",
            "DriveType",
            "FileSystem",
            "FreeSpace",
            "Label",
            "MaximumFileNameLength",
            "Name",
            "SerialNumber",
            "SystemVolume"
        };

        public virtual List<string> GetFields() => FIELDS;
        public virtual string GetClassName() => CLASS;
    }
}
