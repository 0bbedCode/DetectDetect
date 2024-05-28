using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectDetect.WMICore.Classes {
    internal class WmiVideoController : IWmiClassReader {
        internal static readonly string CLASS = "Win32_VideoController";
        internal static readonly List<string> FIELDS = new List<string> {
            "DeviceID",
            "AdapterCompatibility",
            "AdapterDACType",
            "AdapterRAM",
            "Availability",
            "CurrentHorizontalResolution",
            "CurrentVerticalResolution",
            "CurrentNumberOfColors",
            "CurrentRefreshRate",
            "Description",
            "DriverDate",
            "DriverVersion",
            "InfFilename",
            "InstalledDisplayDrivers",
            "MaxRefreshRate",
            "MinRefreshRate",
            "Name",
            "PNPDeviceID",
            "Status",
            "VideoArchitecture",
            "VideoMemoryType",
            "VideoModeDescription",
            "VideoProcessor"
        };

        public virtual List<string> GetFields() => FIELDS;
        public virtual string GetClassName() => CLASS;
    }
}
