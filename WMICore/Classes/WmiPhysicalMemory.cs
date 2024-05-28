using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectDetect.WMICore.Classes {
    internal class WmiPhysicalMemory : IWmiClassReader {
        internal static readonly string CLASS = "Win32_PhysicalMemory";
        internal static readonly List<string> FIELDS = new List<string> {
            "Tag",
            "Attributes",
            "BankLabel",
            "Capacity",
            "Caption",
            "ConfiguredClockSpeed",
            "ConfiguredVoltage",
            "DataWidth",
            "Description",
            "DeviceLocator",
            "FormFactor",
            "InterleavePosition",
            "Manufacturer",
            "Name",
            "PartNumber",
            "SerialNumber",
            "SMBIOSMemoryType"
        };

        public virtual List<string> GetFields() => FIELDS;
        public virtual string GetClassName() => CLASS;
    }
}
