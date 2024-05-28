using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectDetect.WMICore.Classes {
    internal class WmiNetworkAdapterConfiguration : IWmiClassReader {
        internal static readonly string CLASS = "Win32_NetworkAdapterConfiguration";
        internal static readonly List<string> FIELDS = new List<string> {
            "Index",
            "Caption",
            "DatabasePath",
            "Description",
            "DHCPEnabled",
            "DHCPLeaseExpires",
            "DHCPLeaseObtained",
            "DHCPServer",
            "DNSDomain",
            "DNSHostName",
            "InterfaceIndex",
            "IPEnabled",
            "MACAddress",
            "ServiceName",
            "SettingID",
            "TcpipNetbiosOptions",
            "IPFilterSecurityEnabled"
        };

        public virtual List<string> GetFields() => FIELDS;
        public virtual string GetClassName() => CLASS;
    }
}
