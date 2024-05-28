using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectDetect.WMICore.Classes {
    internal class WmiUSBHub : IWmiClassReader {
        internal static readonly string CLASS = "Win32_USBHub";
        internal static readonly List<string> FIELDS = new List<string> {
            "DeviceID",
            "Caption",
            "Description",
            "Name",
            "PNPDeviceID"
        };

        public virtual List<string> GetFields() => FIELDS;
        public virtual string GetClassName() => CLASS;
    }
}
