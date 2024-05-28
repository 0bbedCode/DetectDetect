using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectDetect.WMICore.Classes {
    internal class WmiCacheMemory : IWmiClassReader {
        internal static readonly string CLASS = "Win32_CacheMemory";
        internal static readonly List<string> FIELDS = new List<string> {
            "DeviceID",
            "Associativity",
            "Availability",
            "BlockSize",
            "CacheType",
            "Caption",
            "Description",
            "InstalledSize",
            "Level",
            "Location",
            "MaxCacheSize",
            "Name",
            "NumberOfBlocks",
            "Purpose",
            "Status",
            "StatusInfo",
            "SystemName",
            "WritePolicy"
        };

        public virtual List<string> GetFields() => FIELDS;
        public virtual string GetClassName() => CLASS;
    }
}
