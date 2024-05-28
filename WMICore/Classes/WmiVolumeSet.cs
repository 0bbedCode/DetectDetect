using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectDetect.WMICore.Classes {
    internal class WmiVolumeSet : IWmiClassReader {
        internal static readonly string CLASS = "CIM_StorageVolume";
        internal static readonly List<string> FIELDS = new List<string> {
            "DeviceID",
            "Automount",
            "BootVolume",
            "Capacity",
            "Caption",
            "DriveLetter",
            "Compressed",
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
