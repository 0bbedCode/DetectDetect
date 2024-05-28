using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectDetect.WMICore.Classes {
    internal class WmiLogicalDisk : IWmiClassReader {
        internal static readonly string CLASS = "Win32_LogicalDisk";
        internal static readonly List<string> FIELDS = new List<string> {
            "DeviceID",
            "Access",
            "Caption",
            "Description",
            "DriveType",
            "FileSystem",
            "FreeSpace",
            "MediaType",
            "Name",
            "Size",
            "VolumeDirty",
            "VolumeName",
            "VolumeSerialNumber"
        };

        public virtual List<string> GetFields() => FIELDS;
        public virtual string GetClassName() => CLASS;
    }
}
