using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectDetect.WMICore.Classes {
    internal class WmiNetworkAdapter : IWmiClassReader {
        internal static readonly string CLASS = "Win32_NetworkAdapter";
        internal static readonly List<string> FIELDS = new List<string> {
            "DeviceID",
            "AdapterType",
            "AdapterTypeId",
            "Availability",
            "Caption",
            "Description",
            "Index",
            "InterfaceIndex",
            "Installed",
            "MACAddress",
            "Manufacturer",
            "Name",
            "PNPDeviceID",
            "ProductName",
            "ServiceName",
            "SystemName",
            "TimeOfLastReset"
        };

        public virtual List<string> GetFields() => FIELDS;
        public virtual string GetClassName() => CLASS;
    }
}
