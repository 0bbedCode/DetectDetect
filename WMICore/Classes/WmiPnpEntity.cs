using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectDetect.WMICore.Classes {
    internal class WmiPnpEntity : IWmiClassReader {
        internal static readonly string CLASS = "Win32_PnPEntity";
        internal static readonly List<string> FIELDS = new List<string> {
            "DeviceID",
            "Caption",
            "ClassGuid",
            "Description",
            "Manufacturer",
            "Name",
            "PNPClass",
            "PNPDeviceID",
            "Present",
            "Service",
            "Status",
            "SystemName"
        };

        public virtual List<string> GetFields() => FIELDS;
        public virtual string GetClassName() => CLASS;
    }
}
