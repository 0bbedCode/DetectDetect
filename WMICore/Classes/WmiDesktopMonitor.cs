using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectDetect.WMICore.Classes {
    internal class WmiDesktopMonitor : IWmiClassReader {
        internal static readonly string CLASS = "Win32_DesktopMonitor";
        internal static readonly List<string> FIELDS = new List<string> {
            "DeviceID",
            "Availability",
            "Caption",
            "Description",
            "MonitorManufacturer",
            "MonitorType",
            "Name",
            "PNPDeviceID",
        };

        public virtual List<string> GetFields() => FIELDS;
        public virtual string GetClassName() => CLASS;
    }
}
