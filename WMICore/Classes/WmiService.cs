using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectDetect.WMICore.Classes {
    internal class WmiService : IWmiClassReader {
        internal static readonly string CLASS = "Win32_Service";
        internal static readonly List<string> FIELDS = new List<string> {
            "Name",
            "Caption",
            "Description",
            "DesktopInteract",
            "DisplayName",
            "PathName",
            "ProcessId",
            "ServiceType",
            "Started",
            "StartMode",
            "StartName",
            "Status",
            "TagId"
        };

        public virtual List<string> GetFields() => FIELDS;
        public virtual string GetClassName() => CLASS;
    }
}
