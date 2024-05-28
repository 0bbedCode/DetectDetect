using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectDetect.WMICore.Classes {
    internal class WmiDiskPartition : IWmiClassReader {
        internal static readonly string CLASS = "Win32_DiskPartition";
        internal static readonly List<string> FIELDS = new List<string> {
            "DeviceID",
            "Bootable",
            "BootPartition",
            "Caption",
            "Description",
            "DiskIndex",
            "Index",
            "Name",
            "NumberOfBlocks",
            "PrimaryPartition",
            "Size",
            "StartingOffset",
            "Type"
        };

        public virtual List<string> GetFields() => FIELDS;
        public virtual string GetClassName() => CLASS;
    }
}
